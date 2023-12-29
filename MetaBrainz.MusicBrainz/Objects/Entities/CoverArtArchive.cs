using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class CoverArtArchive : JsonBasedObject, ICoverArtArchive {

  public bool Artwork { get; init; }

  public bool Back { get; init; }

  public int Count { get; init; }

  public bool Darkened { get; init; }

  public bool Front { get; init; }

  public override string ToString() {
    if (this.Darkened) {
      return "<cover art taken down>";
    }
    return (this.Count == 0) ? "<no cover art>" : $"{this.Count} item(s)";
  }

}
