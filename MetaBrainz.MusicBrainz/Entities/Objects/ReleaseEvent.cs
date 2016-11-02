using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class ReleaseEvent : IReleaseEvent {

    public IArea Area => this._json.area.WrapObject(ref this._area, j => new Area(j));

    private Area _area;

    public PartialDate Date => this._json.date;

    #region JSON-Based Construction

    internal ReleaseEvent(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public Area.JSON area;
      [JsonProperty] public PartialDate date;
    }

    #endregion

  }

}
