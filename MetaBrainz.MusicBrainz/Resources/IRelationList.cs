namespace MetaBrainz.MusicBrainz.Resources {

  public interface IRelationList : IResourceList<IRelation> {

    string TargetType { get; }

  }

}
