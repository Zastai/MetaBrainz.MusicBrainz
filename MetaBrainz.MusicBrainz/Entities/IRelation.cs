using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IRelation : ITypedEntity {

    IEnumerable<string> Attributes { get; }

    IDictionary<string, string> AttributeValues { get; }

    string Begin { get; }

    string Direction { get; }

    string End { get; }

    bool? Ended { get; }

    IMbEntity Item { get; }

    int? OrderingKey { get; }

    string SourceCredit { get; }

    string TargetCredit { get; }

    string TargetType { get; }

  }

}
