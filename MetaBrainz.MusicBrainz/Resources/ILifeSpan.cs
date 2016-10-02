namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>The lifespan of an entity.</summary>
  public interface ILifeSpan : IResource {

    /// <summary>The starting date of the lifespan (YYYY, YYYY-MM or YYYY-MM-DD).</summary>
    string Begin { get; }

    /// <summary>The ending date of the lifespan (YYYY, YYYY-MM or YYYY-MM-DD).</summary>
    string End { get; }

    /// <summary>Flag indicating whether or not the lifespan has ended.</summary>
    bool? Ended { get; }

  }

}
