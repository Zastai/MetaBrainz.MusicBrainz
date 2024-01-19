using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseWorks : BrowseWorksBase {

  public BrowseWorks(Query query, IReadOnlyDictionary<string, string> options, int? limit,
                     int? offset) : base(query, "work", null, options, limit, offset) {
  }

}
