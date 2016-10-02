namespace MetaBrainz.MusicBrainz.Resources {

  public interface IRelationAttribute : ITextResource {

    string CreditedAs { get; }

    string Value      { get; }

  }

}
