using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class CdStubReader : ObjectReader<CdStub> {

  public static readonly CdStubReader Instance = new CdStubReader();

  protected override CdStub ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    string? artist = null;
    string? barcode = null;
    string? disambiguation = null;
    string? id = null;
    string? title = null;
    int? trackCount = null;
    IReadOnlyList<ISimpleTrack>? tracks = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "artist":
            artist = reader.GetString();
            break;
          case "barcode":
            barcode = reader.GetString();
            break;
          case "disambiguation":
            disambiguation = reader.GetString();
            break;
          case "id":
            id = reader.GetString();
            break;
          case "title":
            title = reader.GetString();
            break;
          case "count": // SEARCH-608
          case "track-count":
            trackCount = reader.GetOptionalInt32();
            break;
          case "tracks":
            tracks = reader.ReadList(SimpleTrackReader.Instance, options);
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
    if (id == null) {
      throw new JsonException("Expected disc ID not found or null.");
    }
    if (title == null) {
      throw new JsonException("Expected title not found or null.");
    }
    if (trackCount.HasValue && tracks != null) {
      var reported = trackCount.Value;
      var actual = tracks?.Count ?? 0;
      if (reported != actual) {
        throw new JsonException($"The number of tracks ({actual}) does not match the reported track count ({reported}).");
      }
    }
    return new CdStub(id, title) {
      Artist = artist,
      Barcode = barcode,
      Disambiguation = disambiguation,
      TrackCount = trackCount ?? 0,
      Tracks = tracks,
      UnhandledProperties = rest,
    };
  }

}