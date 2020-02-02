using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class CdStub : SearchResult, IFoundCdStub {

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("artist")]
    public string Artist { get; set; }

    [JsonPropertyName("barcode")]
    public string Barcode { get; set; }

    // The search server uses 'count' instead of 'track-count' -> forward to the track count.
    [JsonPropertyName("count")]
    public int SearchTrackCount { set => this.TrackCount = value; }

    [JsonPropertyName("disambiguation")]
    public string Disambiguation { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("track-count")]
    public int? TrackCount { get; set; }

    public IReadOnlyList<ISimpleTrack> Tracks => this.TheTracks;

    [JsonPropertyName("tracks")]
    public SimpleTrack[] TheTracks { get; set; }

    public override string ToString() {
      var text = this.SearchScore.HasValue ? $"[Score: {this.SearchScore.Value}] " : "";
      text += this.Artist + " / " + this.Title;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      return text;
    }

  }

}
