using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  #if NETFX_LT_4_5
  using DiscList  = IEnumerable<IDisc>;
  using TrackList = IEnumerable<ITrack>;
  #else
  using DiscList  = IReadOnlyList<IDisc>;
  using TrackList = IReadOnlyList<ITrack>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Medium : IMedium {

    public TrackList DataTracks => this._dataTracks;

    [JsonProperty("data-tracks", Required = Required.DisallowNull)]
    private Track[] _dataTracks = null;

    public DiscList Discs => this._discs;

    [JsonProperty("discs", Required = Required.DisallowNull)]
    private Disc[] _discs = null;

    [JsonProperty("format", Required = Required.AllowNull)]
    public string Format { get; private set; }

    [JsonProperty("format-id", Required = Required.AllowNull)]
    public Guid? FormatId { get; private set; }

    [JsonProperty("position", Required = Required.Always)]
    public int Position { get; private set; }

    public ITrack Pregap => this._pregap;

    [JsonProperty("pregap", Required = Required.DisallowNull)]
    private Track _pregap = null;

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; private set; }

    [JsonProperty("track-count", Required = Required.AllowNull)]
    public int TrackCount { get; private set; }

    [JsonProperty("track-offset", Required = Required.Default)]
    public int? TrackOffset { get; private set; }

    public TrackList Tracks => this._tracks;

    [JsonProperty("tracks", Required = Required.DisallowNull)]
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
