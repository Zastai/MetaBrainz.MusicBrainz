namespace MetaBrainz.MusicBrainz.Resources {

  public interface ILabelInfo : IResource {

    string CatalogNumber { get; }

    ILabel  Label { get; }

  }

}
