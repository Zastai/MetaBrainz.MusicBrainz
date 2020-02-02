using System.Collections.Generic;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundLabels : SearchResults<IFoundLabel> {

    public FoundLabels(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "label", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundLabel> Results => this.CurrentResult?.Labels;

  }

}
