using System.Collections.Generic;
using System.Text.Json.Serialization;

using MetaBrainz.MusicBrainz.Json.Readers;

namespace MetaBrainz.MusicBrainz.Json {

  internal static class Converters {

    public static IEnumerable<JsonConverter> Readers {
      get {
        // First-Class Entities
        yield return AreaReader.Instance;
        yield return ArtistReader.Instance;
        yield return CollectionReader.Instance;
        yield return EventReader.Instance;
        yield return InstrumentReader.Instance;
        yield return IsrcReader.Instance;
        yield return LabelReader.Instance;
        yield return PlaceReader.Instance;
        yield return RecordingReader.Instance;
        yield return ReleaseReader.Instance;
        yield return ReleaseGroupReader.Instance;
        yield return SeriesReader.Instance;
        yield return UrlReader.Instance;
        yield return WorkReader.Instance;
        // Other objects we deserialize
        yield return DiscIdLookupResultReader.Instance;
        yield return MessageOrErrorReader.Instance;
        yield return TagReader.Instance;
      }
    }

  }

}
