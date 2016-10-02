using System;

namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A typed resource.</summary>
  public interface ITypedResource : IResource {

    /// <summary>The type of the resource, expressed as text.</summary>
    string Type { get; }

    /// <summary>The type of the resource, expressed as an MBID.</summary>
    Guid? TypeId { get; }

  }

}
