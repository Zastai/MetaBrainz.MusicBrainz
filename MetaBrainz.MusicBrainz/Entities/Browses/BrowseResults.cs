namespace MetaBrainz.MusicBrainz.Entities.Browses {

  internal abstract class BrowseResults<T> : PagedQueryResults<IBrowseResults<T>, T>, IBrowseResults<T> where T : IEntity {

    protected BrowseResults(Query query, string endpoint, string value, string extra, int? limit = null, int? offset = null) : base(query, endpoint, value, limit, offset) {
      this._extra = extra;
    }

    private readonly string _extra;

    protected sealed override string FullExtraText() {
      var extra = this._extra;
      if (string.IsNullOrEmpty(extra))
        extra = $"?offset={this.Offset}";
      else
        extra += $"&offset={this.Offset}";
      if (this.Limit.HasValue)
        extra += $"&limit={this.Limit}";
      return extra;
    }

  }

}
