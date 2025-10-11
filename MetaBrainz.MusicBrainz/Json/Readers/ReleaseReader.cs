using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class ReleaseReader : ObjectReader<Release> {

  public static readonly ReleaseReader Instance = new();

  protected override Release ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    IReadOnlyList<IAlias>? aliases = null;
    string? annotation = null;
    IReadOnlyList<INameCredit>? artistCredit = null;
    string? asin = null;
    string? barcode = null;
    IReadOnlyList<ICollection>? collections = null;
    string? country = null;
    ICoverArtArchive? coverArtArchive = null;
    PartialDate? date = null;
    string? disambiguation = null;
    IReadOnlyList<IGenre>? genres = null;
    Guid? id = null;
    IReadOnlyList<ILabelInfo>? labelInfo = null;
    IReadOnlyList<IMedium>? media = null;
    string? packaging = null;
    Guid? packagingId = null;
    string? quality = null;
    IReadOnlyList<IRelationship>? relations = null;
    IReadOnlyList<IReleaseEvent>? releaseEvents = null;
    IReleaseGroup? releaseGroup = null;
    string? status = null;
    Guid? statusId = null;
    IReadOnlyList<ITag>? tags = null;
    ITextRepresentation? textRepresentation = null;
    string? title = null;
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
          case "artist-credit":
            artistCredit = reader.ReadList(NameCreditReader.Instance, options);
            break;
          case "asin":
            asin = reader.GetString();
            break;
          case "barcode":
            barcode = reader.GetString();
            break;
          case "collections":
            collections = reader.ReadList(CollectionReader.Instance, options);
            break;
          case "country":
            country = reader.GetString();
            break;
          case "cover-art-archive":
            coverArtArchive = reader.GetOptionalObject(CoverArtArchiveReader.Instance, options);
            break;
          case "date":
            date = reader.GetOptionalObject(PartialDateReader.Instance, options);
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
          case "label-info":
            labelInfo = reader.ReadList(LabelInfoReader.Instance, options);
            break;
          case "media":
            media = reader.ReadList(MediumReader.Instance, options);
            break;
          case "packaging":
            packaging = reader.GetString();
            break;
          case "packaging-id":
            packagingId = reader.GetOptionalGuid();
            break;
          case "quality":
            quality = reader.GetString();
            break;
          case "relations":
            relations = reader.ReadList(RelationshipReader.Instance, options);
            break;
          case "release-events":
            releaseEvents = reader.ReadList(ReleaseEventReader.Instance, options);
            break;
          case "release-group":
            releaseGroup = reader.GetOptionalObject(ReleaseGroupReader.Instance, options);
            break;
          case "status":
            status = reader.GetString();
            break;
          case "status-id":
            statusId = reader.GetOptionalGuid();
            break;
          case "tags":
            tags = reader.ReadList(TagReader.Instance, options);
            break;
          case "text-representation":
            textRepresentation = reader.GetOptionalObject(TextRepresentationReader.Instance, options);
            break;
          case "title":
            title = reader.GetString();
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
    return new Release {
      Aliases = aliases,
      Annotation = annotation,
      ArtistCredit = artistCredit,
      Asin = asin,
      Barcode = barcode,
      Collections = collections,
      Country = country,
      CoverArtArchive = coverArtArchive,
      Date = date,
      Disambiguation = disambiguation,
      Genres = genres,
      Id = id ?? throw new MissingPropertyException("id"),
      LabelInfo = labelInfo,
      Media = media,
      Packaging = packaging,
      PackagingId = packagingId,
      Quality = quality,
      Relationships = relations,
      ReleaseEvents = releaseEvents,
      ReleaseGroup = releaseGroup,
      Status = status,
      StatusId = statusId,
      Tags = tags,
      TextRepresentation = textRepresentation,
      Title = title,
      UnhandledProperties = rest,
      UserGenres = userGenres,
      UserTags = userTags,
    };
  }

}
