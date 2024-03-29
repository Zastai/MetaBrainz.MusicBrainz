﻿using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseGenres : BrowseResults<IGenre> {

  public BrowseGenres(Query query, int? limit, int? offset) : base(query, "genre", "all", null, limit, offset) {
  }

  public override IReadOnlyList<IGenre> Results => this.CurrentResult?.Genres ?? Array.Empty<IGenre>();

}
