using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class CoordinatesReader : ObjectReader<Coordinates> {

  public static readonly CoordinatesReader Instance = new();

  private static double? ReadCoordinate(ref Utf8JsonReader reader, string property) {
    // These are typically numbers, but in search results (e.g. FindPlaces()) they can be strings (issue #48, MBS-).
    switch (reader.TokenType) {
      case JsonTokenType.Null:
        return null;
      case JsonTokenType.Number:
        return reader.GetDouble();
      case JsonTokenType.String:
        var text = reader.GetString();
        // should not be possible, but handle it anyway
        if (text is null) {
          return null;
        }
        try {
          return double.Parse(text, CultureInfo.InvariantCulture);
        }
        catch (Exception e) {
          throw new JsonException($"Invalid {property} value; should be a floating-point value but got '{text}'.", e);
        }
      default:
        throw new JsonException($"Unexpected {property} value; expected null, a number or a string but got [{reader.TokenType}].");
    }
  }

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
            latitude = CoordinatesReader.ReadCoordinate(ref reader, prop);
            break;
          case "longitude":
            longitude = CoordinatesReader.ReadCoordinate(ref reader, prop);
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
    if (latitude is null) {
      throw new JsonException("Expected property 'latitude' not found or null.");
    }
    if (longitude is null) {
      throw new JsonException("Expected property 'longitude' not found or null.");
    }
    return new Coordinates(latitude.Value, longitude.Value) {
      UnhandledProperties = rest,
    };
  }

}
