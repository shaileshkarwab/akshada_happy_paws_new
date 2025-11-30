using System.Text.Json;
using System.Text.Json.Serialization;

namespace Akshada.API.Convertors
{
    public class IndiaTimeZoneConverter : JsonConverter<DateTime>
    {
        private readonly TimeZoneInfo _indiaTimeZone;

        public IndiaTimeZoneConverter()
        {
            try
            {
                // Windows
                _indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                // Linux
                _indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata");
            }
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var utcTime = reader.GetDateTime();
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, _indiaTimeZone);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var indiaTime = TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.SpecifyKind(value, DateTimeKind.Utc), _indiaTimeZone);

            writer.WriteStringValue(indiaTime.ToString("yyyy-MM-ddTHH:mm:ss.fffK"));
        }
    }
}
