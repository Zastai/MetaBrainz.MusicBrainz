using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.OAuth2;

namespace MetaBrainz.MusicBrainz.Json.OAuth2;

internal sealed class AuthorizationErrorReader : ObjectReader<AuthorizationError> {

  public static readonly AuthorizationErrorReader Instance = new();

  protected override AuthorizationError ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    string? error = null;
    string? description = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "error":
            error = reader.GetString();
            break;
          case "error_description":
            description = reader.GetString();
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
    return new AuthorizationError {
      Error = error,
      Description = description,
      UnhandledProperties = rest
    };
  }

}
