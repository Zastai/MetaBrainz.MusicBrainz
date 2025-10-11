using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseInstruments(Query query, IReadOnlyDictionary<string, string> options, int? limit, int? offset)
  : BrowseResults<IInstrument>(query, "instrument", null, options, limit, offset) {

  public override IReadOnlyList<IInstrument> Results => this.CurrentResult?.Instruments ?? [];

}
