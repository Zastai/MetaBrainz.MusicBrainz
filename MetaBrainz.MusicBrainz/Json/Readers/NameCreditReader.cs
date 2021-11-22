using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class NameCreditReader : ObjectReader<NameCredit> {

  public static readonly NameCreditReader Instance = new();

  protected override NameCredit ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IArtist? artist = null;
    string? joinPhrase = null;
    string? name = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "artist":
            artist = reader.GetObject(ArtistReader.Instance, options);
            break;
          case "joinphrase":
            joinPhrase = reader.GetString();
            break;
          case "name":
            name = reader.GetString();
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
    return new NameCredit {
      Artist = artist,
      JoinPhrase = joinPhrase,
      Name = name,
      UnhandledProperties = rest,
    };
  }

}
