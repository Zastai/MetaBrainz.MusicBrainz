using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class CoordinatesReader : ObjectReader<Coordinates> {

  public static readonly CoordinatesReader Instance = new();

  protected override Coordinates ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    double? latitude = null;
    double? longitude = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "latitude":
            latitude = reader.GetOptionalDouble();
            break;
          case "longitude":
            longitude = reader.GetOptionalDouble();
            break;
          default:
            rest ??= new Dictionary<string, object?>();
            rest[prop] = reader.GetOptionalObject(options);
            break;
        }
      }
      catch (Exception e) {
        throw new JsonException($"Failed to deserialize the '{prop}' property.", e);
      }
      reader.Read();
    }
    if (!latitude.HasValue) {
      throw new JsonException("Expected property 'latitude' not found or null.");
    }
    if (!longitude.HasValue) {
      throw new JsonException("Expected property 'longitude' not found or null.");
    }
    return new Coordinates(latitude.Value, longitude.Value) {
      UnhandledProperties = rest,
    };
  }

}