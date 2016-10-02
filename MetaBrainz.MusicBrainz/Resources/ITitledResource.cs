namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A resource with a title.</summary>
  public interface ITitledResource : IResource {

    /// <summary>The aliases for this resource.</summary>
    IResourceList<IAlias> AliasList { get; }

    /// <summary>The text used to distinguish this resource from others with the same title.</summary>
    string Disambiguation { get; }

    /// <summary>The resource's title.</summary>
    string Title { get; }

  }

}
