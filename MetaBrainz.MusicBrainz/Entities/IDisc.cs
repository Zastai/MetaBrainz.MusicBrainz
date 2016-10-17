using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IDisc : IEntity {

    int OffsetCount { get; }

    IEnumerable<int> Offsets { get; }

    IEnumerable<IRelease> Releases { get; }

    int Sectors { get; }

  }

}
