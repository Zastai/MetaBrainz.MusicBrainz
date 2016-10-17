using System;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal static class HelperMethods {

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
