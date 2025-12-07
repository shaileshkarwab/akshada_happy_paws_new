using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Services
{
    public class GoogleMapService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public GoogleMapService(IConfiguration _configuration)
        {
            _apiKey = _configuration.GetSection("GoogleMapApiKey:Key").Value!;
            _httpClient = new HttpClient();
        }

        public async Task<string> GenerateThumbnail(double lat, double lng)
        {
            string url =
                $"https://maps.googleapis.com/maps/api/staticmap?center={lat},{lng}&zoom=14&size=600x400&markers=color:red|{lat},{lng}&key={_apiKey}";

            var bytes = await _httpClient.GetByteArrayAsync(url);

            string folder = Path.Combine("uploaddata", "thumbnails");
            Directory.CreateDirectory(folder);

            string filePath = Path.Combine(folder, $"map_{lat}_{lng}.png");
            await File.WriteAllBytesAsync(filePath, bytes);
            string address = await GetAddressFromLatLng(lat, lng);
            return filePath;
        }


        public async Task<string> GetAddressFromLatLng(double? lat, double? lng)
        {
            try
            {
                if (lat == null || lng == null)
                    return string.Empty;

                string apiKey = _apiKey;
                string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}&key={apiKey}";

                using var client = new HttpClient();
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                string address = json.results[0].formatted_address;

                return address;
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
