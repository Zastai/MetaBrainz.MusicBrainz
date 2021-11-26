using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class NameCredit : JsonBasedObject, INameCredit {

  public IArtist? Artist { get; set; }

  public string? JoinPhrase { get; set; }

  public string? Name { get; set; }

  public override string ToString() {
    var text = string.Empty;
    if (this.Artist is not null && this.Name is not null && this.Artist.Name != this.Name) {
      text += $"[{this.Artist} as “{this.Name}”]";
    }
    else if (this.Artist is not null) {
      text += this.Artist.ToString();
    }
    else if (this.Name is not null) {
      text += this.Name;
    }
    if (this.JoinPhrase is not null) {
      text += this.JoinPhrase;
    }
    return text;
  }

}
