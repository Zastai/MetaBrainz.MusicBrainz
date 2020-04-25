using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers {

  internal sealed class AnnotationReader : ObjectReader<Annotation> {

    public static readonly AnnotationReader Instance = new AnnotationReader();

    protected override Annotation ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
      Guid? entity = null;
      string? name = null;
      string? text = null;
      EntityType? type = null;
      Dictionary<string, object?>? rest = null;
      while (reader.TokenType == JsonTokenType.PropertyName) {
        var prop = reader.GetString();
        try {
          reader.Read();
          switch (prop) {
            case "entity":
              entity = reader.GetOptionalGuid();
              break;
            case "name":
              name = reader.GetString();
              break;
            case "text":
              text = reader.GetString();
              break;
            case "type":
              type = HelperMethods.ParseEntityType(reader.GetString());
              if (type == EntityType.Unknown)
                goto default; // put the actual value in UnhandledProperties
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
      return new Annotation {
        Entity = entity,
        Name = name,
        Text = text,
        Type = type,
        UnhandledProperties = rest,
      };
    }

  }

}
