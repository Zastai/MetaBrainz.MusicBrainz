using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class LabelReader : ObjectReader<Label> {

  public static readonly LabelReader Instance = new();

  protected override Label ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IReadOnlyList<IAlias>? aliases = null;
    string? annotation = null;
    IArea? area = null;
    string? country = null;
    string? disambiguation = null;
    IReadOnlyList<IGenre>? genres = null;
    Guid? id = null;
    IReadOnlyList<string>? ipis = null;
    IReadOnlyList<string>? isnis = null;
    ILifeSpan? lifeSpan = null;
    string? name = null;
    IRating? rating = null;
    int? labelCode = null;
    IReadOnlyList<IRelationship>? relations = null;
    IReadOnlyList<IRelease>? releases = null;
    string? sortName = null;
    IReadOnlyList<ITag>? tags = null;
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
          case "area":
            area = reader.GetOptionalObject(AreaReader.Instance, options);
            break;
          case "country":
            country = reader.GetString();
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
          case "ipis":
            ipis = reader.ReadList<string>(options);
            break;
          case "isnis":
            isnis = reader.ReadList<string>(options);
            break;
          case "label-code":
            labelCode = reader.GetOptionalInt32();
            break;
          case "life-span":
            lifeSpan = reader.GetObject(LifeSpanReader.Instance, options);
            break;
          case "name":
            name = reader.GetString();
            break;
          case "rating":
            rating = reader.GetObject(RatingReader.Instance, options);
            break;
          case "relations":
            relations = reader.ReadList(RelationshipReader.Instance, options);
            break;
          case "releases":
            releases = reader.ReadList(ReleaseReader.Instance, options);
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
    return new Label(id.Value) {
      Aliases = aliases,
      Annotation = annotation,
      Area = area,
      Country = country,
      Disambiguation = disambiguation,
      Genres = genres,
      Ipis = ipis,
      Isnis = isnis,
      LabelCode = labelCode,
      LifeSpan = lifeSpan,
      Name = name,
      Rating = rating,
      Relationships = relations,
      Releases = releases,
      SortName = sortName,
      Tags = tags,
      Type = type,
      TypeId = typeId,
      UnhandledProperties = rest,
      UserGenres = userGenres,
      UserRating = userRating,
      UserTags = userTags,
    };
  }

}