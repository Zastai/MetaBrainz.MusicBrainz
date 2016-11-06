using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A relationship between two MusicBrainz entities.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IRelationship {

    /// <summary>The target area of the relationship, if applicable.</summary>
    IArea Area { get; }

    /// <summary>The target artist of the relationship, if applicable.</summary>
    IArtist Artist { get; }

    /// <summary>
    ///   The attributes attached to the relationship (if any).
    ///   These values may be keys into <see cref="AttributeCredits"/> and/or <see cref="AttributeValues"/>.
    /// </summary>
    IEnumerable<string> Attributes { get; }

    /// <summary>The credits attached to specified attributes of the relationship (if any).</summary>
    IDictionary<string, string> AttributeCredits { get; }

    /// <summary>The values attached to specified attributes of the relationship (if any).</summary>
    IDictionary<string, string> AttributeValues { get; }

    /// <summary>The date the relationship began.</summary>
    PartialDate Begin { get; }

    /// <summary>The direction of the relationship (forward/backward).</summary>
    string Direction { get; }

    /// <summary>The date the relationship ended.</summary>
    PartialDate End { get; }

    /// <summary>Flag indicating whether or not the relationship has ended.</summary>
    bool Ended { get; }

    /// <summary>The target event of the relationship, if applicable.</summary>
    IEvent Event { get; }

    /// <summary>The target instrument of the relationship, if applicable.</summary>
    IInstrument Instrument { get; }

    /// <summary>The target label of the relationship, if applicable.</summary>
    ILabel Label { get; }

    /// <summary>An optional ordering key for the relationships.</summary>
    int? OrderingKey { get; }

    /// <summary>The target place of the relationship, if applicable.</summary>
    IPlace Place { get; }

    /// <summary>The target recording of the relationship, if applicable.</summary>
    IRecording Recording { get; }

    /// <summary>The target release of the relationship, if applicable.</summary>
    IRelease Release { get; }

    /// <summary>The target release group of the relationship, if applicable.</summary>
    IReleaseGroup ReleaseGroup { get; }

    /// <summary>The target series of the relationship, if applicable.</summary>
    ISeries Series { get; }

    /// <summary>An optional alternate name for the source of the relationship.</summary>
    string SourceCredit { get; }

    /// <summary>The target of the relationship.</summary>
    IRelatableEntity Target { get; }

    /// <summary>An optional alternate name for the target of the relationship.</summary>
    string TargetCredit { get; }

    /// <summary>The type of entity targeted by the relationship (as an enumeration value).</summary>
    EntityType TargetType { get; }

    /// <summary>The type of entity targeted by the relationship (as text).</summary>
    string TargetTypeText { get; }

    /// <summary>The relationship type.</summary>
    string Type { get; }

    /// <summary>The MBID for the relationship type, if applicable.</summary>
    Guid? TypeId { get; }

    /// <summary>The target URL of the relationship, if applicable.</summary>
    IUrl Url { get; }

    /// <summary>The target work of the relationship, if applicable.</summary>
    IWork Work { get; }

  }

}
