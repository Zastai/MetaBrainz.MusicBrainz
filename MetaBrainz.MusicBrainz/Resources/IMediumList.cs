using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IMediumList : IResourceList<IMedium> {

    uint? TrackCount { get; }

  }

}
