using System.Text;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Tag : JsonBasedObject, ITag {

  public required string Name { get; init; }

  public required int VoteCount { get; init; }

  public override string ToString() {
    var text = new StringBuilder();
    text.Append(this.Name);
    if (this.VoteCount is not 0) {
      text.Append(" (votes: ").Append(this.VoteCount).Append(')');
    }
    return text.ToString();
  }

}
