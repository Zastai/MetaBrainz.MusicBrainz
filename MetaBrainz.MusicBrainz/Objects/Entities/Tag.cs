using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class Tag : SearchResult, IFoundTag {

    public Tag(string name, int voteCount) {
      this.Name = name;
      this.VoteCount = voteCount;
    }

    public string Name { get; }

    public int VoteCount { get; }

    public override string ToString() {
      var text = string.Empty;
      if (this.SearchScore.HasValue)
        text += $"[Score: {this.SearchScore.Value}] ";
      text += $"{this.Name} (votes: {this.VoteCount})";
      return text;
    }

  }

}
