using System;
using Newtonsoft.Json;
using SteamWebApiLib.Models.AppDetails;

namespace SteamWebApiLib.Models.Common
{
    // Converts price strings to double, e.g. 2599 => 25.99
    public class SteamPriceStringConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;


        public SteamPriceStringConverter()
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = reader.Value.ToString();

            if (value.Length < 2)
            {
                return double.Parse($".{value}");
            }

            return double.Parse(value.Insert(value.Length - 2, "."));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }

    // Converts a string to a ControllerSupport enum
    public class ControllerSupportConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanConvert(Type t) =>
            t == typeof(ControllerSupport) || t == typeof(ControllerSupport?);


        public ControllerSupportConverter()
        {
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);

            if (Enum.TryParse(value, true, out ControllerSupport convertedValue))
            {
                return convertedValue;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    // Converts an epoch string to a datetime object
    public class EpochToDateTimeConverter : JsonConverter
    {
        public override bool CanRead => true;


        public EpochToDateTimeConverter()
        {
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);

            if (long.TryParse(value, out long parsedValue))
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(parsedValue);
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object untypedValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    // Returns a Requirements object if the provided data is valid, otherwise returns null
    public class RequirementsConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;


        public RequirementsConverter()
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject) return null;
            return serializer.Deserialize<Requirements>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }

}
