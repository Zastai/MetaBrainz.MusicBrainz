using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class MessageResultReader : ObjectReader<MessageResult> {

  public static readonly MessageResultReader Instance = new();

  protected override MessageResult ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    string? message = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
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
    return new MessageResult {
      Message = message ?? "",
      UnhandledProperties = rest
    };
  }

}
