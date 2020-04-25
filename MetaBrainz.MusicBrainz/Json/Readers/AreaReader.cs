using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers {

  internal sealed class AreaReader : ObjectReader<Area> {

    public static readonly AreaReader Instance = new AreaReader();

    protected override Area ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
      IReadOnlyList<IAlias>? aliases = null;
      string? annotation = null;
      string? disambiguation = null;
      IReadOnlyList<IGenre>? genres = null;
      Guid? id = null;
      IReadOnlyList<string>? iso31661Codes = null;
      IReadOnlyList<string>? iso31662Codes = null;
      IReadOnlyList<string>? iso31663Codes = null;
      ILifeSpan? lifeSpan = null;
      string? name = null;
      IReadOnlyList<IRelationship>? relations = null;
      string? sortName = null;
      IReadOnlyList<ITag>? tags = null;
      string? type = null;
      Guid? typeId = null;
      IReadOnlyList<IGenre>? userGenres = null;
      IReadOnlyList<ITag>? userTags = null;
      Dictionary<string, object?>? rest = null;
      while (reader.TokenType == JsonTokenType.PropertyName) {
        var prop = reader.GetString();
        try {
          reader.Read();
          switch (prop) {
            case "aliases":
              aliases = reader.ReadList(AliasReader.Instance, options);
              break;
            case "annotation":
              annotation = reader.GetString();
              break;
            case "disambiguation":
              disambiguation = reader.GetString();
              break;
            case "genres":
              genres = reader.ReadList(GenreReader.Instance, options);
              break;
            case "id":
              id = reader.GetGuid();
              break;
            case "iso-3166-1-codes":
              iso31661Codes = reader.ReadList<string>(options);
              break;
            case "iso-3166-2-codes":
              iso31662Codes = reader.ReadList<string>(options);
              break;
            case "iso-3166-3-codes":
              iso31663Codes = reader.ReadList<string>(options);
              break;
            case "life-span":
              lifeSpan = reader.GetObject(LifeSpanReader.Instance, options);
              break;
            case "name":
              name = reader.GetString();
              break;
            case "relation-list": { // SEARCH-444
              // The search server wraps the 'relations' list in:
              // "relation-list": [
              //   {
              //     "relations": [ ... ]
              //   }
              // ]
              // Assumption: it only persists the part-of relationship, so the list will only ever contain a single wrapper object.
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
            case "sort-name":
              sortName = reader.GetString();
              break;
            case "tags":
              tags = reader.ReadList(TagReader.Instance, options);
              break;
            case "type":
              type = reader.GetString();
              break;
            case "type-id":
              typeId = reader.GetOptionalGuid();
              break;
            case "user-genres":
              userGenres = reader.ReadList(GenreReader.Instance, options);
              break;
            case "user-tags":
              userTags = reader.ReadList(TagReader.Instance, options);
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
      return new Area(id.Value) {
        Aliases = aliases,
        Annotation = annotation,
        Disambiguation = disambiguation,
        Genres = genres,
        Iso31661Codes = iso31661Codes,
        Iso31662Codes = iso31662Codes,
        Iso31663Codes = iso31663Codes,
        LifeSpan = lifeSpan,
        Name = name,
        Relationships = relations,
        SortName = sortName,
        Tags = tags,
        Type = type,
        TypeId = typeId,
        UnhandledProperties = rest,
        UserGenres = userGenres,
        UserTags = userTags,
      };
    }

  }

}
