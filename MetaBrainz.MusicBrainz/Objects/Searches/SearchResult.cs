using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Searches;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [JsonObject(MemberSerialization.OptIn)]
  internal class SearchResult : ISearchResult {

    [JsonProperty("score", Required = Required.Default)] protected byte? SearchScore = null;

    public byte Score => this.SearchScore.GetValueOrDefault(0);

  }

}
