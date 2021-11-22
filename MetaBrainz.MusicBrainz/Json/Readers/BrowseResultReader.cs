using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class BrowseResultReader : ObjectReader<BrowseResult> {

  public static readonly BrowseResultReader Instance = new BrowseResultReader();

  protected override BrowseResult ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IReadOnlyList<IArea>? areas = null;
    IReadOnlyList<IArtist>? artists = null;
    IReadOnlyList<ICollection>? collections = null;
    int? count = null;
    IReadOnlyList<IEvent>? events = null;
    IReadOnlyList<IInstrument>? instruments = null;
    IReadOnlyList<ILabel>? labels = null;
    int? offset = null;
    IReadOnlyList<IPlace>? places = null;
    IReadOnlyList<IRecording>? recordings = null;
    IReadOnlyList<IReleaseGroup>? releaseGroups = null;
    IReadOnlyList<IRelease>? releases = null;
    IReadOnlyList<ISeries>? series = null;
    IReadOnlyList<IWork>? works = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "area-count":
          case "artist-count":
          case "collection-count":
          case "event-count":
          case "instrument-count":
          case "label-count":
          case "place-count":
          case "recording-count":
          case "release-count":
          case "release-group-count":
          case "series-count":
          case "work-count":
            count = reader.GetInt32();
            break;
          case "area-offset":
          case "artist-offset":
          case "collection-offset":
          case "event-offset":
          case "instrument-offset":
          case "label-offset":
          case "place-offset":
          case "recording-offset":
          case "release-offset":
          case "release-group-offset":
          case "series-offset":
          case "work-offset":
            offset = reader.GetInt32();
            break;
          case "areas":
            areas = reader.ReadList(AreaReader.Instance, options);
            break;
          case "artists":
            artists = reader.ReadList(ArtistReader.Instance, options);
            break;
          case "collections":
            collections = reader.ReadList(CollectionReader.Instance, options);
            break;
          case "events":
            events = reader.ReadList(EventReader.Instance, options);
            break;
          case "instruments":
            instruments = reader.ReadList(InstrumentReader.Instance, options);
            break;
          case "labels":
            labels = reader.ReadList(LabelReader.Instance, options);
            break;
          case "places":
            places = reader.ReadList(PlaceReader.Instance, options);
            break;
          case "recordings":
            recordings = reader.ReadList(RecordingReader.Instance, options);
            break;
          case "release-groups":
            releaseGroups = reader.ReadList(ReleaseGroupReader.Instance, options);
            break;
          case "releases":
            releases = reader.ReadList(ReleaseReader.Instance, options);
            break;
          case "series":
            series = reader.ReadList(SeriesReader.Instance, options);
            break;
          case "works":
            works = reader.ReadList(WorkReader.Instance, options);
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
    if (count == null)
      throw new JsonException("Expected result count not found or null.");
    if (offset == null)
      throw new JsonException("Expected result offset not found or null.");
    return new BrowseResult(count.Value, offset.Value) {
      Areas = areas,
      Artists = artists,
      Collections = collections,
      Events = events,
      Instruments = instruments,
      Labels = labels,
      Places = places,
      Recordings = recordings,
      ReleaseGroups = releaseGroups,
      Releases = releases,
      Series = series,
      UnhandledProperties = rest,
      Works = works,
    };
  }

}