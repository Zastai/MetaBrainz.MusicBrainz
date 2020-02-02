using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Tag : SearchResult, IFoundTag {

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("count")]
    public int VoteCount { get; set; }

    public override string ToString() => $"{(this.SearchScore.HasValue ? $"[Score: {this.SearchScore.Value}] " : "")}{this.Name} (votes: {this.VoteCount})";

  }

}
