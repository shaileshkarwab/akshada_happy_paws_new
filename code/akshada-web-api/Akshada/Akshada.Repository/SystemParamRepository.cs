using Akshada.DTO.Enums;
using Akshada.DTO.Models;
using Akshada.EFCore.DbModels;
using Akshada.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Repository
{
    public class SystemParamRepository : GenericRepository<SystemParameter>, ISystemParamRepository
    {
        private readonly AkshadaPawsContext _context;
        private readonly IConfiguration _configuration;
        public SystemParamRepository(AkshadaPawsContext akshadaPawsContext, IConfiguration configuration, IServiceProvider services) : base(akshadaPawsContext, configuration, services)
        {
            _context = akshadaPawsContext;
            _configuration = configuration;
        }

        public override IEnumerable<SystemParameter> Find(Expression<Func<SystemParameter, bool>> expression)
        {
            var body = expression.Body as BinaryExpression;
            if (body != null)
            {
                string propertyName = ((MemberExpression)body.Left).Member.Name;
                object value = Expression.Lambda(body.Right).Compile().DynamicInvoke();
                string operation = body.NodeType.ToString(); // Equal, GreaterThan, LessThan...
                bool blnExecuteInsert = ExecuteMethod(value.ToString());
                if (blnExecuteInsert)
                {
                    // check for Start of Walk and End of Walk in system parameter
                    var jsonData = _configuration.GetSection("Appsettings:DefaultSystemParamData").Get<List<DTO_SystemParamFromAppSetting>>();
                    foreach (var m in jsonData)
                    {
                        foreach (var d in m.data)
                        {
                            var isDataExisting = this.akshadaPawsContext.SystemParameters.Any(p => p.EnumId == m.enum_id && p.ParamAbbrivation == d.param_abbrivation);
                            if (!isDataExisting)
                            {
                                this.akshadaPawsContext.SystemParameters.Add(new SystemParameter
                                {
                                    RowId = System.Guid.NewGuid().ToString(),
                                    ParamValue = d.param_value,
                                    EnumId = m.enum_id,
                                    ParamAbbrivation = d.param_abbrivation,
                                    CreatedAt = System.DateTime.Now,
                                    CreatedBy = 1,
                                    UpdatedBy = 1,
                                    UpdatedAt = System.DateTime.Now
                                });
                                this.akshadaPawsContext.SaveChanges();
                            }
                        }
                    }
                }
            }
            return base.Find(expression);
        }

        bool ExecuteMethod(string value)
        {
            return Convert.ToInt32(value) == (Int32)SystemParameterEnum.TypeOfImagesToUploadForServices ||
                    Convert.ToInt32(value) == (Int32)SystemParameterEnum.MessageTemplateVariables;
        }
    }
}
