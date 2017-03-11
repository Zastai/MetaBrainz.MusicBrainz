using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Coordinates : ICoordinates {

    [JsonProperty("latitude", Required = Required.Always)]
    public double Latitude { get; private set; }

    [JsonProperty("longitude", Required = Required.Always)]
    public double Longitude { get; private set; }

    public override string ToString() => $"({this.Latitude:F6}, {this.Longitude:F6})";

  }

}
