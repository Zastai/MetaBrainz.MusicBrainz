using JetBrains.Annotations;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal abstract class SearchResult : JsonBasedObject, ISearchResult {

    public byte? SearchScore { get; set; }

    public byte Score => this.SearchScore.GetValueOrDefault(0);

  }

}
