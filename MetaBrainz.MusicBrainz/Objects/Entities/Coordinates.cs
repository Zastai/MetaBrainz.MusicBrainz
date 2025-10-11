using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Coordinates : JsonBasedObject, ICoordinates {

  public required double Latitude { get; init; }

  public required double Longitude { get; init; }

  public override string ToString() => $"({this.Latitude:F6}, {this.Longitude:F6})";

}
