using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class NameCredit : JsonBasedObject, INameCredit {

    public IArtist? Artist { get; set; }

    public string? JoinPhrase { get; set; }

    public string? Name { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.Artist != null && this.Artist.Name != this.Name)
        text += $"[{this.Artist} as “{this.Name}”]";
      else if (this.Name != null)
        text += this.Name;
      if (this.JoinPhrase != null)
        text += this.JoinPhrase;
      return text;
    }

  }

}
