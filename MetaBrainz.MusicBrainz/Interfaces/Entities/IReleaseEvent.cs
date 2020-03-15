using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A release event.</summary>
  [PublicAPI]
  public interface IReleaseEvent : IJsonBasedObject {

    /// <summary>The area where the release event took place.</summary>
    IArea? Area { get; }

    /// <summary>The date the release event took place.</summary>
    PartialDate? Date { get; }

  }

}
