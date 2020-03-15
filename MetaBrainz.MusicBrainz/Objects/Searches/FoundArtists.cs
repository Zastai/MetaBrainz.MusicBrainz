using System;
using System.Collections.Generic;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundArtists : SearchResults<IFoundArtist> {

    public FoundArtists(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "artist", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundArtist> Results => this.CurrentResult?.Artists ?? Array.Empty<IFoundArtist>();

  }

}
