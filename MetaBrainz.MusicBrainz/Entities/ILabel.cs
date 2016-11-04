using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A MusicBrainz label.</summary>
  public interface ILabel : IEntity, IAnnotatedEntity, INamedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    /// <summary>The main area associated with the label.</summary>
    IArea Area { get; }

    /// <summary>The ISO 3166-1 code for the (primary) country associated with the label.</summary>
    string Country { get; }

    /// <summary>The IPI (Interested Parties Information) codes associated with this label.</summary>
    IEnumerable<string> Ipis { get; }

    /// <summary>The ISNI (International Standard Name Identifier, ISO 27729) codes associated with this label.</summary>
    IEnumerable<string> Isnis { get; }

    /// <summary>The label code for this label (as in &quot;LC-<em>nnnn</em>&quot;).</summary>
    int? LabelCode { get; }

    /// <summary>The label's lifespan.</summary>
    ILifeSpan LifeSpan { get; }

    /// <summary>The releases associated with the label, if any.</summary>
    IEnumerable<IRelease> Releases { get; }

  }

}
