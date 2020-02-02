using System;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>An attribute for a work.</summary>
  [PublicAPI]
  public interface IWorkAttribute : IJsonBasedObject {

    /// <summary>The type of the attribute.</summary>
    string Type { get; }

    /// <summary>The MBID for the attribute type, if applicable.</summary>
    Guid? TypeId { get; }

    /// <summary>The value for the attribute.</summary>
    string Value { get; }

    /// <summary>The MBID for the value, if applicable.</summary>
    Guid? ValueId { get; }

  }

}
