using System.Text.Json;
using System.Text.Json.Serialization;

namespace SMBTools.Contract.CustomConverters
{
    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        private readonly string _serializationFormat = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.TryParseExact(reader.GetString(), _serializationFormat, out var dateOnly) ? dateOnly : default;
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString(_serializationFormat));
    }
}
