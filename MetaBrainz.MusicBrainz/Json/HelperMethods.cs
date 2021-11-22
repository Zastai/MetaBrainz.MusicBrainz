using System;
using System.Text.Json;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Json;

internal static class HelperMethods {

  public static TimeSpan? GetOptionalTimeSpanFromMilliseconds(this ref Utf8JsonReader reader) {
    var ms = reader.GetOptionalDouble();
    if (ms == null) {
      return null;
    }
    return TimeSpan.FromMilliseconds(ms.Value);
  }

  public static EntityType ParseEntityType(string? text) {
    return text switch {
      "area" => EntityType.Area,
      "artist" => EntityType.Artist,
      "collection" => EntityType.Collection,
      "event" => EntityType.Event,
      "genre" => EntityType.Genre,
      "instrument" => EntityType.Instrument,
      "label" => EntityType.Label,
      "place" => EntityType.Place,
      "recording" => EntityType.Recording,
      "release" => EntityType.Release,
      "release-group" => EntityType.ReleaseGroup, // for Annotation
      "release_group" => EntityType.ReleaseGroup, // for Collection and Relationship
      "series" => EntityType.Series,
      "url" => EntityType.Url,
      "work" => EntityType.Work,
      _ => EntityType.Unknown
    };
  }

}
