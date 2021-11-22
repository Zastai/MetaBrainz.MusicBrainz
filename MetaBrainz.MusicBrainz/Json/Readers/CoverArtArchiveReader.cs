using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class CoverArtArchiveReader : ObjectReader<CoverArtArchive> {

  public static readonly CoverArtArchiveReader Instance = new();

  protected override CoverArtArchive ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    var artwork = false;
    var back = false;
    var darkened = false;
    var count = 0;
    var front = false;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "artwork":
            artwork = reader.GetBoolean();
            break;
          case "back":
            back = reader.GetBoolean();
            break;
          case "count":
            count = reader.GetInt32();
            break;
          case "darkened":
            darkened = reader.GetBoolean();
            break;
          case "front":
            front = reader.GetBoolean();
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
    return new CoverArtArchive {
      Artwork = artwork,
      Back = back,
      Count = count,
      Darkened = darkened,
      Front = front,
      UnhandledProperties = rest,
    };
  }

}