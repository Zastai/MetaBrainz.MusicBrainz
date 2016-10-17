using System;
using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IReleaseGroup : IMbEntity, IAnnotatedEntity, IRatedEntity, IRelatableEntity, ITaggedEntity, ITitledEntity, ITypedEntity {

    IEnumerable<INameCredit> ArtistCredit { get; }

    string FirstReleaseDate { get; }

    string PrimaryType { get; }

    Guid? PrimaryTypeId { get; }

    IEnumerable<IRelease> Releases { get; }

    IEnumerable<string> SecondaryTypes { get; }

    IEnumerable<Guid> SecondaryTypeIds { get; }

  }

}
