using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches; 

internal sealed class FoundRecordings : SearchResults<ISearchResult<IRecording>> {

  public FoundRecordings(Query query, string queryString, int? limit, int? offset, bool simple)
    : base(query, "recording", queryString, limit, offset, simple)
  { }

  public override IReadOnlyList<ISearchResult<IRecording>> Results => this.CurrentResult?.Recordings ?? Array.Empty<ISearchResult<IRecording>>();

}