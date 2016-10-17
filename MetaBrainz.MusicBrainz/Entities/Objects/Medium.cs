using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Medium : IMedium {

    public IEnumerable<ITrack> DataTracks => this._json.data_tracks.WrapArray(ref this._dataTracks, j => new Track(j));

    private Track[] _dataTracks;

    public IEnumerable<IDisc> Discs => this._json.discs.WrapArray(ref this._discs, j => new Disc(j));

    private Disc[] _discs;

    public string Format => this._json.format;

    public Guid? FormatId => this._json.format_id;

    public int? Position => this._json.position;

    public ITrack Pregap => this._json.pregap.WrapObject(ref this._pregap, j => new Track(j));

    private Track _pregap;

    public string Title => this._json.title;

    public int TrackCount => this._json.track_count;

    public int TrackOffset => this._json.track_offset;

    public IEnumerable<ITrack> Tracks => this._json.tracks.WrapArray(ref this._tracks, j => new Track(j));

    private Track[] _tracks;

    #region JSON-Based Construction

    internal Medium(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty("data-tracks")] public Track.JSON[] data_tracks;
      [JsonProperty] public Disc.JSON[] discs;
      [JsonProperty] public string format;
      [JsonProperty("format-id")] public Guid? format_id;
      [JsonProperty] public int? position;
      [JsonProperty] public Track.JSON pregap;
      [JsonProperty] public string title;
      [JsonProperty] public Track.JSON[] tracks;
      [JsonProperty("track-count")] public int track_count;
      [JsonProperty("track-offset")] public int track_offset;
    }

    #endregion

  }

}
