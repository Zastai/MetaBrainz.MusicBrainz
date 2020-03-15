namespace MetaBrainz.MusicBrainz.Objects {

  internal static class HelperMethods {

    public static EntityType ParseEntityType(string? text) {
      return text switch {
        "area"          => EntityType.Area,
        "artist"        => EntityType.Artist,
        "collection"    => EntityType.Collection,
        "event"         => EntityType.Event,
        "instrument"    => EntityType.Instrument,
        "label"         => EntityType.Label,
        "place"         => EntityType.Place,
        "recording"     => EntityType.Recording,
        "release"       => EntityType.Release,
        "release_group" => EntityType.ReleaseGroup,
        "series"        => EntityType.Series,
        "url"           => EntityType.Url,
        "work"          => EntityType.Work,
        _               => EntityType.Unknown
      };
    }

  }

}
