using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  public sealed class CdStub : ICdStub {

    public string ID => this._json.id;

    public string Artist => this._json.artist;

    public string Barcode => this._json.barcode;

    public string Disambiguation => this._json.disambiguation;

    public string Title => this._json.title;

    public int? TrackCount => this._json.track_count;

    public IEnumerable<ISimpleTrack> Tracks => this._json.tracks.WrapArray(ref this._tracks, j => new SimpleTrack(j));

    private SimpleTrack[] _tracks;

    #region JSON-Based Construction

    internal CdStub(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public string artist;
      [JsonProperty] public string barcode;
      [JsonProperty] public string disambiguation;
      [JsonProperty] public string id;
      [JsonProperty] public string title;
      [JsonProperty] public SimpleTrack.JSON[] tracks;
      [JsonProperty("track-count")] public int? track_count;
    }

    #endregion

  }

}
