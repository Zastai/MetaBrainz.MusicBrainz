using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IWork : IMbEntity, IAnnotatedEntity, IRatedEntity, IRelatableEntity, ITaggedEntity, ITitledEntity, ITypedEntity {

    IEnumerable<IWorkAttribute> Attributes { get; }

    IEnumerable<string> Iswcs { get; }

    string Language { get; }

  }

}
