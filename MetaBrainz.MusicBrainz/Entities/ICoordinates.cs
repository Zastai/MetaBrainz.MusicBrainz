namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A set of coordinates.</summary>
  public interface ICoordinates {

    /// <summary>The latitude component of the coordinates.</summary>
    double Latitude { get; }

    /// <summary>The longitude component of the coordinates.</summary>
    double Longitude { get; }

  }

}
