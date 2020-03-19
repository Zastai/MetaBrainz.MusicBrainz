using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal abstract class SearchResult : JsonBasedObject, ISearchResult {

    [JsonPropertyName("score")]
    public byte? SearchScore { get; set; }

    public byte Score => this.SearchScore.GetValueOrDefault(0);

  }

}
