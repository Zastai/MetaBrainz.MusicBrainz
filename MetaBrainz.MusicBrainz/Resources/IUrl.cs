using System;

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IUrl : IMbEntity, IRelatableResource {

    Uri Resource { get; }

  }

}
