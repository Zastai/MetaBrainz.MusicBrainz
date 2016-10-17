using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IIsrc {

    IEnumerable<IRecording> Recordings { get; }

    string Value { get; }

  }

}
