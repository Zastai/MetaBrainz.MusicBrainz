using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  #if NETFX_GE_4_5
  using DiscList  = IReadOnlyList<IDisc>;
  using TrackList = IReadOnlyList<ITrack>;
  #else
  using DiscList  = IEnumerable<IDisc>;
  using TrackList = IEnumerable<ITrack>;
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

    [JsonProperty("format-id", Required = Required.Default)]
    public Guid? FormatId { get; private set; }

    [JsonProperty("position", Required = Required.DisallowNull)]
    public int Position { get; private set; }

    public ITrack Pregap => this._pregap;

    [JsonProperty("pregap", Required = Required.DisallowNull)]
    private Track _pregap = null;

    [JsonProperty("title", Required = Required.DisallowNull)]
    public string Title { get; private set; }

    [JsonProperty("track-count", Required = Required.AllowNull)]
    public int TrackCount { get; private set; }

    [JsonProperty("track-offset", Required = Required.Default)]
    public int? TrackOffset { get; private set; }

    public TrackList Tracks => this._tracks;

    [JsonProperty("tracks", Required = Required.DisallowNull)]
    private Track[] _tracks = null;

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - the format ID is not serialized
    // - the position ID is not serialized
    // - the title ID is not serialized
    // => Adjusted the Required flags for affected properties (to allow their omission).

    #endregion

    public override string ToString() {
      var text = this.Format ?? "Medium";
      if (!string.IsNullOrEmpty(this.Title))
        text += " “" + this.Title + "”";
      text += $" ({this.TrackCount} track(s))";
      return text;
    }

  }

}
