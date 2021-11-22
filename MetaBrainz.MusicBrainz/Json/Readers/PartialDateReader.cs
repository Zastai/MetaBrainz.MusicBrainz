using System;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class PartialDateReader : JsonReader<PartialDate> {

  public static readonly PartialDateReader Instance = new PartialDateReader();

  public override PartialDate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
    if (reader.TokenType == JsonTokenType.Number) { // Consider this to be an unquoted string, i.e. a year value.
      if (reader.TryGetInt16(out var year))
        return new PartialDate(year);
    }
    else if (reader.TokenType == JsonTokenType.String) {
      var text = reader.GetString();
      if (text != null)
        return new PartialDate(text);
    }
    throw new JsonException($"Token ({reader.TokenType}: {reader.GetRawStringValue()}) cannot be converted to a (partial) date.");
  }

}