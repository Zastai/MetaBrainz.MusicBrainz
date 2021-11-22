using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class TextRepresentationReader : ObjectReader<TextRepresentation> {

  public static readonly TextRepresentationReader Instance = new TextRepresentationReader();

  protected override TextRepresentation ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    string? language = null;
    string? script = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "language":
            language = reader.GetString();
            break;
          case "script":
            script = reader.GetString();
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
    return new TextRepresentation {
      Language = language,
      Script = script,
      UnhandledProperties = rest,
    };
  }

}