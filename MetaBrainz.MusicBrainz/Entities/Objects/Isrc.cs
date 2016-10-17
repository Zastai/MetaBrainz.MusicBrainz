using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Isrc : IIsrc {

    public IEnumerable<IRecording> Recordings => this._json.recordings.WrapArray(ref this._recordings, j => new Recording(j));

    private Recording[] _recordings;

    public string Value => this._json.isrc;

    #region JSON-Based Construction

    internal Isrc(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public string isrc;
      [JsonProperty] public Recording.JSON[] recordings;
    }

    #endregion

  }

}
