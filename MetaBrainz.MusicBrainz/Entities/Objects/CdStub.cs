using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class CdStub : ICdStub {

    [JsonProperty("id")]
    public string Id { get; private set; }

    [JsonProperty("artist")]
    public string Artist { get; private set; }

    [JsonProperty("barcode")]
    public string Barcode { get; private set; }

    [JsonProperty("disambiguation")]
    public string Disambiguation { get; private set; }

    [JsonProperty("title")]
    public string Title { get; private set; }

    [JsonProperty("track-count")]
    public int? TrackCount { get; private set; }

    public IEnumerable<ISimpleTrack> Tracks => this._tracks;

    [JsonProperty("tracks")]
    private SimpleTrack[] _tracks = null;

    public override string ToString() {
      var text = this.Artist + " / " + this.Title;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      return text;
    }

  }

}
