namespace MetaBrainz.MusicBrainz.Entities {

  public interface ILabelInfo {

    string CatalogNumber { get; }

    ILabel  Label { get; }

  }

}
