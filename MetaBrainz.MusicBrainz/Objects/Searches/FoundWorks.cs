using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundWorks : SearchResults<IFoundWork> {

    public FoundWorks(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "work", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundWork> Results => this.CurrentResult?.Works ?? Array.Empty<IFoundWork>();

  }

}
