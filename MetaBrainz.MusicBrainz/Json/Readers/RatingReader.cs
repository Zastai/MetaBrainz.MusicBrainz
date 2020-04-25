using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers {

  internal sealed class RatingReader : ObjectReader<Rating> {

    public static readonly RatingReader Instance = new RatingReader();

    protected override Rating ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
      decimal? value = null;
      int? voteCount = null;
      Dictionary<string, object?>? rest = null;
      while (reader.TokenType == JsonTokenType.PropertyName) {
        var prop = reader.GetString();
        try {
          reader.Read();
          switch (prop) {
            case "value":
              value = reader.GetOptionalDecimal();
              break;
            case "votes-count":
              voteCount = reader.GetOptionalInt32();
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
      return new Rating {
        UnhandledProperties = rest,
        Value = value,
        VoteCount = voteCount,
      };
    }

  }

}
