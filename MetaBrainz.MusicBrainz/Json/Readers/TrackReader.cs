using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class TrackReader : ObjectReader<Track> {

  public static readonly TrackReader Instance = new();

  protected override Track ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IReadOnlyList<INameCredit>? artistCredit = null;
    Guid? id = null;
    TimeSpan? length = null;
    string? number = null;
    int? position = null;
    IRecording? recording = null;
    string? title = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "artist-credit":
            artistCredit = reader.ReadList(NameCreditReader.Instance, options);
            break;
          case "id":
            id = reader.GetGuid();
            break;
          case "length":
            length = reader.GetOptionalTimeSpanFromMilliseconds();
            break;
          case "number":
            number = reader.GetString();
            break;
          case "position":
            position = reader.GetOptionalInt32();
            break;
          case "recording":
            recording = reader.GetOptionalObject(RecordingReader.Instance, options);
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
    return new Track {
      ArtistCredit = artistCredit,
      Id = id ?? throw new MissingPropertyException("id"),
      Length = length,
      Number = number,
      Position = position,
      Recording = recording,
      Title = title ?? throw new MissingPropertyException("title"),
      UnhandledProperties = rest,
    };
  }

}
