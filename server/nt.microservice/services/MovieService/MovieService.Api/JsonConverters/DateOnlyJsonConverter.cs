using System.Text.Json;

namespace MovieService.Api.JsonConverters
{
    public class DateOnlyJsonConverter : System.Text.Json.Serialization.JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }

}
