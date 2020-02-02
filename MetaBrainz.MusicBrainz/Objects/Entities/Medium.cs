using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Medium : JsonBasedObject, IMedium {

    public IReadOnlyList<ITrack> DataTracks => this.TheDataTracks;

    [JsonPropertyName("data-tracks")]
    public Track[] TheDataTracks { get; set; }

    public IReadOnlyList<IDisc> Discs => this.TheDiscs;

    [JsonPropertyName("discs")]
    public Disc[] TheDiscs { get; set; }

    [JsonPropertyName("format")]
    public string Format { get; set; }

    [JsonPropertyName("format-id")]
    public Guid? FormatId { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    public ITrack Pregap => this.ThePregap;

    [JsonPropertyName("pregap")]
    public Track ThePregap { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("track-count")]
    public int TrackCount { get; set; }

    [JsonPropertyName("track-offset")]
    public int? TrackOffset { get; set; }

    // SEARCH-604: A medium can have either 'track' or 'tracks' depending on how it was included in the search.
    public IReadOnlyList<ITrack> Tracks => this.TheTracks ?? this.TheTrack;

    [JsonPropertyName("track")]
    public Track[] TheTrack { get; set; }

    [JsonPropertyName("tracks")]
    public Track[] TheTracks { get; set; }

    public override string ToString() {
      var text = this.Format ?? "Medium";
      if (!string.IsNullOrEmpty(this.Title))
        text += " “" + this.Title + "”";
      text += $" ({this.TrackCount} track(s))";
      return text;
    }

  }

}
