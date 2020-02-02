using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseCollections : BrowseResults<ICollection, BrowseCollections.JSON> {

    public BrowseCollections(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "collection", null, extra, limit, offset) {
    }

    public override IReadOnlyList<ICollection> Results => this.CurrentResult?.Results;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class JSON : ResultObject {

      [JsonPropertyName("collections")]
      public Collection[] Results { get; set; }

      [JsonPropertyName("collection-count")]
      public override int Count { get; set; }

      [JsonPropertyName("collection-offset")]
      public override int Offset { get; set; }

    }

  }

}
