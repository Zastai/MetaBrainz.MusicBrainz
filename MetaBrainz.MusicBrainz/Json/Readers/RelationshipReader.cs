using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class RelationshipReader : ObjectReader<Relationship> {

  public static readonly RelationshipReader Instance = new();

  protected override Relationship ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IArea? area = null;
    IArtist? artist = null;
    IReadOnlyList<string>? attributes = null;
    IReadOnlyDictionary<string, string>? attributeCredits = null;
    IReadOnlyDictionary<string, Guid>? attributeIds = null;
    IReadOnlyDictionary<string, string>? attributeValues = null;
    PartialDate? begin = null;
    string? direction = null;
    PartialDate? end = null;
    var ended = false;
    IEvent? @event = null;
    IInstrument? instrument = null;
    ILabel? label = null;
    int? orderingKey = null;
    IPlace? place = null;
    IRecording? recording = null;
    IRelease? release = null;
    IReleaseGroup? releaseGroup = null;
    ISeries? series = null;
    string? sourceCredit = null;
    string? targetCredit = null;
    Guid? target = null;
    EntityType? targetType = null;
    string? type = null;
    Guid? typeId = null;
    IUrl? url = null;
    IWork? work = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "area":
            area = reader.GetOptionalObject(AreaReader.Instance, options);
            break;
          case "artist":
            artist = reader.GetOptionalObject(ArtistReader.Instance, options);
            break;
          case "attributes":
            attributes = reader.ReadList<string>(options);
            break;
          case "attribute-credits":
            attributeCredits = reader.ReadDictionary<string>(options);
            break;
          case "attribute-ids":
            attributeIds = reader.ReadDictionary<Guid>(options);
            break;
          case "attribute-values":
            attributeValues = reader.ReadDictionary<string>(options);
            break;
          case "begin":
            begin = reader.GetOptionalObject(PartialDateReader.Instance, options);
            break;
          case "direction":
            direction = reader.GetString();
            break;
          case "end":
            end = reader.GetOptionalObject(PartialDateReader.Instance, options);
            break;
          case "ended":
            ended = reader.GetOptionalBoolean() ?? false;
            break;
          case "event":
            @event = reader.GetOptionalObject(EventReader.Instance, options);
            break;
          case "instrument":
            instrument = reader.GetOptionalObject(InstrumentReader.Instance, options);
            break;
          case "label":
            label = reader.GetOptionalObject(LabelReader.Instance, options);
            break;
          case "ordering-key":
            orderingKey = reader.GetOptionalInt32();
            break;
          case "place":
            place = reader.GetOptionalObject(PlaceReader.Instance, options);
            break;
          case "recording":
            recording = reader.GetOptionalObject(RecordingReader.Instance, options);
            break;
          case "release":
            release = reader.GetOptionalObject(ReleaseReader.Instance, options);
            break;
          case "release_group":
            releaseGroup = reader.GetOptionalObject(ReleaseGroupReader.Instance, options);
            break;
          case "series":
            series = reader.GetOptionalObject(SeriesReader.Instance, options);
            break;
          case "source-credit":
            sourceCredit = reader.GetString();
            break;
          case "target-credit":
            targetCredit = reader.GetString();
            break;
          case "target": // SEARCH-444
            target = reader.GetOptionalGuid();
            break;
          case "target-type": // SEARCH-444 prevents us from requiring this
            if (reader.TokenType != JsonTokenType.Null) {
              targetType = HelperMethods.ParseEntityType(reader.GetString());
              if (targetType == EntityType.Unknown) {
                goto default; // put the actual value in UnhandledProperties
              }
            }
            break;
          case "type":
            type = reader.GetString();
            break;
          case "type-id":
            typeId = reader.GetOptionalGuid();
            break;
          case "url":
            url = reader.GetOptionalObject(UrlReader.Instance, options);
            break;
          case "work":
            work = reader.GetOptionalObject(WorkReader.Instance, options);
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
    return new Relationship {
      Area = area,
      Artist = artist,
      AttributeCredits = attributeCredits,
      AttributeIds = attributeIds,
      AttributeValues = attributeValues,
      Attributes = attributes,
      Begin = begin,
      Direction = direction,
      End = end,
      Ended = ended,
      Event = @event,
      Instrument = instrument,
      Label = label,
      OrderingKey = orderingKey,
      Place = place,
      Recording = recording,
      Release = release,
      ReleaseGroup = releaseGroup,
      Series = series,
      SourceCredit = sourceCredit,
      TargetId = target,
      TargetCredit = targetCredit,
      TargetType = targetType,
      Type = type,
      TypeId = typeId,
      UnhandledProperties = rest,
      Url = url,
      Work = work,
    };
  }

}