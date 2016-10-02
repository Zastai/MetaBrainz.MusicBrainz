using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IStringList : IResource {

    int? Count { get; }

    IEnumerable<string> Items { get; }

  }

}
