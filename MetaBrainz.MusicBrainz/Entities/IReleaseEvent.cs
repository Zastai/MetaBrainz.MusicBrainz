namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A release event.</summary>
  public interface IReleaseEvent {

    /// <summary>The area where the release event took place.</summary>
    IArea Area { get; }

    /// <summary>The date the release event took place.</summary>
    PartialDate Date { get; }

  }

}
