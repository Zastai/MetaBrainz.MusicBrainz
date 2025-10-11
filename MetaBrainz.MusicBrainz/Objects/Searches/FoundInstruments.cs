using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundInstruments(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<IInstrument>>(query, "instrument", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<IInstrument>> Results => this.CurrentResult?.Instruments ?? [];

}
