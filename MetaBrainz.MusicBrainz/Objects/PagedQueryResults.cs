using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces;

namespace MetaBrainz.MusicBrainz.Objects {

  internal abstract partial class PagedQueryResults<TInterface, TItem> : IPagedQueryResults<TInterface, TItem> {

    protected PagedQueryResults(Query query, string endpoint, string value, int? limit = null, int? offset = null) {
      if (query    == null) throw new ArgumentNullException(nameof(query));
      if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
      this._query     = query;
      this._endpoint  = endpoint;
      this._value     = value;
      this.Limit      = limit;
      this.Offset     = 0;
      this.NextOffset = offset;
    }

    public int? Limit { get; set; }

    public abstract TInterface Next();

    public int? NextOffset { get; set; }

    public int Offset { get; private set; }

    public abstract TInterface Previous();

#if NETFX_LT_4_5
    public abstract IEnumerable<TItem> Results { get; }
#else
    public abstract IReadOnlyList<TItem> Results { get; }
#endif

    public abstract int TotalResults { get; }

    private readonly Query  _query;
    private readonly string _endpoint;
    private readonly string _value;

    protected abstract string FullExtraText();

    protected string NextResponse(int lastResultCount) {
      this.UpdateOffset(lastResultCount);
      return this._query.PerformRequest(this._endpoint, this._value, this.FullExtraText());
    }

    protected string PreviousResponse() {
      this.UpdateOffset();
      return this._query.PerformRequest(this._endpoint, this._value, this.FullExtraText());
    }

    private void UpdateOffset() {
      if (this.NextOffset.HasValue) {
        this.Offset = this.NextOffset.Value;
        this.NextOffset = null;
      }
      else {
        var limit = Math.Min(this.Limit.GetValueOrDefault(Query.DefaultPageSize), Query.MaximumPageSize);
        if (limit < 1)
          limit = Query.DefaultPageSize;
        this.Offset -= limit;
      }
      if (this.Offset < 0)
        this.Offset = 0;
    }

    private void UpdateOffset(int lastResultCount) {
      if (this.NextOffset.HasValue) {
        this.Offset = this.NextOffset.Value;
        this.NextOffset = null;
      }
      else
        this.Offset += lastResultCount;
      if (this.Offset < 0)
        this.Offset = 0;
    }

  }

}
