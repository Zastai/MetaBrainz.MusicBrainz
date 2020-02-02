using System.Collections.Generic;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundRecordings : SearchResults<IFoundRecording> {

    public FoundRecordings(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "recording", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundRecording> Results => this.CurrentResult?.Recordings;

  }

}
