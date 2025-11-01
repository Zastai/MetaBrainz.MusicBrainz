using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class MultiUrlLookupResultReader : ObjectReader<MultiUrlLookupResult> {

  public static readonly MultiUrlLookupResultReader Instance = new();

  protected override MultiUrlLookupResult ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    int? count = null;
    int? offset = null;
    Dictionary<string, object?>? rest = null;
    IReadOnlyList<IUrl>? urls = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "url-count":
            count = reader.GetInt32();
            break;
          case "url-offset":
            offset = reader.GetInt32();
            break;
          case "urls":
            urls = reader.ReadList(UrlReader.Instance, options);
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
    return new MultiUrlLookupResult {
      Offset = offset ?? throw new MissingPropertyException("url-offset"),
      Results = urls ?? throw new MissingPropertyException("urls"),
      TotalResults = count ?? throw new MissingPropertyException("url-count"),
      UnhandledProperties = rest,
    };
  }

}
