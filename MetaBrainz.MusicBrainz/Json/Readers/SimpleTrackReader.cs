using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers {

  internal sealed class SimpleTrackReader : ObjectReader<SimpleTrack> {

    public static readonly SimpleTrackReader Instance = new SimpleTrackReader();

    protected override SimpleTrack ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
      string? artist = null;
      TimeSpan? length = null;
      string? title = null;
      Dictionary<string, object?>? rest = null;
      while (reader.TokenType == JsonTokenType.PropertyName) {
        var prop = reader.GetString();
        try {
          reader.Read();
          switch (prop) {
            case "artist":
              artist = reader.GetString();
              break;
            case "length":
              length = TimeSpan.FromMilliseconds(reader.GetDouble());
              break;
            case "title":
              title = reader.GetString();
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
      if (title == null)
        throw new JsonException("Expected track title not found or null.");
      if (!length.HasValue)
        throw new JsonException("Expected track length not found or null.");
      return new SimpleTrack(title, length.Value) {
        Artist = artist,
        UnhandledProperties = rest,
      };
    }

  }

}
