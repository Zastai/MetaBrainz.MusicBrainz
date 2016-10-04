using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IArtistCredit : IResource {

    IEnumerable<INameCredit> NameCredits { get; }

  }

}
