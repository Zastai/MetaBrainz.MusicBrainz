using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IResourceList<out T> : IResource where T : IResource {

    uint? Count { get; }

    uint? Offset { get; }

    IEnumerable<T> Items { get; }

  }

}
