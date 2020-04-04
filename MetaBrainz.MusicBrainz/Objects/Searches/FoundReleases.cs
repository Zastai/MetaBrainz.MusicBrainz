using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundReleases : SearchResults<IFoundRelease> {

    public FoundReleases(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "release", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundRelease> Results => this.CurrentResult?.Releases ?? Array.Empty<IFoundRelease>();

    /// <summary>
    /// Custom converter to work around <a href="https://tickets.metabrainz.org/browse/SEARCH-579">SEARCH-579</a>.<br/>
    /// This fixes <a href="https://github.com/Zastai/MetaBrainz.MusicBrainz/issues/1">Issue #1</a> by checking whether the
    /// 'packaging' property is a string or an object, and handling it accordingly.
    /// </summary>
    public sealed class WorkaroundForSearchBug579 : JsonConverter<Release[]> {

      private sealed class ObjectConverter : JsonConverter<Release> {

        public override Release Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
          if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException($"Expected the start of a Release object, but got a {reader.TokenType} instead (at offset {reader.TokenStartIndex}).");
          var r = new Release();
          var complete = false;
          while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndObject) {
              complete = true;
              break;
            }
            if (reader.TokenType != JsonTokenType.PropertyName)
              throw new JsonException($"Expected a property of a Release object, but got a {reader.TokenType} instead (at offset {reader.TokenStartIndex}).");
            var property = reader.GetRawStringValue();
            if (!reader.Read())
              throw new JsonException($"Expected value for the '{property}' property of Release object not encountered.");
            switch (property) {
              case "aliases":
                r.Aliases = JsonSerializer.Deserialize<IReadOnlyList<IAlias>?>(ref reader, options);
                break;
              case "annotation":
                r.Annotation = reader.GetString();
                break;
              case "artist-credit":
                r.ArtistCredit = JsonSerializer.Deserialize<IReadOnlyList<INameCredit>?>(ref reader, options);
                break;
              case "asin":
                r.Asin = reader.GetString();
                break;
              case "barcode":
                r.BarCode = reader.GetString();
                break;
              case "collections":
                r.Collections = JsonSerializer.Deserialize<IReadOnlyList<ICollection>?>(ref reader, options);
                break;
              case "country":
                r.Country = reader.GetString();
                break;
              case "cover-art-archive":
                r.CoverArtArchive = JsonSerializer.Deserialize<ICoverArtArchive?>(ref reader, options);
                break;
              case "date":
                r.Date = JsonSerializer.Deserialize<PartialDate?>(ref reader, options);
                break;
              case "disambiguation":
                r.Disambiguation = reader.GetString();
                break;
              case "genres":
                r.Genres = JsonSerializer.Deserialize<IReadOnlyList<ITag>?>(ref reader, options);
                break;
              case "id":
                r.MbId = reader.GetGuid();
                break;
              case "label-info":
                r.LabelInfo = JsonSerializer.Deserialize<IReadOnlyList<ILabelInfo>?>(ref reader, options);
                break;
              case "media":
                r.Media = JsonSerializer.Deserialize<IReadOnlyList<IMedium>?>(ref reader, options);
                break;
              case "packaging":
                if (reader.TokenType == JsonTokenType.String) // packaging: "foo"
                  r.Packaging = reader.GetRawStringValue();
                else if (reader.TokenType == JsonTokenType.StartObject) { // packaging: { "name": "foo", id: "guid" }
                  while (reader.Read()) {
                    if (reader.TokenType == JsonTokenType.EndObject)
                      break;
                    if (reader.TokenType != JsonTokenType.PropertyName)
                      throw new JsonException($"Expected a property of a Release object's 'packaging' field, but got a {reader.TokenType} instead (at offset {reader.TokenStartIndex}).");
                    var subprop = reader.GetRawStringValue();
                    if (!reader.Read())
                      throw new JsonException($"Expected value for the '{subprop}' property of Release object's 'packaging' field not encountered.");
                    if (subprop == "name")
                      r.Packaging = reader.GetString();
                    else if (subprop == "id")
                      r.PackagingId = reader.GetGuid();
                    else {
                      r.UnhandledProperties ??= new Dictionary<string, object?>();
                      r.UnhandledProperties.Add(property + ":" + subprop, JsonSerializer.Deserialize(ref reader, typeof(object), options));
                    }
                  }
                }
                else if (reader.TokenType != JsonTokenType.Null)
                  throw new JsonException($"Unsupported value (of type {reader.TokenType}) found for the 'packaging' property of a Release object at offset {reader.TokenStartIndex}.");
                break;
              case "packaging-id":
                r.PackagingId = reader.GetGuid();
                break;
              case "quality":
                r.Quality = reader.GetString();
                break;
              case "relations":
                r.Relationships = JsonSerializer.Deserialize<IReadOnlyList<IRelationship>?>(ref reader, options);
                break;
              case "release-events":
                r.ReleaseEvents = JsonSerializer.Deserialize<IReadOnlyList<IReleaseEvent>?>(ref reader, options);
                break;
              case "release-group":
                r.ReleaseGroup = JsonSerializer.Deserialize<IReleaseGroup?>(ref reader, options);
                break;
              case "score":
                r.SearchScore = reader.GetByte();
                break;
              case "status":
                r.Status = reader.GetString();
                break;
              case "status-id":
                r.StatusId = reader.GetGuid();
                break;
              case "tags":
                r.Tags = JsonSerializer.Deserialize<IReadOnlyList<ITag>?>(ref reader, options);
                break;
              case "text-representation":
                r.TextRepresentation = JsonSerializer.Deserialize<ITextRepresentation?>(ref reader, options);
                break;
              case "title":
                r.Title = reader.GetString();
                break;
              case "user-genres":
                r.UserGenres = JsonSerializer.Deserialize<IReadOnlyList<IUserTag>?>(ref reader, options);
                break;
              case "user-tags":
                r.UserTags = JsonSerializer.Deserialize<IReadOnlyList<IUserTag>?>(ref reader, options);
                break;
              default:
                r.UnhandledProperties ??= new Dictionary<string, object?>();
                r.UnhandledProperties.Add(property, JsonSerializer.Deserialize(ref reader, typeof(object), options));
                break;
            }
          }
          if (!complete)
            throw new JsonException("Expected end of Release object not encountered.");
          return r;
        }

        public override void Write(Utf8JsonWriter writer, Release value, JsonSerializerOptions options) {
          throw new NotSupportedException("This converter only handles deserialization.");
        }

      }

      public override Release[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        if (reader.TokenType != JsonTokenType.StartArray)
          throw new JsonException($"Expected the start of a Release array, but got a {reader.TokenType} instead (at offset {reader.TokenStartIndex}).");
        var releases = new List<Release>();
        var complete = false;
        var oc = new ObjectConverter();
        while (reader.Read()) {
          if (reader.TokenType == JsonTokenType.EndArray) {
            complete = true;
            break;
          }
          releases.Add(oc.Read(ref reader, typeof(Release), options));
        }
        if (!complete)
          throw new JsonException("Expected end of Release array not encountered.");
        return releases.ToArray();
      }

      public override void Write(Utf8JsonWriter writer, Release[] value, JsonSerializerOptions options) {
        throw new NotSupportedException("This converter only handles deserialization.");
      }

    }

  }

}
