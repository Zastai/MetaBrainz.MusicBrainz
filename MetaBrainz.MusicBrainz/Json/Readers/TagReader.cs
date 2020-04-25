using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers {

  internal sealed class TagReader : ObjectReader<Tag> {

    public static readonly TagReader Instance = new TagReader();

    protected override Tag ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
      int? count = null;
      string? name = null;
      byte? score = null;
      Dictionary<string, object?>? rest = null;
      while (reader.TokenType == JsonTokenType.PropertyName) {
        var prop = reader.GetString();
        try {
          reader.Read();
          switch (prop) {
            case "count":
              count = reader.GetInt32();
              break;
            case "name":
              name = reader.GetString();
              break;
            case "score":
              score = reader.GetByte();
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
      if (name == null)
        throw new JsonException("Expected tag name not found or null.");
      return new Tag(name) {
        SearchScore = score,
        UnhandledProperties = rest,
        VoteCount = count,
      };
    }

  }

}
