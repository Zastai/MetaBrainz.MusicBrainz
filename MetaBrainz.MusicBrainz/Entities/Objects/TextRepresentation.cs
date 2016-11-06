using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class TextRepresentation : ITextRepresentation {

    [JsonProperty("language", Required = Required.AllowNull)]
    public string Language { get; private set; }

    [JsonProperty("script", Required = Required.AllowNull)]
    public string Script { get; private set; }

    public override string ToString() => $"{this.Language ?? "???"} / {this.Script ?? "????"}";

  }

}
