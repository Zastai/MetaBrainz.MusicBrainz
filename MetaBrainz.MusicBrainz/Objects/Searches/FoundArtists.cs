using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundArtists(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<IArtist>>(query, "artist", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<IArtist>> Results => this.CurrentResult?.Artists ?? [];

}
