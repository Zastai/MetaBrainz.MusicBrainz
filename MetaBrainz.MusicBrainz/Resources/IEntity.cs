namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>An item identified by an some identifier.</summary>
  public interface IEntity : IResource {

    /// <summary>The identifier that identifies this entity.</summary>
    string Id { get; }

  }

}
