using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseEvents : BrowseResults<IEvent> {

    public BrowseEvents(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "event", null, extra, limit, offset) {
    }

    public override IReadOnlyList<IEvent> Results => this.CurrentResult?.Events ?? Array.Empty<IEvent>();

  }

}
