using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class GenreReader : ObjectReader<Genre> {

  public static readonly GenreReader Instance = new GenreReader();

  protected override Genre ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    int? count = null;
    string? disambiguation = null;
    Guid? id = null;
    string? name = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "count":
            count = reader.GetInt32();
            break;
          case "disambiguation":
            disambiguation = reader.GetString();
            break;
          case "id":
            id = reader.GetGuid();
            break;
          case "name":
            name = reader.GetString();
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
    if (id == null)
      throw new JsonException("Expected genre id not found or null.");
    if (name == null)
      throw new JsonException("Expected genre name not found or null.");
    return new Genre(id.Value, name) {
      Disambiguation = disambiguation,
      UnhandledProperties = rest,
      VoteCount = count,
    };
  }

}