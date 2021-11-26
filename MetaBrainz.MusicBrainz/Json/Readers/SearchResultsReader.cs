using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Entities;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class SearchResultsReader : ObjectReader<SearchResults> {

  public static readonly SearchResultsReader Instance = new();


  protected override SearchResults ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IReadOnlyList<ISearchResult<IAnnotation>>? annotations = null;
    IReadOnlyList<ISearchResult<IArea>>? areas = null;
    IReadOnlyList<ISearchResult<IArtist>>? artists = null;
    IReadOnlyList<ISearchResult<ICdStub>>? cdStubs = null;
    int? count = null;
    DateTimeOffset? created = null;
    IReadOnlyList<ISearchResult<IEvent>>? events = null;
    IReadOnlyList<ISearchResult<IInstrument>>? instruments = null;
    IReadOnlyList<ISearchResult<ILabel>>? labels = null;
    int? offset = null;
    IReadOnlyList<ISearchResult<IPlace>>? places = null;
    IReadOnlyList<ISearchResult<IRecording>>? recordings = null;
    IReadOnlyList<ISearchResult<IReleaseGroup>>? releaseGroups = null;
    IReadOnlyList<ISearchResult<IRelease>>? releases = null;
    IReadOnlyList<ISearchResult<ISeries>>? series = null;
    IReadOnlyList<ISearchResult<ITag>>? tags = null;
    IReadOnlyList<ISearchResult<IUrl>>? urls = null;
    IReadOnlyList<ISearchResult<IWork>>? works = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "count":
            count = reader.GetOptionalInt32();
            break;
          case "created":
            created = reader.GetOptionalDateTimeOffset();
            break;
          case "offset":
            offset = reader.GetOptionalInt32();
            break;
          case "annotations":
            annotations = reader.ReadList(SearchResultReader<Annotation>.Instance, options);
            break;
          case "areas":
            areas = reader.ReadList(SearchResultReader<Area>.Instance, options);
            break;
          case "artists":
            artists = reader.ReadList(SearchResultReader<Artist>.Instance, options);
            break;
          case "cdstubs":
            cdStubs = reader.ReadList(SearchResultReader<CdStub>.Instance, options);
            break;
          case "events":
            events = reader.ReadList(SearchResultReader<Event>.Instance, options);
            break;
          case "instruments":
            instruments = reader.ReadList(SearchResultReader<Instrument>.Instance, options);
            break;
          case "labels":
            labels = reader.ReadList(SearchResultReader<Label>.Instance, options);
            break;
          case "places":
            places = reader.ReadList(SearchResultReader<Place>.Instance, options);
            break;
          case "recordings":
            recordings = reader.ReadList(SearchResultReader<Recording>.Instance, options);
            break;
          case "release-groups":
            releaseGroups = reader.ReadList(SearchResultReader<ReleaseGroup>.Instance, options);
            break;
          case "releases":
            releases = reader.ReadList(SearchResultReader<Release>.Instance, options);
            break;
          case "series":
            series = reader.ReadList(SearchResultReader<Series>.Instance, options);
            break;
          case "tags":
            tags = reader.ReadList(SearchResultReader<Tag>.Instance, options);
            break;
          case "urls":
            urls = reader.ReadList(SearchResultReader<Url>.Instance, options);
            break;
          case "works":
            works = reader.ReadList(SearchResultReader<Work>.Instance, options);
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
    if (count is null) {
      throw new JsonException("Expected result count not found or null.");
    }
    if (offset is null) {
      throw new JsonException("Expected result offset not found or null.");
    }
    if (created is null) {
      throw new JsonException("Expected result creation timestamp not found or null.");
    }
    return new SearchResults(count.Value, offset.Value, created.Value) {
      Annotations = annotations,
      Areas = areas,
      Artists = artists,
      CdStubs = cdStubs,
      Events = events,
      Instruments = instruments,
      Labels = labels,
      Places = places,
      Recordings = recordings,
      ReleaseGroups = releaseGroups,
      Releases = releases,
      Series = series,
      Tags = tags,
      UnhandledProperties = rest,
      Urls = urls,
      Works = works,
    };
  }

}
