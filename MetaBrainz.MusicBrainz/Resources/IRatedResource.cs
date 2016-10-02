namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A rated resource.</summary>
  public interface IRatedResource : IResource {

    /// <summary>The rating for the resource.</summary>
    IRating Rating { get; }

    /// <summary>The rating given to the resource by the authenticated user.</summary>
    byte? UserRating { get; }

  }

}
