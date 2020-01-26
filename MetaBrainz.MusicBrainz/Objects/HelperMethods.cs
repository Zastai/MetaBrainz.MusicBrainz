namespace MetaBrainz.MusicBrainz.Objects {

  internal static class HelperMethods {

    public static EntityType SetFrom(out EntityType? et, string text) {
      return text switch {
        "area"          => (et = EntityType.Area        ).Value,
        "artist"        => (et = EntityType.Artist      ).Value,
        "collection"    => (et = EntityType.Collection  ).Value,
        "event"         => (et = EntityType.Event       ).Value,
        "instrument"    => (et = EntityType.Instrument  ).Value,
        "label"         => (et = EntityType.Label       ).Value,
        "place"         => (et = EntityType.Place       ).Value,
        "recording"     => (et = EntityType.Recording   ).Value,
        "release"       => (et = EntityType.Release     ).Value,
        "release_group" => (et = EntityType.ReleaseGroup).Value,
        "series"        => (et = EntityType.Series      ).Value,
        "url"           => (et = EntityType.Url         ).Value,
        "work"          => (et = EntityType.Work        ).Value,
        _               => (et = EntityType.Unknown     ).Value
      };
    }

  }

}
