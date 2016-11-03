using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IRecording : IEntity, IAnnotatedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITitledEntity {

    IEnumerable<INameCredit> ArtistCredit { get; }

    IEnumerable<string> Isrcs { get; }

    int? Length { get; }

    IEnumerable<IRelease> Releases { get; }

    bool? Video { get; }

  }

}
