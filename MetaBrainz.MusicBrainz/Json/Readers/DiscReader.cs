using System;
using System.Collections.Generic;
using System.Text.Json;

using MetaBrainz.Common.Json;
using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json.Readers;

internal sealed class DiscReader : ObjectReader<Disc> {

  public static readonly DiscReader Instance = new();

  protected override Disc ReadObjectContents(ref Utf8JsonReader reader, JsonSerializerOptions options) {
    string? id = null;
    int? offsetCount = null;
    IReadOnlyList<int>? offsets = null;
    IReadOnlyList<IRelease>? releases = null;
    int? sectors = null;
    Dictionary<string, object?>? rest = null;
    while (reader.TokenType == JsonTokenType.PropertyName) {
      var prop = reader.GetPropertyName();
      try {
        reader.Read();
        switch (prop) {
          case "id":
            id = reader.GetString();
            break;
          case "offset-count":
            offsetCount = reader.GetOptionalInt32();
            break;
          case "offsets":
            offsets = reader.ReadList<int>(options);
            break;
          case "releases": // FIXME: Is this ever present outside of a DiscID lookup (which is handled by a different reader)?
            releases = reader.ReadList(ReleaseReader.Instance, options);
            break;
          case "sectors":
            sectors = reader.GetOptionalInt32();
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
    if (offsetCount is not null || offsets is not null) {
      var reported = offsetCount.GetValueOrDefault();
      var actual = offsets?.Count ?? 0;
      if (reported != actual) {
        throw new JsonException($"The number of offsets ({actual}) does not match the reported offset count ({reported}).");
      }
    }
    return new Disc {
      Id = id ?? throw new MissingPropertyException("id"),
      Offsets = offsets ?? throw new MissingPropertyException("offsets"),
      Releases = releases,
      Sectors = sectors ?? throw new MissingPropertyException("sectors"),
      UnhandledProperties = rest,
    };
  }

}
