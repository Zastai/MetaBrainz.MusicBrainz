using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Tag : SearchResult, IFoundTag {

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    [JsonProperty("count", Required = Required.Default)]
    public int VoteCount { get; private set; }

    public override string ToString() => $"{(this.SearchScore.HasValue ? $"[Score: {this.SearchScore.Value}] " : "")}{this.Name} (votes: {this.VoteCount})";

  }

}
