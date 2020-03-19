using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal abstract class SearchResults<TInterface>
  : PagedQueryResults<ISearchResults<TInterface>, TInterface, SearchResults<TInterface>.JSON>,
    ISearchResults<TInterface>
  where TInterface : ISearchResult {

    protected SearchResults(Query query, string endpoint, string queryString, int? limit = null, int? offset = null)
    : base(query, endpoint, null, limit, offset) {
      this._queryString = queryString;
    }

    private readonly string _queryString;

    public DateTime? Created => this.CurrentResult?.Created;

    protected sealed override ISearchResults<TInterface> Deserialize(string json) {
      this.CurrentResult = Query.Deserialize<JSON>(json);
      return this;
    }

    protected sealed override string FullExtraText() {
      var extra = "?query=";
      extra += Uri.EscapeDataString(this._queryString);
      if (this.Offset > 0)
        extra += $"&offset={this.Offset}";
      if (this.Limit.HasValue)
        extra += $"&limit={this.Limit}";
      return extra;
    }

    public sealed override int TotalResults => this.CurrentResult?.Count ?? 0;

    public override IReadOnlyDictionary<string, object?>? UnhandledProperties => this.CurrentResult?.UnhandledProperties;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    internal sealed class JSON : JsonBasedObject {

      [JsonPropertyName("count")]
      public int Count { get; set; }

      [JsonPropertyName("created")]
      public DateTime? Created { get; set; }

      [JsonPropertyName("offset")]
      public int Offset { get; set; }

      [JsonPropertyName("annotations")]
      public Annotation[]? Annotations { get; set; }

      [JsonPropertyName("areas")]
      public Area[]? Areas { get; set; }

      [JsonPropertyName("artists")]
      public Artist[]? Artists { get; set; }

      [JsonPropertyName("cdstubs")]
      public CdStub[]? CdStubs { get; set; }

      [JsonPropertyName("events")]
      public Event[]? Events { get; set; }

      [JsonPropertyName("instruments")]
      public Instrument[]? Instruments { get; set; }

      [JsonPropertyName("labels")]
      public Label[]? Labels { get; set; }

      [JsonPropertyName("places")]
      public Place[]? Places { get; set; }

      [JsonPropertyName("recordings")]
      public Recording[]? Recordings { get; set; }

      [JsonPropertyName("release-groups")]
      public ReleaseGroup[]? ReleaseGroups { get; set; }

      [JsonPropertyName("releases")]
      public Release[]? Releases { get; set; }

      [JsonPropertyName("series")]
      public Series[]? Series { get; set; }

      [JsonPropertyName("tags")]
      public Tag[]? Tags { get; set; }

      [JsonPropertyName("urls")]
      public Url[]? Urls { get; set; }

      [JsonPropertyName("works")]
      public Work[]? Works { get; set; }

    }

  }

}
