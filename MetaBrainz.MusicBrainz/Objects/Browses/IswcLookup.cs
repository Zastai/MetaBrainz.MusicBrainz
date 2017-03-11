namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class IswcLookup : BrowseWorksBase {

    public IswcLookup(Query query, string iswc, string extra) : base(query, "iswc", iswc, extra) { }

  }

}
