using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MetaBrainz.MusicBrainz {

  // TODO: Move this to a shared MetaBrainz.Common library.
  internal static class JsonUtils {

    private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions {
      AllowTrailingCommas = false,
      IgnoreReadOnlyProperties = true,
      PropertyNameCaseInsensitive = false,
      WriteIndented = true,
      IgnoreNullValues = false,
    };

    public static T Deserialize<T>(string json) {
      return JsonSerializer.Deserialize<T>(json, SerializerOptions);
    }

    public static string Prettify(string json) {
      try {
        return JsonSerializer.Serialize(JsonDocument.Parse(json).RootElement, SerializerOptions);
      }
      catch {
        return json;
      }
    }

    public static Dictionary<string, object?>? Unwrap(Dictionary<string, object?>? dictionary) {
      if (dictionary == null)
        return null;
      Dictionary<string, object?>? unwrapped = null;
      foreach (var item in dictionary) {
        if (item.Value is JsonElement je) {
          if (unwrapped == null) // make a copy to modify
            unwrapped = new Dictionary<string, object?>(dictionary);
          unwrapped[item.Key] = Unwrap(je);
        }
      }
      return unwrapped ?? dictionary;
    }

    public static object? Unwrap(JsonElement je) {
      switch (je.ValueKind) {
        case JsonValueKind.Array: {
          var array = je.EnumerateArray().Select(Unwrap).ToArray();
          try {
            // if all elements are the same type (or null), try to map it to an array of that type
            Type? elementType = null;
            var hasNulls = false;
            foreach (var element in array) {
              if (element == null) {
                hasNulls = true;
                continue;
              }
              var t = element.GetType();
              // ignore nullability
              t = Nullable.GetUnderlyingType(t) ?? t;
              if (elementType == null)
                elementType = t;
              else if (elementType != t) {
                elementType = null;
                break;
              }
            }
            if (elementType != null) {
              if (elementType.IsValueType && hasNulls) // make it nullable
                elementType = typeof(Nullable<>).MakeGenericType(elementType);
              var len = array.Length;
              var typedArray = Array.CreateInstance(elementType, len);
              for (var i = 0; i < len; ++i)
                typedArray.SetValue(Convert.ChangeType(array[i], elementType), i);
              return typedArray;
            }
          }
          catch {
            // No worries, we still have the basic object array to return
          }
          return array;
        }
        case JsonValueKind.False:
          return false;
        case JsonValueKind.Null:
          return null;
        case JsonValueKind.Number: {
          if (je.TryGetInt64(out var intVal))
            return intVal;
          if (je.TryGetUInt64(out var uintVal))
            return uintVal;
          if (je.TryGetDecimal(out var decVal))
            return decVal;
          if (je.TryGetDouble(out var floatVal))
            return floatVal;
          break;
        }
        case JsonValueKind.Object:
          // TODO: How to map an object?
          break;
        case JsonValueKind.True:
          return true;
        case JsonValueKind.String: {
          // Note: NOT TryGetBytesFromBase64() because that has many false positives (e.g. most ISRC values).
          if (je.TryGetDateTimeOffset(out var dto))
            return dto;
          if (je.TryGetDateTime(out var dt))
            return dt;
          if (je.TryGetGuid(out var guid))
            return guid;
          var text = je.GetString();
          if (Uri.TryCreate(text, UriKind.Absolute, out var uri))
            return uri;
          // FIXME: Are the other "special" strings we should recognize?
          return text;
        }
        case JsonValueKind.Undefined:
          return null; // map 'no value' to null too
      }
      return je;
    }

  }

}
