using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using BookBird.Domain.Enumerations;
using BookBird.Domain.Primitives;

namespace BookBird.Api.JsonConverters
{
    public class MeetingTypeJsonConverter : JsonConverter<MeetingType>
    {
        public override MeetingType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => 
            Enumeration.FromValue<MeetingType>(reader.GetInt32());

        public override void Write(Utf8JsonWriter writer, MeetingType value, JsonSerializerOptions options) => 
            writer.WriteNumberValue(value.Value);
    }
}