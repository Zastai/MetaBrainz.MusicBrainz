using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>Label information for a release.</summary>
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface ILabelInfo {

    /// <summary>The catalog number (specific to <see cref="Label"/>) associated with the release.</summary>
    string CatalogNumber { get; }

    /// <summary>The label associated with the release.</summary>
    ILabel  Label { get; }

  }

}
