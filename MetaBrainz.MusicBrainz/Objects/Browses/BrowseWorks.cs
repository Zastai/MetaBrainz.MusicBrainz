namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseWorks : BrowseWorksBase {

    public BrowseWorks(Query query, string extra, int? limit, int? offset) : base(query, "work", null, extra, limit, offset) { }

  }

}
