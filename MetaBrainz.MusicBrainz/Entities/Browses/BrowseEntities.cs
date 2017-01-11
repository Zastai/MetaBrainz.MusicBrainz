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
      this._query    = query;
      this._endpoint = endpoint;
      this._value    = value;
      this._extra    = extra;
      this.Limit     = limit;
      this.Offset    = offset.GetValueOrDefault(0);
    }

    public int? Limit { get; set; }

    public abstract IBrowseEntities<T> Next();

#if NETFX_GE_4_5
    public abstract Task<IBrowseEntities<T>> NextAsync();
#endif

    public int Offset { get; set; }

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

    protected string NextResponse() {
      // TODO: Maybe adjust offset
      return this._query.PerformRequest(this._endpoint, this._value, this.FullExtraText());
    }

    protected string PreviousResponse() {
      // TODO: Maybe adjust offset
      return this._query.PerformRequest(this._endpoint, this._value, this.FullExtraText());
    }

#if NETFX_GE_4_5


    protected Task<string> NextResponseAsync() {
      // TODO: Maybe adjust offset
      return this._query.PerformRequestAsync(this._endpoint, this._value, this.FullExtraText());
    }

    protected Task<string> PreviousResponseAsync() {
      // TODO: Maybe adjust offset
      return this._query.PerformRequestAsync(this._endpoint, this._value, this.FullExtraText());
    }

#endif

  }

}
