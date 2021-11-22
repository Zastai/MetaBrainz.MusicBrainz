using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundInstruments : SearchResults<ISearchResult<IInstrument>> {

  public FoundInstruments(Query query, string queryString, int? limit, int? offset, bool simple)
    : base(query, "instrument", queryString, limit, offset, simple) {
  }

  public override IReadOnlyList<ISearchResult<IInstrument>> Results
    => this.CurrentResult?.Instruments ?? Array.Empty<ISearchResult<IInstrument>>();

}
