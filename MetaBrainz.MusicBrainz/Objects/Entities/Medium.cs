using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Medium : IMedium {

    public IReadOnlyList<ITrack> DataTracks => this._dataTracks;

    [JsonProperty("data-tracks", Required = Required.DisallowNull)]
    private Track[] _dataTracks = null;

    public IReadOnlyList<IDisc> Discs => this._discs;

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

    public IReadOnlyList<ITrack> Tracks => this._tracks;

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
