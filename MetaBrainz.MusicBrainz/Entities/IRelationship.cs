using System;
using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A relationship between two MusicBrainz entities.</summary>
  public interface IRelationship {

    /// <summary>The attributes attached to the relationship (if any). These values are keys into <see cref="AttributeValues"/>.</summary>
    IEnumerable<string> Attributes { get; }

    /// <summary>The attribute values attached to the relationship (if any).</summary>
    IDictionary<string, string> AttributeValues { get; }

    /// <summary>The date the relationship began.</summary>
    PartialDate Begin { get; }

    /// <summary>The direction of the relationship (forward/backward).</summary>
    string Direction { get; }

    /// <summary>The date the relationship ended.</summary>
    PartialDate End { get; }

    /// <summary>Flag indicating whether or not the relationship has ended.</summary>
    bool Ended { get; }

    /// <summary>An optional ordering key for the relationships.</summary>
    int? OrderingKey { get; }

    /// <summary>An optional alternate name for the source of the relationship.</summary>
    string SourceCredit { get; }

    /// <summary>The target of the relationship.</summary>
    IEntity Target { get; }

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

  }

}
