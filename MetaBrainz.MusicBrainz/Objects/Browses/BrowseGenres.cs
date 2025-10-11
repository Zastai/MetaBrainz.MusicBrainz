using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseGenres(Query query, IReadOnlyDictionary<string, string> options, int? limit, int? offset)
  : BrowseResults<IGenre>(query, "genre", "all", options, limit, offset) {

  public override IReadOnlyList<IGenre> Results => this.CurrentResult?.Genres ?? [];

}
