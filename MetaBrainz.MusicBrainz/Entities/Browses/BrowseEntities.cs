using System;
using System.Collections.Generic;

#if NETFX_GE_4_5
using System.Threading.Tasks;
#endif

namespace MetaBrainz.MusicBrainz.Entities.Browses {

  internal abstract class BrowseEntities<T> : IBrowseEntities<T> where T : IEntity {

    protected BrowseEntities(Query query, string endpoint, string value, string extra, int? limit = null, int? offset = null) {
      if (query    == null) throw new ArgumentNullException(nameof(query));
      if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
      this._query     = query;
      this._endpoint  = endpoint;
      this._value     = value;
      this._extra     = extra;
      this.Limit      = limit;
      this.Offset     = 0;
      this.NextOffset = offset;
    }

    public int? Limit { get; set; }

    public abstract IBrowseEntities<T> Next();

#if NETFX_GE_4_5
    public abstract Task<IBrowseEntities<T>> NextAsync();
#endif

    public int? NextOffset { get; set; }

    public int Offset { get; private set; }

    public abstract IBrowseEntities<T> Previous();

#if NETFX_GE_4_5
    public abstract Task<IBrowseEntities<T>> PreviousAsync();
#endif

#if NETFX_LT_4_5
    public abstract IEnumerable<T> Results { get; }
#else
    public abstract IReadOnlyList<T> Results { get; }
#endif

    public abstract int TotalResults { get; }

    private readonly Query  _query;
    private readonly string _endpoint;
    private readonly string _value;
    private readonly string _extra;

    private string FullExtraText() {
      var extra = this._extra;
      if (string.IsNullOrEmpty(extra))
        extra = $"?offset={this.Offset}";
      else
        extra += $"&offset={this.Offset}";
      if (this.Limit.HasValue)
        extra += $"&limit={this.Limit}";
      return extra;
    }

    protected string NextResponse(int lastResultCount) {
      this.UpdateOffset(lastResultCount);
      return this._query.PerformRequest(this._endpoint, this._value, this.FullExtraText());
    }

    protected string PreviousResponse() {
      this.UpdateOffset();
      return this._query.PerformRequest(this._endpoint, this._value, this.FullExtraText());
    }

#if NETFX_GE_4_5

    protected Task<string> NextResponseAsync(int lastResultCount) {
      this.UpdateOffset(lastResultCount);
      return this._query.PerformRequestAsync(this._endpoint, this._value, this.FullExtraText());
    }

    protected Task<string> PreviousResponseAsync() {
      this.UpdateOffset();
      return this._query.PerformRequestAsync(this._endpoint, this._value, this.FullExtraText());
    }

#endif

    private void UpdateOffset() {
      if (this.NextOffset.HasValue) {
        this.Offset = this.NextOffset.Value;
        this.NextOffset = null;
      }
      else {
        var limit = Math.Min(this.Limit.GetValueOrDefault(Query.DefaultBrowseLimit), Query.MaximumBrowseLimit);
        if (limit < 1)
          limit = Query.DefaultBrowseLimit;
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
