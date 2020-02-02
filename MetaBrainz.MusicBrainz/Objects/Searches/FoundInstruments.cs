using System.Collections.Generic;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundInstruments : SearchResults<IFoundInstrument> {

    public FoundInstruments(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "instrument", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundInstrument> Results => this.CurrentResult?.Instruments;

  }

}
