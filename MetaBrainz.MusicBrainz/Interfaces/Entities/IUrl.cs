using System;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A MusicBrainz URL.</summary>
  [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IUrl : IEntity, IRelatableEntity {

    /// <summary>The URL's resource location.</summary>
    Uri Resource { get; }

  }

}
