using System;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>An attribute for a work.</summary>
  public interface IWorkAttribute {

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
