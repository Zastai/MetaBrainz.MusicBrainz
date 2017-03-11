using System;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class WorkAttribute : IWorkAttribute {

    [JsonProperty("type", Required = Required.Always)]
    public string Type { get; private set; }

    [JsonProperty("type-id", Required = Required.Always)]
    public Guid? TypeId { get; private set; }

    [JsonProperty("value", Required = Required.AllowNull)]
    public string Value { get; private set; }

    [JsonProperty("value-id", Required = Required.DisallowNull)]
    public Guid? ValueId { get; private set; }

    public override string ToString() => $"{this.Type}: {this.Value}";

  }

}
