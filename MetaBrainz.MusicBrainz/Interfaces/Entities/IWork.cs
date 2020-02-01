using System.Collections.Generic;
using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A MusicBrainz work.</summary>
  [PublicAPI]
  public interface IWork : IAnnotatedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITitledEntity, ITypedEntity {

    /// <summary>The attributes attached to this work (if any).</summary>
    IReadOnlyList<IWorkAttribute> Attributes { get; }

    /// <summary>The ISWCs (International Standard Musical Work Codes) attached to this work (if any).</summary>
    IReadOnlyList<string> Iswcs { get; }

    /// <summary>The ISO 639-2 language code for the lyrics of this work, if applicable.</summary>
    string Language { get; }

  }

}
