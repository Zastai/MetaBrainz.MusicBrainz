﻿using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class IswcLookup : BrowseWorksBase {

  public IswcLookup(Query query, string iswc, IReadOnlyDictionary<string, string> options) : base(query, "iswc", iswc, options) {
  }

}
