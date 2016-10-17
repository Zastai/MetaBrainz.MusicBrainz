using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface ITrack : IEntity {

    IEnumerable<INameCredit> ArtistCredit { get; }

    int? Length { get; }

    string Number { get; }

    int? Position { get; }

    IRecording Recording { get; }

    string Title { get; }

  }

}
