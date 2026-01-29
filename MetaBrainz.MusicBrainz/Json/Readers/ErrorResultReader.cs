using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class ErrorResultReader : ObjectReader<ErrorResult> {

  public static readonly ErrorResultReader Instance = new();

  protected override ErrorResult ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    string? error = null;
    string? help = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "error":
            error = reader.GetString();
            break;
          case "help":
            help = reader.GetString();
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
    return new ErrorResult {
      Error = error ?? "",
      Help = help ?? "",
      UnhandledProperties = rest
    };
  }

}
