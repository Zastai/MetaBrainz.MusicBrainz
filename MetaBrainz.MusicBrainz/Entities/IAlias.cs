using System;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>An alias for an entity.</summary>
  public interface IAlias {

    /// <summary>The date at which the alias became applicable.</summary>
    string BeginDate { get; }

    /// <summary>The date at which the alias ceased to be applicable.</summary>
    string EndDate { get; }

    /// <summary>A flag indicating whether or not the alias has ceased to be applicable.</summary>
    bool? Ended { get; }

    /// <summary>The specific locale where the alias applies.</summary>
    string Locale { get; }

    /// <summary>The alias.</summary>
    string Name { get; }

    /// <summary>Flag indicating whether or not this is the primary alias within the locale specified by <see cref="Locale"/>.</summary>
    bool? Primary { get; }

    /// <summary>The sort name form of the alias, if applicable.</summary>
    string SortName { get; }

    /// <summary>The type of the alias.</summary>
    string Type { get; }

    /// <summary>The internal ID for the alias type.</summary>
    Guid? TypeId { get; }

  }

}
