using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities; 

internal sealed class Coordinates : JsonBasedObject, ICoordinates {

  public Coordinates(double latitude, double longitude) {
    this.Latitude = latitude;
    this.Longitude = longitude;
  }

  public double Latitude { get; }

  public double Longitude { get; }

  public override string ToString() => $"({this.Latitude:F6}, {this.Longitude:F6})";

}