using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundEvents : SearchResults<ISearchResult<IEvent>> {

    public FoundEvents(Query query, string queryString, int? limit, int? offset, bool simple)
    : base(query, "event", queryString, limit, offset, simple)
    { }

    public override IReadOnlyList<ISearchResult<IEvent>> Results => this.CurrentResult?.Events ?? Array.Empty<ISearchResult<IEvent>>();

  }

}
