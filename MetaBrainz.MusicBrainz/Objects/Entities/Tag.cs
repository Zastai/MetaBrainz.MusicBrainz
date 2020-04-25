using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class Tag : SearchResult, IFoundTag {

    public Tag(string name) {
      this.Name = name;
    }

    public string Name { get; }

    public int? VoteCount { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.SearchScore.HasValue)
        text += $"[Score: {this.SearchScore.Value}] ";
      text += this.Name;
      if (this.VoteCount.HasValue)
        text += $" (votes: {this.VoteCount})";
      return text;
    }

  }

}
