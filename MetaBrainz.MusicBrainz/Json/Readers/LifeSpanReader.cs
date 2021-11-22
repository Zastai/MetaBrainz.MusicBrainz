using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class LifeSpanReader : ObjectReader<LifeSpan> {

  public static readonly LifeSpanReader Instance = new LifeSpanReader();

  protected override LifeSpan ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    PartialDate? begin = null;
    PartialDate? end = null;
    var ended = false;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "begin":
            begin = reader.GetOptionalObject(PartialDateReader.Instance, options);
            break;
          case "end":
            end = reader.GetOptionalObject(PartialDateReader.Instance, options);
            break;
          case "ended":
            ended = reader.GetOptionalBoolean() ?? false;
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
    return new LifeSpan {
      Begin = begin,
      End = end,
      Ended = ended,
      UnhandledProperties = rest,
    };
  }

}