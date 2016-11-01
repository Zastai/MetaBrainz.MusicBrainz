using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface ILabel : IMbEntity, IAnnotatedEntity, INamedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    IArea Area { get; }

    string Country { get; }

    /// <summary>The IPI (Interested Parties Information) codes associated with this label.</summary>
    IEnumerable<string> Ipis { get; }

    /// <summary>The ISNI (International Standard Name Identifier, ISO 27729) codes associated with this label.</summary>
    IEnumerable<string> Isnis { get; }

    int? LabelCode { get; }

    ILifeSpan LifeSpan { get; }

    IEnumerable<IRelease> Releases { get; }

  }

}
