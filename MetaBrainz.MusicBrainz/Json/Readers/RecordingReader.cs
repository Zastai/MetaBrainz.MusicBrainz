using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class RecordingReader : ObjectReader<Recording> {

  public static readonly RecordingReader Instance = new();

  protected override Recording ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IReadOnlyList<IAlias>? aliases = null;
    string? annotation = null;
    IReadOnlyList<INameCredit>? artistCredit = null;
    string? disambiguation = null;
    PartialDate? firstReleaseDate = null;
    IReadOnlyList<IGenre>? genres = null;
    Guid? id = null;
    IReadOnlyList<string>? isrcs = null;
    TimeSpan? length = null;
    IRating? rating = null;
    IReadOnlyList<IRelationship>? relations = null;
    IReadOnlyList<IRelease>? releases = null;
    IReadOnlyList<ITag>? tags = null;
    string? title = null;
    IReadOnlyList<IGenre>? userGenres = null;
    IRating? userRating = null;
    IReadOnlyList<ITag>? userTags = null;
    var video = false;
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
          case "artist-credit":
            artistCredit = reader.ReadList(NameCreditReader.Instance, options);
            break;
          case "disambiguation":
            disambiguation = reader.GetString();
            break;
          case "first-release-date":
            firstReleaseDate = reader.GetOptionalObject(PartialDateReader.Instance, options);
            break;
          case "genres":
            genres = reader.ReadList(GenreReader.Instance, options);
            break;
          case "id":
            id = reader.GetGuid();
            break;
          case "isrcs":
            isrcs = reader.ReadList<string>(options);
            break;
          case "length":
            length = reader.GetOptionalTimeSpanFromMilliseconds();
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
          case "tags":
            tags = reader.ReadList(TagReader.Instance, options);
            break;
          case "title":
            title = reader.GetString();
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
          case "video":
            video = reader.GetOptionalBoolean() ?? false;
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
    return new Recording {
      Aliases = aliases ?? [],
      Annotation = annotation ?? "",
      ArtistCredit = artistCredit ?? [],
      Disambiguation = disambiguation ?? "",
      FirstReleaseDate = firstReleaseDate,
      Genres = genres ?? [],
      Id = id ?? throw new MissingPropertyException("id"),
      Isrcs = isrcs ?? [],
      Length = length,
      Rating = rating,
      Relationships = relations ?? [],
      Releases = releases ?? [],
      Tags = tags ?? [],
      Title = title ?? "",
      UnhandledProperties = rest,
      UserGenres = userGenres ?? [],
      UserRating = userRating,
      UserTags = userTags ?? [],
      Video = video
    };
  }

}
