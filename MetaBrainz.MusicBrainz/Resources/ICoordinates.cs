namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A set of coordinates.</summary>
  public interface ICoordinates : IResource {

    /// <summary>The latitude component of the coordinates.</summary>
    double Latitude { get; }

    /// <summary>The longitude component of the coordinates.</summary>
    double Longitude { get; }

  }

}
