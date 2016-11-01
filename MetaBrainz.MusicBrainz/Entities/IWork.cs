using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IWork : IMbEntity, IAnnotatedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITitledEntity, ITypedEntity {

    IEnumerable<IWorkAttribute> Attributes { get; }

    IEnumerable<string> Iswcs { get; }

    string Language { get; }

  }

}
