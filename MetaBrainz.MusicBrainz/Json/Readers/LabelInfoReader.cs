using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class LabelInfoReader : ObjectReader<LabelInfo> {

  public static readonly LabelInfoReader Instance = new LabelInfoReader();

  protected override LabelInfo ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    string? catalogNumber = null;
    ILabel? label = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "catalog-number":
            catalogNumber = reader.GetString();
            break;
          case "label":
            label = reader.GetOptionalObject(LabelReader.Instance, options);
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
    return new LabelInfo {
      CatalogNumber = catalogNumber,
      Label = label,
      UnhandledProperties = rest,
    };
  }

}