using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects;

namespace MetaBrainz.MusicBrainz.Json.Readers {

  internal sealed class MessageOrErrorReader : ObjectReader<MessageOrError> {

    public static readonly MessageOrErrorReader Instance = new MessageOrErrorReader();

    protected override MessageOrError ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
      string? error = null;
      string? help = null;
      string? message = null;
      Dictionary<string, object?>? rest = null;
      while (reader.TokenType == JsonTokenType.PropertyName) {
        var prop = reader.GetString();
        try {
          reader.Read();
          switch (prop) {
            case "error":
              error = reader.GetString();
              break;
            case "help":
              help = reader.GetString();
              break;
            case "message":
              message = reader.GetString();
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
      return new MessageOrError {
        Error = error,
        Help = help,
        Message = message,
        UnhandledProperties = rest
      };
    }

  }

}
