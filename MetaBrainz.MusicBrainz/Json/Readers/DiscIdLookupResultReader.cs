using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class DiscIdLookupResultReader : ObjectReader<DiscIdLookupResult> {

  public static readonly DiscIdLookupResultReader Instance = new();

  // Currently this can be:
  // - a serialized Disc
  // - a serialized CD stub
  // - a list of releases (as a serialized object containing only a "releases": [...] property)
  // It would be nicer if the first two had a wrapper object with a disc and stub property, respectively.
  protected override DiscIdLookupResult ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    string? artist = null;
    string? barcode = null;
    string? disambiguation = null;
    string? id = null;
    int? offsetCount = null;
    IReadOnlyList<int>? offsets = null;
    int? releaseCount = null;
    int? releaseOffset = null;
    IReadOnlyList<IRelease>? releases = null;
    int? sectors = null;
    string? title = null;
    int? trackCount = null;
    IReadOnlyList<ISimpleTrack>? tracks = null;
    var rest = new Dictionary<string, object?>();
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
          case "offset-count":
            offsetCount = reader.GetOptionalInt32();
            break;
          case "offsets":
            offsets = reader.ReadList<int>(options);
            break;
          case "release-count":
            releaseCount = reader.GetOptionalInt32();
            break;
          case "release-offset":
            releaseOffset = reader.GetOptionalInt32();
            break;
          case "releases":
            releases = reader.ReadList(ReleaseReader.Instance, options);
            break;
          case "sectors":
            sectors = reader.GetOptionalInt32();
            break;
          case "title":
            title = reader.GetString();
            break;
          case "track-count":
            trackCount = reader.GetOptionalInt32();
            break;
          case "tracks":
            tracks = reader.ReadList(SimpleTrackReader.Instance, options);
            break;
          default:
            rest[prop] = reader.GetOptionalObject(options);
            break;
        }
      }
      catch (Exception e) {
        throw new JsonException($"Failed to deserialize the '{prop}' property.", e);
      }
      reader.Read();
    }
    DiscIdLookupResult result = new DiscIdLookupResult();
    if (id != null && offsets != null && sectors.HasValue) { // Disc
      if (offsetCount.HasValue) {
        var reported = offsetCount.Value;
        var actual = offsets.Count;
        if (reported != actual) {
          throw new JsonException($"The number of offsets ({actual}) does not match the reported offset count ({reported}).");
        }
      }
      result.Disc = new Disc(id, offsets, sectors.Value) {
        Releases = releases,
      };
      // clear used fields
      id = null;
      offsetCount = null;
      offsets = null;
      releases = null;
      sectors = null;
    }
    else if (id != null && title != null && tracks != null) { // Stub
      if (trackCount.HasValue) {
        var reported = trackCount.Value;
        var actual = tracks?.Count ?? 0;
        if (reported != actual) {
          throw new JsonException($"The number of tracks ({actual}) does not match the reported track count ({reported}).");
        }
      }
      result.Stub = new CdStub(id, title) {
        Artist = artist,
        Barcode = barcode,
        Disambiguation = disambiguation,
        TrackCount = trackCount ?? 0,
        Tracks = tracks,
      };
      // clear used fields
      artist = null;
      barcode = null;
      disambiguation = null;
      id = null;
      title = null;
      trackCount = null;
      tracks = null;
    }
    else if (id == null && releases != null) { // Fuzzy Lookup - release list
      if (releaseCount.HasValue) {
        var reported = releaseCount.Value;
        var actual = releases?.Count ?? 0;
        if (reported != actual) // FIXME: Or should this just throw?
        {
          rest["release-count"] = releaseCount.Value;
        }
      }
      if (releaseOffset.HasValue && releaseOffset.Value != 0) // FIXME: Or should this just throw?
      {
        rest["release-offset"] = releaseOffset.Value;
      }
      result.Releases = releases;
      // clear used fields
      releaseCount = null;
      releaseOffset = null;
      releases = null;
    }
    // any field still set at this point is unhandled
    if (artist != null) {
      rest["artist"] = artist;
    }
    if (barcode != null) {
      rest["barcode"] = barcode;
    }
    if (disambiguation != null) {
      rest["disambiguation"] = disambiguation;
    }
    if (id != null) {
      rest["id"] = id;
    }
    if (offsetCount.HasValue) {
      rest["offset-count"] = offsetCount.Value;
    }
    if (offsets != null) {
      rest["offsets"] = offsets;
    }
    if (releaseCount.HasValue) {
      rest["release-count"] = releaseCount.Value;
    }
    if (releaseOffset.HasValue) {
      rest["release-offset"] = releaseOffset.Value;
    }
    if (releases != null) {
      rest["releases"] = releases;
    }
    if (sectors.HasValue) {
      rest["sectors"] = sectors.Value;
    }
    if (title != null) {
      rest["title"] = title;
    }
    if (trackCount.HasValue) {
      rest["track-count"] = trackCount.Value;
    }
    if (tracks != null) {
      rest["tracks"] = tracks;
    }
    result.UnhandledProperties = rest.Count != 0 ? rest : null;
    return result;
  }

}