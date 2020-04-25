using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers {

  internal sealed class WorkAttributeReader : ObjectReader<WorkAttribute> {

    public static readonly WorkAttributeReader Instance = new WorkAttributeReader();

    protected override WorkAttribute ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
      string? type = null;
      Guid? typeId = null;
      string? value = null;
      Guid? valueId = null;
      Dictionary<string, object?>? rest = null;
      while (reader.TokenType == JsonTokenType.PropertyName) {
        var prop = reader.GetString();
        try {
          reader.Read();
          switch (prop) {
            case "type":
              type = reader.GetString();
              break;
            case "type-id":
              typeId = reader.GetOptionalGuid();
              break;
            case "value":
              value = reader.GetString();
              break;
            case "value-id":
              valueId = reader.GetOptionalGuid();
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
      return new WorkAttribute {
        Type = type,
        TypeId = typeId,
        UnhandledProperties = rest,
        Value = value,
        ValueId = valueId,
      };
    }

  }

}
