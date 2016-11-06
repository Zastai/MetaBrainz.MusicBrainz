using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class CoverArtArchive : ICoverArtArchive {

    [JsonProperty("artwork")]
    public bool Artwork { get; private set; }

    [JsonProperty("back")]
    public bool Back { get; private set; }

    [JsonProperty("count")]
    public int Count { get; private set; }

    [JsonProperty("darkened")]
    public bool? Darkened { get; private set; }

    [JsonProperty("front")]
    public bool Front { get; private set; }

    public override string ToString() {
      if (this.Darkened.GetValueOrDefault())
        return "<cover art taken down>";
      return (this.Count == 0) ? "<no cover art>" : $"{this.Count} item(s)";
    }

  }

}
