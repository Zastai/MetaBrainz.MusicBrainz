namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A resource with a name.</summary>
  public interface INamedResource : IResource {

    /// <summary>The aliases for this resource.</summary>
    IResourceList<IAlias> AliasList { get; }

    /// <summary>The text used to distinguish this resource from others with the same name.</summary>
    string Disambiguation { get; }

    /// <summary>The resource's name.</summary>
    string Name { get; }

    /// <summary>The resource's sort name.</summary>
    string SortName { get; }

  }

}
