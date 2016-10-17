using System;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IUrl : IMbEntity, IRelatableEntity {

    Uri Resource { get; }

  }

}
