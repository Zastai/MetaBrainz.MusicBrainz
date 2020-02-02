using System.Collections.Generic;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundUrls : SearchResults<IFoundUrl> {

    public FoundUrls(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "url", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundUrl> Results => this.CurrentResult?.Urls;

  }

}
