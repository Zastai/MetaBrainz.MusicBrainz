using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class CollectionReader : ObjectReader<Collection> {

  public static readonly CollectionReader Instance = new();

  protected override Collection ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    int? areaCount = null;
    int? artistCount = null;
    EntityType? contentType = null;
    string? editor = null;
    int? eventCount = null;
    Guid? id = null;
    int? instrumentCount = null;
    int? labelCount = null;
    string? name = null;
    int? placeCount = null;
    int? recordingCount = null;
    int? releaseCount = null;
    int? releaseGroupCount = null;
    int? seriesCount = null;
    string? type = null;
    Guid? typeId = null;
    int? workCount = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "area-count":
            areaCount = reader.GetInt32();
            break;
          case "artist-count":
            artistCount = reader.GetInt32();
            break;
          case "editor":
            editor = reader.GetString();
            break;
          case "entity-type":
            contentType = HelperMethods.ParseEntityType(reader.GetString());
            if (contentType == EntityType.Unknown) {
              goto default; // put the actual value in UnhandledProperties
            }
            break;
          case "event-count":
            eventCount = reader.GetInt32();
            break;
          case "id":
            id = reader.GetGuid();
            break;
          case "instrument-count":
            instrumentCount = reader.GetInt32();
            break;
          case "label-count":
            labelCount = reader.GetInt32();
            break;
          case "name":
            name = reader.GetString();
            break;
          case "place-count":
            placeCount = reader.GetInt32();
            break;
          case "recording-count":
            recordingCount = reader.GetInt32();
            break;
          case "release-count":
            releaseCount = reader.GetInt32();
            break;
          case "release-group-count":
            releaseGroupCount = reader.GetInt32();
            break;
          case "series-count":
            seriesCount = reader.GetInt32();
            break;
          case "type":
            type = reader.GetString();
            break;
          case "type-id":
            typeId = reader.GetOptionalGuid();
            break;
          case "work-count":
            workCount = reader.GetInt32();
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
    var itemCount = contentType switch {
      EntityType.Area => areaCount ?? throw new MissingPropertyException("area-count"),
      EntityType.Artist => artistCount ?? throw new MissingPropertyException("artist-count"),
      EntityType.Event => eventCount ?? throw new MissingPropertyException("event-count"),
      EntityType.Instrument => instrumentCount ?? throw new MissingPropertyException("instrument-count"),
      EntityType.Label => labelCount ?? throw new MissingPropertyException("label-count"),
      EntityType.Place => placeCount ?? throw new MissingPropertyException("place-count"),
      EntityType.Recording => recordingCount ?? throw new MissingPropertyException("recording-count"),
      EntityType.Release => releaseCount ?? throw new MissingPropertyException("release-count"),
      EntityType.ReleaseGroup => releaseGroupCount ?? throw new MissingPropertyException("release-group-count"),
      EntityType.Series => seriesCount ?? throw new MissingPropertyException("series-count"),
      EntityType.Work => workCount ?? throw new MissingPropertyException("work-count"),
      _ => -1
    };
    // Add unexpected counts to UnhandledProperties
    CollectionReader.CheckCount(ref rest, contentType == EntityType.Area, "area-count", areaCount);
    CollectionReader.CheckCount(ref rest, contentType == EntityType.Artist, "artist-count", artistCount);
    CollectionReader.CheckCount(ref rest, contentType == EntityType.Event, "event-count", eventCount);
    CollectionReader.CheckCount(ref rest, contentType == EntityType.Instrument, "instrument-count", instrumentCount);
    CollectionReader.CheckCount(ref rest, contentType == EntityType.Label, "label-count", labelCount);
    CollectionReader.CheckCount(ref rest, contentType == EntityType.Place, "place-count", placeCount);
    CollectionReader.CheckCount(ref rest, contentType == EntityType.Recording, "recording-count", recordingCount);
    CollectionReader.CheckCount(ref rest, contentType == EntityType.Release, "release-count", releaseCount);
    CollectionReader.CheckCount(ref rest, contentType == EntityType.ReleaseGroup, "release-group-count", releaseGroupCount);
    CollectionReader.CheckCount(ref rest, contentType == EntityType.Series, "series-count", seriesCount);
    CollectionReader.CheckCount(ref rest, contentType == EntityType.Work, "work-count", workCount);
    // Create the object
    return new Collection {
      ContentType = contentType ?? throw new MissingPropertyException("entity-type"),
      Editor = editor,
      Id = id ?? throw new MissingPropertyException("id"),
      ItemCount = itemCount,
      Name = name ?? "",
      Type = type is "" ? null : type,
      TypeId = typeId,
      UnhandledProperties = rest,
    };
  }

  private static void CheckCount(ref Dictionary<string, object?>? dictionary, bool ok, string name, int? count) {
    if (ok || count is null) {
      return;
    }
    dictionary ??= new Dictionary<string, object?>();
    dictionary[name] = count.Value;
  }

}
