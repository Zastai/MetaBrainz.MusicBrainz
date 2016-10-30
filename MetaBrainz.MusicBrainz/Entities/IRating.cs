namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>Information about an entity's rating.</summary>
  public interface IRating {

    /// <summary>The rating value, expressed as a fractional number of stars (0-5).</summary>
    decimal Value { get; }

    /// <summary>The number of ratings submitted for the entity.</summary>
    int VoteCount { get; }

  }

}
