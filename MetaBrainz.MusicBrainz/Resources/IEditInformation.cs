namespace MetaBrainz.MusicBrainz.Resources {

  public interface IEditInformation : IResource {

    uint AutoEditsAccepted { get; }

    uint EditsAccepted { get; }

    uint EditsFailed { get; }

    uint EditsRejected { get; }

  }

}
