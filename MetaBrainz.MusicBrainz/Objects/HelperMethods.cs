namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal static class HelperMethods {

    public static EntityType SetFrom(out EntityType? et, string text) {
      switch (text) {
        case "area":          return (et = EntityType.Area        ).Value;
        case "artist":        return (et = EntityType.Artist      ).Value;
        case "collection":    return (et = EntityType.Collection  ).Value;
        case "event":         return (et = EntityType.Event       ).Value;
        case "instrument":    return (et = EntityType.Instrument  ).Value;
        case "label":         return (et = EntityType.Label       ).Value;
        case "place":         return (et = EntityType.Place       ).Value;
        case "recording":     return (et = EntityType.Recording   ).Value;
        case "release":       return (et = EntityType.Release     ).Value;
        case "release_group": return (et = EntityType.ReleaseGroup).Value;
        case "series":        return (et = EntityType.Series      ).Value;
        case "url":           return (et = EntityType.Url         ).Value;
        case "work":          return (et = EntityType.Work        ).Value;
        default:              return (et = EntityType.Unknown     ).Value;
      }
    }

  }

}
