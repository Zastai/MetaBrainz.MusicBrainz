namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A tagged resource.</summary>
  public interface ITaggedResource : IResource {

    /// <summary>The tags associated with this resource.</summary>
    IResourceList<ITag> TagList { get; }

    /// <summary>The tags set on this resource by the authenticated user.</summary>
    IResourceList<IUserTag> UserTagList { get; }

  }

}
