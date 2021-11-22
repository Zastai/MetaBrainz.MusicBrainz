using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class WorkReader : ObjectReader<Work> {

  public static readonly WorkReader Instance = new();

  protected override Work ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IReadOnlyList<IAlias>? aliases = null;
    string? annotation = null;
    IReadOnlyList<IWorkAttribute>? attributes = null;
    string? disambiguation = null;
    IReadOnlyList<IGenre>? genres = null;
    Guid? id = null;
    IReadOnlyList<string>? iswcs = null;
    string? language = null;
    IReadOnlyList<string>? languages = null;
    IRating? rating = null;
    IReadOnlyList<IRelationship>? relations = null;
    IReadOnlyList<ITag>? tags = null;
    string? title = null;
    string? type = null;
    Guid? typeId = null;
    IReadOnlyList<IGenre>? userGenres = null;
    IRating? userRating = null;
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
          case "attributes":
            attributes = reader.ReadList(WorkAttributeReader.Instance, options);
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
          case "iswcs":
            iswcs = reader.ReadList<string>(options);
            break;
          case "language":
            language = reader.GetString();
            break;
          case "languages":
            languages = reader.ReadList<string>(options);
            break;
          case "rating":
            rating = reader.GetObject(RatingReader.Instance, options);
            break;
          case "relations":
            relations = reader.ReadList(RelationshipReader.Instance, options);
            break;
          case "tags":
            tags = reader.ReadList(TagReader.Instance, options);
            break;
          case "title":
            title = reader.GetString();
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
          case "user-rating":
            userRating = reader.GetObject(RatingReader.Instance, options);
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
    if (!id.HasValue) {
      throw new JsonException("Expected property 'id' not found or null.");
    }
    return new Work(id.Value) {
      Aliases = aliases,
      Annotation = annotation,
      Attributes = attributes,
      Disambiguation = disambiguation,
      Genres = genres,
      Iswcs = iswcs,
      Language = language,
      Languages = languages,
      Rating = rating,
      Relationships = relations,
      Tags = tags,
      Title = title,
      Type = type,
      TypeId = typeId,
      UnhandledProperties = rest,
      UserGenres = userGenres,
      UserRating = userRating,
      UserTags = userTags,
    };
  }

}