using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class SeriesReader : ObjectReader<Series> {

  public static readonly SeriesReader Instance = new();

  protected override Series ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IReadOnlyList<IAlias>? aliases = null;
    string? annotation = null;
    string? disambiguation = null;
    IReadOnlyList<IGenre>? genres = null;
    Guid? id = null;
    string? name = null;
    IReadOnlyList<IRelationship>? relations = null;
    IReadOnlyList<ITag>? tags = null;
    string? type = null;
    Guid? typeId = null;
    IReadOnlyList<IGenre>? userGenres = null;
    IReadOnlyList<ITag>? userTags = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
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
          case "name":
            name = reader.GetString();
            break;
          case "relations":
            relations = reader.ReadList(RelationshipReader.Instance, options);
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
    if (id is null) {
      throw new JsonException("Expected property 'id' not found or null.");
    }
    return new Series(id.Value) {
      Aliases = aliases,
      Annotation = annotation,
      Disambiguation = disambiguation,
      Genres = genres,
      Name = name,
      Relationships = relations,
      Tags = tags,
      Type = type,
      TypeId = typeId,
      UnhandledProperties = rest,
      UserGenres = userGenres,
      UserTags = userTags,
    };
  }

}
