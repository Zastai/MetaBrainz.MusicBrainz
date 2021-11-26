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
    if (id is null) {
      throw new JsonException("Expected property 'id' not found or null.");
    }
    if (contentType is null) {
      throw new JsonException("Expected entity type not found or null.");
    }
    int itemCount;
    switch (contentType.Value) {
      case EntityType.Area:
        itemCount = areaCount ?? throw new JsonException("Expected area count not found or null.");
        break;
      case EntityType.Artist:
        itemCount = artistCount ?? throw new JsonException("Expected artist count not found or null.");
        break;
      case EntityType.Event:
        itemCount = eventCount ?? throw new JsonException("Expected event count not found or null.");
        break;
      case EntityType.Instrument:
        itemCount = instrumentCount ?? throw new JsonException("Expected instrument count not found or null.");
        break;
      case EntityType.Label:
        itemCount = labelCount ?? throw new JsonException("Expected label count not found or null.");
        break;
      case EntityType.Place:
        itemCount = placeCount ?? throw new JsonException("Expected place count not found or null.");
        break;
      case EntityType.Recording:
        itemCount = recordingCount ?? throw new JsonException("Expected recording count not found or null.");
        break;
      case EntityType.Release:
        itemCount = releaseCount ?? throw new JsonException("Expected release count not found or null.");
        break;
      case EntityType.ReleaseGroup:
        itemCount = releaseGroupCount ?? throw new JsonException("Expected release group count not found or null.");
        break;
      case EntityType.Series:
        itemCount = seriesCount ?? throw new JsonException("Expected series count not found or null.");
        break;
      case EntityType.Work:
        itemCount = workCount ?? throw new JsonException("Expected work count not found or null.");
        break;
      default:
        itemCount = -1;
        break;
    }
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
    return new Collection(id.Value, contentType.Value, itemCount) {
      Editor = editor,
      Name = name,
      Type = type,
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
