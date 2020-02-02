using System.Collections.Generic;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundPlaces : SearchResults<IFoundPlace> {

    public FoundPlaces(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "place", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundPlace> Results => this.CurrentResult?.Places;

  }

}
