using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class MediumReader : ObjectReader<Medium> {

  public static readonly MediumReader Instance = new();

  protected override Medium ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IReadOnlyList<ITrack>? dataTracks = null;
    IReadOnlyList<IDisc>? discs = null;
    string? format = null;
    Guid? formatId = null;
    Guid? id = null;
    int? position = null;
    ITrack? pregap = null;
    string? title = null;
    int? trackCount = null;
    int? trackOffset = null;
    IReadOnlyList<ITrack>? tracks = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "data-tracks":
            dataTracks = reader.ReadList(TrackReader.Instance, options);
            break;
          case "discs":
            discs = reader.ReadList(DiscReader.Instance, options);
            break;
          case "format":
            format = reader.GetString();
            break;
          case "format-id":
            formatId = reader.GetOptionalGuid();
            break;
          case "id":
            id = reader.GetOptionalGuid();
            break;
          case "position":
            position = reader.GetOptionalInt32();
            break;
          case "pregap":
            pregap = reader.GetOptionalObject(TrackReader.Instance, options);
            break;
          case "title":
            title = reader.GetString();
            break;
          case "track-count":
            trackCount = reader.GetOptionalInt32();
            break;
          case "track-offset":
            trackOffset = reader.GetOptionalInt32();
            break;
          case "track": // SEARCH-604
          case "tracks":
            tracks = reader.ReadList(TrackReader.Instance, options);
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
    return new Medium {
      DataTracks = dataTracks ?? [],
      Discs = discs ?? [],
      Format = format is "" ? null : format,
      FormatId = formatId,
      Id = id,
      Position = position ?? -1,
      Pregap = pregap,
      Title = title ?? "",
      TrackCount = trackCount ?? 0,
      TrackOffset = trackOffset,
      Tracks = tracks ?? [],
      UnhandledProperties = rest,
    };
  }

}
