using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseArtists : BrowseResults<IArtist> {

    public BrowseArtists(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "artist", null, extra, limit, offset) {
    }

    public override IReadOnlyList<IArtist> Results => this.CurrentResult?.Artists ?? Array.Empty<IArtist>();

  }

}
