using System.Collections.Generic;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal abstract class BrowseResults<TResult, TResultObject>
  : PagedQueryResults<IBrowseResults<TResult>, TResult, TResultObject>,
    IBrowseResults<TResult>
  where TResult : IEntity
  where TResultObject : BrowseResults<TResult, TResultObject>.ResultObject {

    protected BrowseResults(Query query, string endpoint, string? value, string extra, int? limit = null, int? offset = null)
    : base(query, endpoint, value, limit, offset) {
      this._extra = extra;
    }

    private readonly string _extra;

    protected override IBrowseResults<TResult> Deserialize(string json) {
      this.CurrentResult = Query.Deserialize<TResultObject>(json);
      return this;
    }

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

    public override int TotalResults => this.CurrentResult?.Count ?? 0;

    public override IReadOnlyDictionary<string, object?>? UnhandledProperties => this.CurrentResult?.UnhandledProperties;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public abstract class ResultObject : JsonBasedObject {

      public abstract int Count { get; set; }

      public abstract int Offset { get; set; }

    }

  }

}
