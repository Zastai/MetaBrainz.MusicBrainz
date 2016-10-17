using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IRecording : IMbEntity, IAnnotatedEntity, IRatedEntity, IRelatableEntity, ITaggedEntity, ITitledEntity {

    IEnumerable<INameCredit> ArtistCredit { get; }

    IEnumerable<string> Isrcs { get; }

    int? Length { get; }

    IEnumerable<IRelease> Releases { get; }

    bool? Video { get; }

  }

}
