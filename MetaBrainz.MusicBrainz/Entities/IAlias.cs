using System;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>An alias for an entity.</summary>
  public interface IAlias {

    /// <summary>The date at which the alias became applicable.</summary>
    PartialDate Begin { get; }

    /// <summary>The date at which the alias ceased to be applicable.</summary>
    PartialDate End { get; }

    /// <summary>A flag indicating whether or not the alias has ceased to be applicable.</summary>
    bool Ended { get; }

    /// <summary>The specific locale where the alias applies.</summary>
    string Locale { get; }

    /// <summary>The alias.</summary>
    string Name { get; }

    /// <summary>Flag indicating whether or not this is the primary alias within the locale specified by <see cref="Locale"/>.</summary>
    bool? Primary { get; }

    /// <summary>The sort name form of the alias, if applicable.</summary>
    string SortName { get; }

    /// <summary>The type of the alias, expressed as text.</summary>
    string Type { get; }

    /// <summary>The type of the alias, expressed as an MBID.</summary>
    Guid? TypeId { get; }

  }

}
