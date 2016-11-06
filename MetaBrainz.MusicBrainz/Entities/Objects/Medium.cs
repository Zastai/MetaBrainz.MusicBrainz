using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Medium : IMedium {

    public IEnumerable<ITrack> DataTracks => this._dataTracks;

    [JsonProperty("data-tracks")]
    private Track[] _dataTracks = null;

    public IEnumerable<IDisc> Discs => this._discs;

    [JsonProperty("discs")]
    private Disc[] _discs = null;

    [JsonProperty("format")]
    public string Format { get; private set; }

    [JsonProperty("format-id")]
    public Guid? FormatId { get; private set; }

    [JsonProperty("position")]
    public int? Position { get; private set; }

    public ITrack Pregap => this._pregap;

    [JsonProperty("pregap")]
    private Track _pregap = null;

    [JsonProperty("title")]
    public string Title { get; private set; }

    [JsonProperty("track-count")]
    public int TrackCount { get; private set; }

    [JsonProperty("track-offset")]
    public int TrackOffset { get; private set; }

    public IEnumerable<ITrack> Tracks => this._tracks;

    [JsonProperty("tracks")]
    private Track[] _tracks = null;

    public override string ToString() {
      var text = this.Format ?? "Medium";
      if (!string.IsNullOrEmpty(this.Title))
        text += " “" + this.Title + "”";
      text += $" ({this.TrackCount} track(s))";
      return text;
    }

  }

}
