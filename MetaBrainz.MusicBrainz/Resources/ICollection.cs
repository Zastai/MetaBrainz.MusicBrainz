namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A collection.</summary>
  public interface ICollection : IMbEntity, ITypedResource {

    /// <summary>The name of the editor who created the collection.</summary>
    string Editor { get; }

    /// <summary>The type of entity stored in the collection.</summary>
    string EntityType { get; }

    /// <summary>The name of the collection.</summary>
    string Name { get; }

    /// <summary>The areas in the collection.</summary>
    /// <remarks>Applies only when <see cref="EntityType"/> is &quot;area&quot;.</remarks>
    IResourceList<IArea> Areas { get; }

    /// <summary>The artists in the collection.</summary>
    /// <remarks>Applies only when <see cref="EntityType"/> is &quot;artist&quot;.</remarks>
    IResourceList<IArtist> Artists { get; }

    /// <summary>The events in the collection.</summary>
    /// <remarks>Applies only when <see cref="EntityType"/> is &quot;event&quot;.</remarks>
    IResourceList<IEvent> Events { get; }

    /// <summary>The instruments in the collection.</summary>
    /// <remarks>Applies only when <see cref="EntityType"/> is &quot;instrument&quot;.</remarks>
    IResourceList<IInstrument> Instruments { get; }

    /// <summary>The labels in the collection.</summary>
    /// <remarks>Applies only when <see cref="EntityType"/> is &quot;label&quot;.</remarks>
    IResourceList<ILabel> Labels { get; }

    /// <summary>The places in the collection.</summary>
    /// <remarks>Applies only when <see cref="EntityType"/> is &quot;place&quot;.</remarks>
    IResourceList<IPlace> Places { get; }

    /// <summary>The recordings in the collection.</summary>
    /// <remarks>Applies only when <see cref="EntityType"/> is &quot;recording&quot;.</remarks>
    IResourceList<IRecording> Recordings { get; }

    /// <summary>The releases in the collection.</summary>
    /// <remarks>Applies only when <see cref="EntityType"/> is &quot;release&quot;.</remarks>
    IResourceList<IRelease> Releases { get; }

    /// <summary>The release groups in the collection.</summary>
    /// <remarks>Applies only when <see cref="EntityType"/> is &quot;release-group&quot;.</remarks>
    IResourceList<IReleaseGroup> ReleaseGroups { get; }

    /// <summary>The series in the collection.</summary>
    /// <remarks>Applies only when <see cref="EntityType"/> is &quot;series&quot;.</remarks>
    IResourceList<ISeries> Series { get; }

    /// <summary>The works in the collection.</summary>
    /// <remarks>Applies only when <see cref="EntityType"/> is &quot;work&quot;.</remarks>
    IResourceList<IWork> Works { get; }

  }

}
