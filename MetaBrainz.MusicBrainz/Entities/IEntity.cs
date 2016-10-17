namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>An item identified by an some identifier.</summary>
  public interface IEntity {

    /// <summary>The identifier that identifies this entity.</summary>
    string ID { get; }

  }

}
