using System.Text;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class NameCredit : JsonBasedObject, INameCredit {

  public IArtist? Artist { get; init; }

  public string? JoinPhrase { get; init; }

  public string? Name { get; init; }

  public override string ToString() {
    var text = new StringBuilder();
    if (this.Artist is not null && this.Name is not null && this.Artist.Name != this.Name) {
      text.Append('[').Append(this.Artist).Append(" as “").Append(this.Name).Append("”]");
    }
    else if (this.Artist is not null) {
      text.Append(this.Artist);
    }
    else if (this.Name is not null) {
      text.Append(this.Name);
    }
    if (this.JoinPhrase is not null) {
      text.Append(this.JoinPhrase);
    }
    return text.ToString();
  }

}
