using System;

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

    public static TO[] WrapArray<TO, TJ>(this TJ[] json, ref TO[] array, Func<TJ, TO> wrap) where TO : class where TJ : class {
      if (json == null)
        return null;
      if (array != null)
        return array;
      array = new TO[json.Length];
      for (var i = 0; i < json.Length; ++i)
        array[i] = wrap(json[i]);
      return array;
    }

    public static TO WrapObject<TO, TJ>(this TJ json, ref TO obj, Func<TJ, TO> wrap) where TO : class where TJ : class {
      if (json == null)
        return null;
      if (obj != null)
        return obj;
      return (obj = wrap(json));
    }

  }

}
