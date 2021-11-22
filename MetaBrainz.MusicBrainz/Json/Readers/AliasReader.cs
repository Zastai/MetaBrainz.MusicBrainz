using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class AliasReader : ObjectReader<Alias> {

  public static readonly AliasReader Instance = new AliasReader();

  protected override Alias ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    PartialDate? begin = null;
    PartialDate? end = null;
    var ended = false;
    string? locale = null;
    string? name = null;
    string? sortName = null;
    var primary = false;
    string? type = null;
    Guid? typeId = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "begin-date": // SEARCH-607
          case "begin":
            begin = reader.GetOptionalObject(PartialDateReader.Instance, options);
            break;
          case "end-date": // SEARCH-607
          case "end":
            end = reader.GetOptionalObject(PartialDateReader.Instance, options);
            break;
          case "ended":
            ended = reader.GetOptionalBoolean() ?? false;
            break;
          case "locale":
            locale = reader.GetString();
            break;
          case "name":
            name = reader.GetString();
            break;
          case "primary":
            primary = reader.GetOptionalBoolean() ?? false;
            break;
          case "sort-name":
            sortName = reader.GetString();
            break;
          case "type":
            type = reader.GetString();
            break;
          case "type-id":
            typeId = reader.GetOptionalGuid();
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
    if (name == null)
      throw new JsonException("Expected name not found or null.");
    return new Alias(name, primary) {
      Begin = begin,
      End = end,
      Ended = ended,
      Locale = locale,
      SortName = sortName,
      Type = type,
      TypeId = typeId,
      UnhandledProperties = rest,
    };
  }

}