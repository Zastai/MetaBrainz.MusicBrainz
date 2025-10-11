using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseEvents(Query query, IReadOnlyDictionary<string, string> options, int? limit, int? offset)
  : BrowseResults<IEvent>(query, "event", null, options, limit, offset) {

  public override IReadOnlyList<IEvent> Results => this.CurrentResult?.Events ?? [];

}
