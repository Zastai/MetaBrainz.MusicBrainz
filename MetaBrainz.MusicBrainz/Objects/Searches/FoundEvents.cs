using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundEvents : SearchResults<IFoundEvent> {

    public FoundEvents(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "event", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundEvent> Results => this.CurrentResult?.Events ?? Array.Empty<IFoundEvent>();

  }

}
