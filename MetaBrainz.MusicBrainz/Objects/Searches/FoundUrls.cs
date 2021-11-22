using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches; 

internal sealed class FoundUrls : SearchResults<ISearchResult<IUrl>> {

  public FoundUrls(Query query, string queryString, int? limit, int? offset, bool simple)
    : base(query, "url", queryString, limit, offset, simple)
  { }

  public override IReadOnlyList<ISearchResult<IUrl>> Results => this.CurrentResult?.Urls ?? Array.Empty<ISearchResult<IUrl>>();

}