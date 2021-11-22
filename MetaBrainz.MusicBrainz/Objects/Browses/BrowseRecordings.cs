using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses; 

internal sealed class BrowseRecordings : BrowseResults<IRecording> {

  public BrowseRecordings(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "recording", null, extra, limit, offset) {
  }

  public override IReadOnlyList<IRecording> Results => this.CurrentResult?.Recordings ?? Array.Empty<IRecording>();

}