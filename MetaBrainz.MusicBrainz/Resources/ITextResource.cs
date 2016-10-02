namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A resource based on a textual value.</summary>
  public interface ITextResource : IResource {

    /// <summary>The textual value for this resource.</summary>
    string Text { get; }

  }

}
