using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class CoverArtArchive : JsonBasedObject, ICoverArtArchive {

    public bool Artwork { get; set; }

    public bool Back { get; set; }

    public int Count { get; set; }

    public bool Darkened { get; set; }

    public bool Front { get; set; }

    public override string ToString() {
      if (this.Darkened)
        return "<cover art taken down>";
      return (this.Count == 0) ? "<no cover art>" : $"{this.Count} item(s)";
    }

  }

}
