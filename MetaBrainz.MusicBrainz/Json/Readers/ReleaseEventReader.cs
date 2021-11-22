using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class ReleaseEventReader : ObjectReader<ReleaseEvent> {

  public static readonly ReleaseEventReader Instance = new();

  protected override ReleaseEvent ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IArea? area = null;
    PartialDate? date = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "area":
            area = reader.GetOptionalObject(AreaReader.Instance, options);
            break;
          case "date":
            date = reader.GetOptionalObject(PartialDateReader.Instance, options);
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
    return new ReleaseEvent {
      Area = area,
      Date = date,
      UnhandledProperties = rest,
    };
  }

}
