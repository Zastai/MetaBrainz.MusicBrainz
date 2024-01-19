using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseEvents : BrowseResults<IEvent> {

  public BrowseEvents(Query query, IReadOnlyDictionary<string, string> options, int? limit,
                      int? offset) : base(query, "event", null, options, limit, offset) {
  }

  public override IReadOnlyList<IEvent> Results => this.CurrentResult?.Events ?? Array.Empty<IEvent>();

}
