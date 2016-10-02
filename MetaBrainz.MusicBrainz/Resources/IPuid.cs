using System;

namespace MetaBrainz.MusicBrainz.Resources {

  [Obsolete]
  public interface IPuid : IMbEntity {

    IResourceList<IRecording> RecordingList { get; }

  }

}
