namespace MetaBrainz.MusicBrainz.Resources {

  public interface IIsrc : IEntity {

    IResourceList<IRecording> RecordingList { get; }

  }

}
