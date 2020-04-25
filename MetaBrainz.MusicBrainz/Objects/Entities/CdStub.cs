using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class CdStub : SearchResult, IFoundCdStub {

    public CdStub(string id, string title) {
      this.Id = id;
      this.Title = title;
    }

    public string Id { get; }

    public string? Artist { get; set; }

    public string? Barcode { get; set; }

    public string? Disambiguation { get; set; }

    public string Title { get; }

    public int TrackCount { get; set; }

    public IReadOnlyList<ISimpleTrack>? Tracks { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.SearchScore.HasValue)
        text += $"[Score: {this.SearchScore.Value}] ";
      text += this.Artist + " / " + this.Title;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      return text;
    }

  }

}
