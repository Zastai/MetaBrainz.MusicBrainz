using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A set of coordinates.</summary>
  [PublicAPI]
  public interface ICoordinates {

    /// <summary>The latitude component of the coordinates.</summary>
    double Latitude { get; }

    /// <summary>The longitude component of the coordinates.</summary>
    double Longitude { get; }

  }

}
