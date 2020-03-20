using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class CoverArtArchive : JsonBasedObject, ICoverArtArchive {

    [JsonPropertyName("artwork")]
    public bool Artwork { get; set; }

    [JsonPropertyName("back")]
    public bool Back { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("darkened")]
    public bool Darkened { get; set; }

    [JsonPropertyName("front")]
    public bool Front { get; set; }

    public override string ToString() {
      if (this.Darkened)
        return "<cover art taken down>";
      return (this.Count == 0) ? "<no cover art>" : $"{this.Count} item(s)";
    }

  }

}
