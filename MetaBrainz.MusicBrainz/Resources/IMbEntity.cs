using System;

namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>An item identified by an MBID.</summary>
  public interface IMbEntity : IEntity {

    /// <summary>The MBID that identifies this entity.</summary>
    new Guid Id { get; }

  }

}
