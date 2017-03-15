using System;

using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal abstract class SearchResults<T> : PagedQueryResults<ISearchResults<T>, T>, ISearchResults<T> where T : ISearchResult {

    protected SearchResults(Query query, string endpoint, string queryString, int? limit = null, int? offset = null) : base(query, endpoint, null, limit, offset) {
      this._queryString = queryString;
    }

    private readonly string _queryString;

    public abstract DateTime? Created { get; }

    protected sealed override string FullExtraText() {
      var extra = "?query=";
      extra += Uri.EscapeDataString(this._queryString);
      if (this.Offset > 0)
        extra += $"&offset={this.Offset}";
      if (this.Limit.HasValue)
        extra += $"&limit={this.Limit}";
      // Pending fix for MBS-9258
      extra += "&fmt=json";
      return extra;
    }

  }

}
