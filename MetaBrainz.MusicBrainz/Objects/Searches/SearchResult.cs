using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class SearchResult<T>(T item, byte score) : ISearchResult<T> {

  public T Item { get; } = item;

  public byte Score { get; } = score;

  public override string ToString() => $"[Score: {this.Score}] {this.Item}";

}
