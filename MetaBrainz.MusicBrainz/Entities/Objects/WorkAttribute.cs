using System;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class WorkAttribute : IWorkAttribute {

    [JsonProperty("type")]
    public string Type { get; private set; }

    [JsonProperty("type-id")]
    public Guid? TypeId { get; private set; }

    [JsonProperty("value")]
    public string Value { get; private set; }

    [JsonProperty("value-id")]
    public Guid? ValueId { get; private set; }

    public override string ToString() => $"{this.Type}: {this.Value}";

  }

}
