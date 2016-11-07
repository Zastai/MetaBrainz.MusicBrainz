using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  #if NETFX_LT_4_5
  using SimpleTrackList = IEnumerable<ISimpleTrack>;
  #else
  using SimpleTrackList = IReadOnlyList<ISimpleTrack>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class CdStub : ICdStub {

    [JsonProperty("id", Required = Required.Always)]
    public string Id { get; private set; }

    [JsonProperty("artist", Required = Required.Always)]
    public string Artist { get; private set; }

    [JsonProperty("barcode", Required = Required.AllowNull)]
    public string Barcode { get; private set; }

    [JsonProperty("disambiguation", Required = Required.Always)]
    public string Disambiguation { get; private set; }

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; private set; }

    [JsonProperty("track-count", Required = Required.AllowNull)]
    public int? TrackCount { get; private set; }

    public SimpleTrackList Tracks => this._tracks;

    [JsonProperty("tracks", Required = Required.Always)]
    private SimpleTrack[] _tracks = null;

    public override string ToString() {
      var text = this.Artist + " / " + this.Title;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      return text;
    }

  }

}
