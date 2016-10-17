using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Disc : IDisc {

    public string Id => this._json.id;

    public int OffsetCount => this._json.offset_count;

    public IEnumerable<int> Offsets => this._json.offsets;

    public IEnumerable<IRelease> Releases => this._json.releases.WrapArray(ref this._releases, j => new Release(j));

    private Release[] _releases;

    public int Sectors => this._json.sectors;

    #region JSON-Based Construction

    internal Disc(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public string id;
      [JsonProperty] public int[] offsets;
      [JsonProperty("offset-count")] public int offset_count;
      [JsonProperty] public Release.JSON[] releases;
      [JsonProperty] public int sectors;
    }

    #endregion

  }

}
