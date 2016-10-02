using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IStringList : IResource {

    uint? Count { get; }

    uint? Offset { get; }

    IEnumerable<string> Items { get; }

  }

}
