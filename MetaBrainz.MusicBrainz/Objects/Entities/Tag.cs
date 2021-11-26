using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Tag : JsonBasedObject, ITag {

  public Tag(string name) {
    this.Name = name;
  }

  public string Name { get; }

  public int? VoteCount { get; set; }

  public override string ToString() {
    var text = this.Name;
    if (this.VoteCount is not null) {
      text += $" (votes: {this.VoteCount})";
    }
    return text;
  }

}
