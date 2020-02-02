using System.Collections.Generic;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundSeries : SearchResults<IFoundSeries> {

    public FoundSeries(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "series", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundSeries> Results => this.CurrentResult?.Series;

  }

}
