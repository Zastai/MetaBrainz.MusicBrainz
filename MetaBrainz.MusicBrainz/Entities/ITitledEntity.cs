using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>An entity with a title.</summary>
  public interface ITitledEntity {

    /// <summary>The aliases for this entity.</summary>
    IEnumerable<IAlias> Aliases { get; }

    /// <summary>The text used to distinguish this entity from others with the same title.</summary>
    string Disambiguation { get; }

    /// <summary>The entity's title.</summary>
    string Title { get; }

  }

}
