namespace MetaBrainz.MusicBrainz.Resources {

  public interface IArtistCredit : IResource {

    IResourceList<INameCredit> NameCredits { get; }

  }

}
