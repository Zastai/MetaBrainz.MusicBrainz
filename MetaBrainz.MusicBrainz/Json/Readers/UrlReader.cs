using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers {

  internal sealed class UrlReader : ObjectReader<Url> {

    public static readonly UrlReader Instance = new UrlReader();

    protected override Url ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
      Guid? id = null;
      IReadOnlyList<IRelationship>? relations = null;
      Uri? resource = null;
      Dictionary<string, object?>? rest = null;
      while (reader.TokenType == JsonTokenType.PropertyName) {
        var prop = reader.GetString();
        try {
          reader.Read();
          switch (prop) {
            case "id":
              id = reader.GetGuid();
              break;
            case "relations":
              relations = reader.ReadList(RelationshipReader.Instance, options);
              break;
            case "resource":
              resource = reader.GetOptionalUri();
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
      if (!id.HasValue)
        throw new JsonException("Expected property 'id' not found or null.");
      if (resource == null)
        throw new JsonException("Expected property 'resource' not found or null.");
      return new Url(id.Value, resource) {
        Relationships = relations,
        UnhandledProperties = rest,
      };
    }

  }

}
