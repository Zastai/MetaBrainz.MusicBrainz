using System.Collections.Generic;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A MusicBrainz area.</summary>
  [PublicAPI]
  public interface IArea : IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    /// <summary>The ISO 3166-1 codes associated with this area, if any.</summary>
    IReadOnlyList<string> Iso31661Codes { get; }

    /// <summary>The ISO 3166-2 codes associated with this area, if any.</summary>
    IReadOnlyList<string> Iso31662Codes { get; }

    /// <summary>The ISO 3166-3 codes associated with this area, if any.</summary>
    IReadOnlyList<string> Iso31663Codes { get; }

    /// <summary>The area's lifespan.</summary>
    ILifeSpan LifeSpan { get; }

  }

}
