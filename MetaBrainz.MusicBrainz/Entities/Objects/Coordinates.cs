using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Coordinates : ICoordinates {

    public double Latitude => this._json.latitude;

    public double Longitude => this._json.longitude;

    #region JSON-Based Construction

    internal Coordinates(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public double latitude;
      [JsonProperty] public double longitude;
    }

    #endregion

  }

}
