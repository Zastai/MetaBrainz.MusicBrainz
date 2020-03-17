using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MetaBrainz.MusicBrainz.Objects {

  internal sealed class JsonAnythingConverter : JsonConverter<object?> {

    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      switch (reader.TokenType) {
        // Easy cases
        case JsonTokenType.False: return false;
        case JsonTokenType.True:  return true;
        case JsonTokenType.None:  return null;
        case JsonTokenType.Null:  return null;
        // Cases requiring further deduction
        case JsonTokenType.Number: {
          if (reader.TryGetInt64(out var intVal))
            return intVal;
          if (reader.TryGetUInt64(out var uintVal))
            return uintVal;
          if (reader.TryGetDecimal(out var decVal))
            return decVal;
          if (reader.TryGetDouble(out var floatVal))
            return floatVal;
          return reader.GetRawStringValue();
        }
        case JsonTokenType.String: {
          // Note: NOT TryGetBytesFromBase64() because that has many false positives (e.g. most ISRC values).
          if (reader.TryGetDateTimeOffset(out var dto))
            return dto;
          if (reader.TryGetDateTime(out var dt))
            return dt;
          if (reader.TryGetGuid(out var guid))
            return guid;
          var text = reader.GetString();
          if (Uri.TryCreate(text, UriKind.Absolute, out var uri))
            return uri;
          // FIXME: Are the other "special" strings we should recognize?
          return text;
        }
        case JsonTokenType.StartArray: {
          var elements = new List<object?>();
          while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndArray)
              break;
            elements.Add(this.Read(ref reader, typeToConvert, options));
          }
          try {
            // if all elements are the same type (or null), try to map it to an array of that type
            Type? elementType = null;
            var hasNulls = false;
            foreach (var element in elements) {
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
              var len = elements.Count;
              var typedArray = Array.CreateInstance(elementType, len);
              for (var i = 0; i < len; ++i)
                typedArray.SetValue(Convert.ChangeType(elements[i], elementType), i);
              return typedArray;
            }
          }
          catch {
            // No worries, we still have the basic object array to return
          }
          return elements.ToArray();
        }
        case JsonTokenType.StartObject: { // Use JsonElement
          using var document = JsonDocument.ParseValue(ref reader);
          return document.RootElement.Clone();
        }
        default:
          throw new JsonException($"Unexpected token type: {reader.TokenType}.");
      }
    }

    public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options) {
      throw new JsonException("Serialization not implemented.");
    }

  }

}
