using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>Label information for a release.</summary>
  [PublicAPI]
  public interface ILabelInfo : IJsonBasedObject {

    /// <summary>The catalog number (specific to <see cref="Label"/>) associated with the release.</summary>
    string CatalogNumber { get; }

    /// <summary>The label associated with the release.</summary>
    ILabel  Label { get; }

  }

}
