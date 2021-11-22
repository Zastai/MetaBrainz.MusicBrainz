using System;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class SearchResultReader<T> : JsonReader<SearchResult<T>> where T : JsonBasedObject {

  public static readonly SearchResultReader<T> Instance = new();

  public override SearchResult<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
    var item = reader.GetObject<T>(options);
    byte score = 0;
    if (item.UnhandledProperties != null) {
      if (item.UnhandledProperties.TryGetValue("score", out var rawScore)) {
        if (rawScore is int intValue) {
          score = (byte) intValue;
          item.UnhandledProperties.Remove("score");
          if (item.UnhandledProperties.Count == 0) {
            item.UnhandledProperties = null;
          }
        }
      }
    }
    return new SearchResult<T>(item, score);
  }

}
