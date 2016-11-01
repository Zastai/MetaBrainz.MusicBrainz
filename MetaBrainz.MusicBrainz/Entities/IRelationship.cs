using System;
using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IRelationship {

    IEnumerable<string> Attributes { get; }

    IDictionary<string, string> AttributeValues { get; }

    string Begin { get; }

    string Direction { get; }

    string End { get; }

    bool? Ended { get; }

    int? OrderingKey { get; }

    string SourceCredit { get; }

    IMbEntity Target { get; }

    string TargetCredit { get; }

    EntityType TargetType { get; }

    string TargetTypeText { get; }

    string Type { get; }

    Guid? TypeId { get; }

  }

}
