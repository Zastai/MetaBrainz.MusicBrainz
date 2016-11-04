using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A MuscBrainz work.</summary>
  public interface IWork : IEntity, IAnnotatedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITitledEntity, ITypedEntity {

    /// <summary>The attributes attached to this work (if any).</summary>
    IEnumerable<IWorkAttribute> Attributes { get; }

    /// <summary>The ISWCs (International Standard Musical Work Codes) attached to this work (if any).</summary>
    IEnumerable<string> Iswcs { get; }

    /// <summary>The ISO 639-2 language code for the lyrics of this work, if applicable.</summary>
    string Language { get; }

  }

}
