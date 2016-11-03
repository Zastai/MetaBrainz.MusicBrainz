using System;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IUrl : IEntity, IRelatableEntity {

    Uri Resource { get; }

  }

}
