using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MetaBrainz.MusicBrainz.Objects {

  // TODO: Move this to a shared MetaBrainz.Common library.
  internal sealed class JsonInterfaceListConverter<TInterface, TObject> : JsonConverter<IReadOnlyList<TInterface>?> where TInterface : class where TObject : class, TInterface {

    public override IReadOnlyList<TInterface>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      var objects = JsonSerializer.Deserialize<List<TObject>>(ref reader, options);
      var interfaces = objects?.Select(o => (TInterface) o).ToList();
      return interfaces;
    }

    public override void Write(Utf8JsonWriter writer, IReadOnlyList<TInterface>? interfaces, JsonSerializerOptions options) {
      if (interfaces == null)
        return;
      var objects = interfaces.Select(i => (TObject) i).ToList();
      JsonSerializer.Serialize(writer, objects, options);
    }

  }

}
