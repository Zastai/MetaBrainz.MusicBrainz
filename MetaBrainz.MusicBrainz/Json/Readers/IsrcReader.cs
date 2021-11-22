using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers; 

internal sealed class IsrcReader : ObjectReader<Isrc> {

  public static readonly IsrcReader Instance = new();

  protected override Isrc ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    string? isrc = null;
    IReadOnlyList<IRecording>? recordings = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "isrc":
            isrc = reader.GetString();
            break;
          case "recordings":
            recordings = reader.ReadList(RecordingReader.Instance, options);
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
    if (isrc == null) {
      throw new JsonException("Expected property 'isrc' not found or null.");
    }
    if (recordings == null) {
      throw new JsonException("Expected property 'recordings' not found or null.");
    }
    return new Isrc(isrc, recordings) {
      UnhandledProperties = rest
    };
  }

}