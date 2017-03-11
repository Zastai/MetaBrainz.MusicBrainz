using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Tag : ITag {

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    [JsonProperty("count", Required = Required.Always)]
    public int VoteCount { get; private set; }

    public override string ToString() => $"{this.Name} (votes: {this.VoteCount})";

  }

}
