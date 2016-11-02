namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>The lifespan of an entity.</summary>
  public interface ILifeSpan {

    /// <summary>The starting date of the lifespan (YYYY, YYYY-MM or YYYY-MM-DD).</summary>
    PartialDate Begin { get; }

    /// <summary>The ending date of the lifespan (YYYY, YYYY-MM or YYYY-MM-DD).</summary>
    PartialDate End { get; }

    /// <summary>Flag indicating whether or not the lifespan has ended.</summary>
    bool Ended { get; }

  }

}
