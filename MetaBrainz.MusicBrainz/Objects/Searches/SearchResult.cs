using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class SearchResult<T> : ISearchResult<T> {

  public SearchResult(T item, byte score) {
    this.Item = item;
    this.Score = score;
  }

  public T Item { get; }

  public byte Score { get; }

  public override string ToString() => $"[Score: {this.Score}] {this.Item}";

}
