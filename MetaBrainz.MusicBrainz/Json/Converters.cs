using System.Collections.Generic;
using System.Text.Json.Serialization;

using MetaBrainz.MusicBrainz.Json.Readers;

namespace MetaBrainz.MusicBrainz.Json {

  internal static class Converters {

    public static IEnumerable<JsonConverter> Readers {
      get {
        // Primary Entities
        yield return AreaReader.Instance;
        yield return ArtistReader.Instance;
        yield return EventReader.Instance;
        yield return GenreReader.Instance;
        yield return InstrumentReader.Instance;
        yield return LabelReader.Instance;
        yield return PlaceReader.Instance;
        yield return RecordingReader.Instance;
        yield return ReleaseReader.Instance;
        yield return ReleaseGroupReader.Instance;
        yield return SeriesReader.Instance;
        yield return UrlReader.Instance;
        yield return WorkReader.Instance;
        // Secondary Lookup Results
        yield return CollectionReader.Instance;
        yield return DiscIdLookupResultReader.Instance;
        yield return IsrcReader.Instance;
        // Search Results
        yield return AnnotationReader.Instance;
        yield return CdStubReader.Instance;
        yield return SearchResultsReader.Instance;
        yield return TagReader.Instance;
        // Other objects we deserialize
        yield return MessageOrErrorReader.Instance;
      }
    }

  }

}
