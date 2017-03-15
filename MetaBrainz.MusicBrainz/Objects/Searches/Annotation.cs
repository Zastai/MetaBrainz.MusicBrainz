using System;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Searches;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Annotation : IFoundAnnotation {

    [JsonProperty("entity", Required = Required.Always)]
    public Guid Entity { get; private set; }

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    [JsonProperty("score", Required = Required.Always)]
    public byte Score { get; private set; }

    [JsonProperty("text", Required = Required.Always)]
    public string Text { get; private set; }

    [JsonProperty("type", Required = Required.Always)]
    public string Type { get; private set; }

    public override string ToString() => $"[{this.Score,3}] {this.Text}";

  }

}
