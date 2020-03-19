using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundCdStubs : SearchResults<IFoundCdStub> {

    public FoundCdStubs(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "cdstub", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundCdStub> Results => this.CurrentResult?.CdStubs ?? Array.Empty<IFoundCdStub>();

  }

}
