using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MetaBrainz.MusicBrainz.Objects {

  // TODO: Move this to a shared MetaBrainz.Common library.
  internal sealed class JsonInterfaceConverter<TInterface, TObject> : JsonConverter<TInterface?> where TInterface : class where TObject : class, TInterface {

    public override TInterface? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
      return JsonSerializer.Deserialize<TObject?>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, TInterface? value, JsonSerializerOptions options) {
      JsonSerializer.Serialize(writer, (TObject?) value, options);
    }

  }

}
