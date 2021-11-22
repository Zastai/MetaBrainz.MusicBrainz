using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class ArtistReader : ObjectReader<Artist> {

  public static readonly ArtistReader Instance = new ArtistReader();

  protected override Artist ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IReadOnlyList<IAlias>? aliases = null;
    string? annotation = null;
    IArea? area = null;
    IArea? beginArea = null;
    string? country = null;
    string? disambiguation = null;
    IArea? endArea = null;
    string? gender = null;
    Guid? genderId = null;
    IReadOnlyList<IGenre>? genres = null;
    Guid? id = null;
    IReadOnlyList<string>? ipis = null;
    IReadOnlyList<string>? isnis = null;
    ILifeSpan? lifeSpan = null;
    string? name = null;
    IRating? rating = null;
    IReadOnlyList<IRecording>? recordings = null;
    IReadOnlyList<IRelationship>? relations = null;
    IReadOnlyList<IRelease>? releases = null;
    IReadOnlyList<IReleaseGroup>? releaseGroups = null;
    string? sortName = null;
    IReadOnlyList<ITag>? tags = null;
    string? type = null;
    Guid? typeId = null;
    IReadOnlyList<IGenre>? userGenres = null;
    IRating? userRating = null;
    IReadOnlyList<ITag>? userTags = null;
    IReadOnlyList<IWork>? works = null;
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
          case "begin_area": // MBS-10072
          case "begin-area":
            beginArea = reader.GetOptionalObject(AreaReader.Instance, options);
            break;
          case "country":
            country = reader.GetString();
            break;
          case "disambiguation":
            disambiguation = reader.GetString();
            break;
          case "end_area": // MBS-10072
          case "end-area":
            endArea = reader.GetOptionalObject(AreaReader.Instance, options);
            break;
          case "gender":
            gender = reader.GetString();
            break;
          case "gender-id":
            genderId = reader.GetOptionalGuid();
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
          case "life-span":
            lifeSpan = reader.GetObject(LifeSpanReader.Instance, options);
            break;
          case "name":
            name = reader.GetString();
            break;
          case "rating":
            rating = reader.GetObject(RatingReader.Instance, options);
            break;
          case "recordings":
            recordings = reader.ReadList(RecordingReader.Instance, options);
            break;
          case "relations":
            relations = reader.ReadList(RelationshipReader.Instance, options);
            break;
          case "releases":
            releases = reader.ReadList(ReleaseReader.Instance, options);
            break;
          case "release-groups":
            releaseGroups = reader.ReadList(ReleaseGroupReader.Instance, options);
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
          case "works":
            works = reader.ReadList(WorkReader.Instance, options);
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
    return new Artist(id.Value) {
      Aliases = aliases,
      Annotation = annotation,
      Area = area,
      BeginArea = beginArea,
      Country = country,
      Disambiguation = disambiguation,
      EndArea = endArea,
      Gender = gender,
      GenderId = genderId,
      Genres = genres,
      Ipis = ipis,
      Isnis = isnis,
      LifeSpan = lifeSpan,
      Name = name,
      Rating = rating,
      Recordings = recordings,
      Relationships = relations,
      Releases = releases,
      ReleaseGroups = releaseGroups,
      SortName = sortName,
      Tags = tags,
      Type = type,
      TypeId = typeId,
      UnhandledProperties = rest,
      UserGenres = userGenres,
      UserRating = userRating,
      UserTags = userTags,
      Works = works
    };
  }

}