using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundRecordings(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<IRecording>>(query, "recording", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<IRecording>> Results => this.CurrentResult?.Recordings ?? [];

}
