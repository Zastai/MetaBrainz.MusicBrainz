namespace MetaBrainz.MusicBrainz.Resources {

  public interface IRelation : ITypedResource {

    IResourceList<IRelationAttribute> AttributeList { get; }

    string Begin { get; }

    string Direction { get; }

    string End { get; }

    bool? Ended { get; }

    IMbEntity Item { get; }

    string OrderingKey { get; }

    string SourceCredit { get; }

    IRelationTarget Target { get; }

    string TargetCredit { get; }

  }

}
