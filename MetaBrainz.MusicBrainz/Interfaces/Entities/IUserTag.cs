using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A tag set by the authenticated user.</summary>
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IUserTag {

    /// <summary>The name of the tag.</summary>
    string Name { get; }

  }

}
