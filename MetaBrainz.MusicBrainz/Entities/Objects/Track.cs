using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  public sealed class Track : ITrack {

    public string Id => this._json.id;

    public IEnumerable<INameCredit> ArtistCredit => this._json.artist_credit.WrapArray(ref this._artistCredit, j => new NameCredit(j));

    private NameCredit[] _artistCredit;

    public int? Length => this._json.length;

    public string Number => this._json.number;

    public int? Position => this._json.position;

    public IRecording Recording => this._json.recording.WrapObject(ref this._recording, j => new Recording(j));

    private Recording _recording;

    public string Title => this._json.title;

    #region JSON-Based Construction

    internal Track(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty("artist-credit")] public NameCredit.JSON[] artist_credit;
      [JsonProperty] public string id;
      [JsonProperty] public int? length;
      [JsonProperty] public string number;
      [JsonProperty] public int? position;
      [JsonProperty] public string title;
      [JsonProperty] public Recording.JSON recording;
    }

    #endregion

  }

}
