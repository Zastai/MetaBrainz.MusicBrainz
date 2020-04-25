using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundRecordings : SearchResults<ISearchResult<IRecording>> {

    public FoundRecordings(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "recording", queryString, limit, offset)
    { }

    public override IReadOnlyList<ISearchResult<IRecording>> Results => this.CurrentResult?.Recordings ?? Array.Empty<ISearchResult<IRecording>>();

  }

}
