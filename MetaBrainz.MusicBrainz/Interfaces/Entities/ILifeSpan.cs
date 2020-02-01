using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>The lifespan of an entity.</summary>
  [PublicAPI]
  public interface ILifeSpan {

    /// <summary>The starting date of the lifespan.</summary>
    PartialDate Begin { get; }

    /// <summary>The ending date of the lifespan.</summary>
    PartialDate End { get; }

    /// <summary>Flag indicating whether or not the lifespan has ended.</summary>
    bool Ended { get; }

  }

}
