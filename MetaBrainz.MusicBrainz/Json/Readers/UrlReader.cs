using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class UrlReader : ObjectReader<Url> {

  public static readonly UrlReader Instance = new UrlReader();

  protected override Url ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    Guid? id = null;
    IReadOnlyList<IRelationship>? relations = null;
    Uri? resource = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "id":
            id = reader.GetGuid();
            break;
          case "relation-list": { // SEARCH-444
            // The search server wraps the 'relations' list in:
            // "relation-list": [
            //   {
            //     "relations": [ ... ]
            //   }
            // ]
            // Assumption: relation-list will only ever contain a single wrapper object.
            if (reader.TokenType != JsonTokenType.StartArray)
              throw new JsonException("Expected start of list not found.");
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject)
              throw new JsonException("Expected start of object not found.");
            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName || reader.GetString() != "relations")
              throw new JsonException("Expected 'relations' property not found.");
            reader.Read();
            relations = reader.ReadList(RelationshipReader.Instance, options);
            reader.Read();
            if (reader.TokenType != JsonTokenType.EndObject)
              throw new JsonException("Expected end of object not found.");
            reader.Read();
            if (reader.TokenType != JsonTokenType.EndArray)
              throw new JsonException("Expected end of list not found.");
            break;
          }
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