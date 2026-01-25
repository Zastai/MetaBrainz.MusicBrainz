using System;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Create a helper object to be used to add or remove areas from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public AreaCollectionModification ModifyAreaCollection(string client, Guid id) => new(this, client, id);

  /// <summary>Create an object to be used to add or remove areas from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public AreaCollectionModification ModifyAreaCollection(string client, ICollection collection) {
    if (collection.ContentType != EntityType.Area) {
      throw new ArgumentException("The specified collection is not an area collection.", nameof(collection));
    }
    return new AreaCollectionModification(this, client, collection.Id);
  }

  /// <summary>Create a helper object to be used to add or remove artists from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public ArtistCollectionModification ModifyArtistCollection(string client, Guid id) => new(this, client, id);

  /// <summary>Create an object to be used to add or remove artists from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public ArtistCollectionModification ModifyArtistCollection(string client, ICollection collection) {
    if (collection.ContentType != EntityType.Artist) {
      throw new ArgumentException("The specified collection is not an artist collection.", nameof(collection));
    }
    return new ArtistCollectionModification(this, client, collection.Id);
  }

  /// <summary>Create a helper object to be used to add or remove items from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <param name="type">The type of entity stored in the collection identified by <paramref name="id"/>.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public CollectionModification ModifyCollection(string client, Guid id, EntityType type) => new(this, client, id, type);

  /// <summary>Create an object to be used to add or remove items from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public CollectionModification ModifyCollection(string client, ICollection collection)
    => new(this, client, collection.Id, collection.ContentType);

  /// <summary>Create a helper object to be used to add or remove events from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public EventCollectionModification ModifyEventCollection(string client, Guid id) => new(this, client, id);

  /// <summary>Create an object to be used to add or remove events from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public EventCollectionModification ModifyEventCollection(string client, ICollection collection) {
    if (collection.ContentType != EntityType.Event) {
      throw new ArgumentException("The specified collection is not an events collection.", nameof(collection));
    }
    return new EventCollectionModification(this, client, collection.Id);
  }

  /// <summary>Create a helper object to be used to add or remove instruments from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public InstrumentCollectionModification ModifyInstrumentCollection(string client, Guid id) => new(this, client, id);

  /// <summary>Create an object to be used to add or remove instruments from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public InstrumentCollectionModification ModifyInstrumentCollection(string client, ICollection collection) {
    if (collection.ContentType != EntityType.Instrument) {
      throw new ArgumentException("The specified collection is not an instruments collection.", nameof(collection));
    }
    return new InstrumentCollectionModification(this, client, collection.Id);
  }

  /// <summary>Create a helper object to be used to add or remove labels from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public LabelCollectionModification ModifyLabelCollection(string client, Guid id) => new(this, client, id);

  /// <summary>Create an object to be used to add or remove labels from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public LabelCollectionModification ModifyLabelCollection(string client, ICollection collection) {
    if (collection.ContentType != EntityType.Label) {
      throw new ArgumentException("The specified collection is not a labels collection.", nameof(collection));
    }
    return new LabelCollectionModification(this, client, collection.Id);
  }

  /// <summary>Create a helper object to be used to add or remove places from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public PlaceCollectionModification ModifyPlaceCollection(string client, Guid id) => new(this, client, id);

  /// <summary>Create an object to be used to add or remove places from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public PlaceCollectionModification ModifyPlaceCollection(string client, ICollection collection) {
    if (collection.ContentType != EntityType.Place) {
      throw new ArgumentException("The specified collection is not a places collection.", nameof(collection));
    }
    return new PlaceCollectionModification(this, client, collection.Id);
  }

  /// <summary>Create a helper object to be used to add or remove recordings from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public RecordingCollectionModification ModifyRecordingCollection(string client, Guid id) => new(this, client, id);

  /// <summary>Create an object to be used to add or remove recordings from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public RecordingCollectionModification ModifyRecordingCollection(string client, ICollection collection) {
    if (collection.ContentType != EntityType.Recording) {
      throw new ArgumentException("The specified collection is not a recordings collection.", nameof(collection));
    }
    return new RecordingCollectionModification(this, client, collection.Id);
  }

  /// <summary>Create a helper object to be used to add or remove releases from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public ReleaseCollectionModification ModifyReleaseCollection(string client, Guid id) => new(this, client, id);

  /// <summary>Create an object to be used to add or remove releases from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public ReleaseCollectionModification ModifyReleaseCollection(string client, ICollection collection) {
    if (collection.ContentType != EntityType.Release) {
      throw new ArgumentException("The specified collection is not a releases collection.", nameof(collection));
    }
    return new ReleaseCollectionModification(this, client, collection.Id);
  }

  /// <summary>Create a helper object to be used to add or remove release groups from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public ReleaseGroupCollectionModification ModifyReleaseGroupCollection(string client, Guid id) => new(this, client, id);

  /// <summary>Create an object to be used to add or remove release groups from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public ReleaseGroupCollectionModification ModifyReleaseGroupCollection(string client, ICollection collection) {
    if (collection.ContentType != EntityType.ReleaseGroup) {
      throw new ArgumentException("The specified collection is not a release groups collection.", nameof(collection));
    }
    return new ReleaseGroupCollectionModification(this, client, collection.Id);
  }

  /// <summary>Create a helper object to be used to add or remove series from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public SeriesCollectionModification ModifySeriesCollection(string client, Guid id) => new(this, client, id);

  /// <summary>Create an object to be used to add or remove series from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public SeriesCollectionModification ModifySeriesCollection(string client, ICollection collection) {
    if (collection.ContentType != EntityType.Series) {
      throw new ArgumentException("The specified collection is not a series collection.", nameof(collection));
    }
    return new SeriesCollectionModification(this, client, collection.Id);
  }

  /// <summary>Create a helper object to be used to add or remove works from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="id">The MBID of the collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public WorkCollectionModification ModifyWorkCollection(string client, Guid id) => new(this, client, id);

  /// <summary>Create an object to be used to add or remove works from a collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to modify.</param>
  /// <returns>A collection modification helper.</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  public WorkCollectionModification ModifyWorkCollection(string client, ICollection collection) {
    if (collection.ContentType != EntityType.Work) {
      throw new ArgumentException("The specified collection is not a works collection.", nameof(collection));
    }
    return new WorkCollectionModification(this, client, collection.Id);
  }

}
