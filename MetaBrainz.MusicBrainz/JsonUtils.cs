using System;
using System.Text;
using System.Text.Json;

namespace MetaBrainz.MusicBrainz {

  // TODO: Move this to a shared MetaBrainz.Common library.
  internal static class JsonUtils {

    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions {
      // @formatter:off
      AllowTrailingCommas         = false,
      IgnoreNullValues            = false,
      IgnoreReadOnlyProperties    = true,
      PropertyNameCaseInsensitive = false,
      WriteIndented               = true,
      // @formatter:on
    };

    private static string DecodeUtf8(ReadOnlySpan<byte> bytes) {
#if NETSTD_GE_2_1 || NETCORE_GE_2_1
      return Encoding.UTF8.GetString(bytes);
#else
      return Encoding.UTF8.GetString(bytes.ToArray());
#endif
    }

    public static T Deserialize<T>(string json) {
      return JsonSerializer.Deserialize<T>(json, Options);
    }

    public static T Deserialize<T>(string json, JsonSerializerOptions options) {
      return JsonSerializer.Deserialize<T>(json, options);
    }

    public static string GetRawStringValue(this ref Utf8JsonReader reader) {
      var value = "";
      if (reader.HasValueSequence) {
        foreach (var memory in reader.ValueSequence)
          value += DecodeUtf8(memory.Span);
      }
      else
        value = DecodeUtf8(reader.ValueSpan);
      return value;
    }

    public static string Prettify(string json) {
      try {
        return JsonSerializer.Serialize(JsonDocument.Parse(json).RootElement, Options);
      }
      catch {
        return json;
      }
    }

  }

}
