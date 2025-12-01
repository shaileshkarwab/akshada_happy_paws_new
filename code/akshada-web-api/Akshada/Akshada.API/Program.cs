//scriban

//Scaffold-DbContext "server=localhost;user id=root;password=Admin@1234#;database=akshada_paws" Pomelo.EntityFrameworkCore.MySql -o DbModels -f

using Akshada.API.CommonConfiguration;
using Akshada.API.Convertors;
using Akshada.API.CustomExceptionMiddleware;
using Akshada.API.Migrations;
using Akshada.API.ServiceRegistration;
using Akshada.EFCore.DbModels;
using Akshada.Services.Services;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.Converters.Add(new IndiaTimeZoneConverter());
//    });

builder.Services.AddControllers();

//adding fluent migrator services
var connectionString = builder.Configuration.GetValue<String>("DatabaseConnection:Default");
builder.Services
    .AddLogging(c => c.AddFluentMigratorConsole())
    .AddFluentMigratorCore()
    .ConfigureRunner(c => c.AddMySql8()
    .WithGlobalConnectionString(connectionString)
    .ScanIn(Assembly.GetExecutingAssembly()).For.All()
    );


// adding - enableing cors
const string policyName = "CorsPolicy";

var allowedOrigins = new[] {
    "https://akshada-api.vpobcenter.com",
    "http://localhost:4200",
    "https://localhost:4200",
    "https://manager.akshadashappypaws.com"
};

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName, builder =>
    {
        builder.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithExposedHeaders("X-Pagination");
    });
});



// adding db context
builder.Services.AddDbContext<AkshadaPawsContext>(c => { c.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); });


//adding and registering services
builder.Services.AddServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});


//jwt token specifications
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});


builder.Services.Configure<GoogleServiceAccountOptions>(
    builder.Configuration.GetSection("GoogleDriveServiceAccount"));


// Force JSON to use UTC everywhere
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new UtcDateTimeConverter());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors(policyName);
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.UseMigrations();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"uploaddata")),
    RequestPath = new PathString("/uploaddata")
});
app.UseMiddleware<ExceptionMiddleware>();
app.SetCommonConfiguration();

EnsureRequiredFolders(app);

//registering signal t hub
app.MapHub<HubMessage>("/api/synchub");
app.Run();


static void EnsureRequiredFolders(WebApplication app)
{
    string[] folders = app.Configuration.GetSection("Folders:FoldersForUse").Get<string[]>(); ;

    List<string> foldersToCheck = [];
    foreach(var folder in folders)
    {
        foldersToCheck.Add(Path.Combine(string.Format("{0}\\{1}",  app.Environment.ContentRootPath, "uploaddata"), folder));
    }

    foreach (var folder in foldersToCheck)
    {
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
    }
}