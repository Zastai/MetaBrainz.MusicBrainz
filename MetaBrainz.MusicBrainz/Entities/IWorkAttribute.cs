using System;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>An attribute set for a work (<see cref="IWork"/>).</summary>
  public interface IWorkAttribute {

    /// <summary>The type of the attribute.</summary>
    string Type { get; }

    /// <summary>The internal ID for the attribute type.</summary>
    Guid? TypeId { get; }

    /// <summary>The value for the attribute.</summary>
    string Value { get; }

    /// <summary>The internal ID for the value, if applicable.</summary>
    Guid? ValueId { get; }

  }

}
