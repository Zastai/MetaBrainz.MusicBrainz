using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IArea : IEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    IEnumerable<string> Iso31661Codes { get; }

    IEnumerable<string> Iso31662Codes { get; }

    IEnumerable<string> Iso31663Codes { get; }

    ILifeSpan LifeSpan { get; }

  }

}
