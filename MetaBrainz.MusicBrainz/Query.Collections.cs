using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Submissions;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  #region Adding Items

  /// <summary>Adds the specified item to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="item"/> to.</param>
  /// <param name="entityType">The type of entity stored in the collection identified by <paramref name="collection"/>.</param>
  /// <param name="item">The MBID of the item to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, Guid collection, EntityType entityType, Guid item)
    => Utils.ResultOf(this.AddToCollectionAsync(client, collection, entityType, item));

  /// <summary>Adds the specified items to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
  /// <param name="entityType">The type of entity stored in the collection identified by <paramref name="collection"/>.</param>
  /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, Guid collection, EntityType entityType, params Guid[] items)
    => Utils.ResultOf(this.AddToCollectionAsync(client, collection, entityType, items));

  /// <summary>Adds the specified items to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
  /// <param name="entityType">The type of entity stored in the collection identified by <paramref name="collection"/>.</param>
  /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, Guid collection, EntityType entityType, IEnumerable<Guid> items)
    => Utils.ResultOf(this.AddToCollectionAsync(client, collection, entityType, items));

  /// <summary>Adds the specified item to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="item"/> to.</param>
  /// <param name="item">The MBID of the item to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, ICollection collection, Guid item)
    => Utils.ResultOf(this.AddToCollectionAsync(client, collection, item));

  /// <summary>Adds the specified items to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="items"/> to.</param>
  /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, ICollection collection, params Guid[] items)
    => Utils.ResultOf(this.AddToCollectionAsync(client, collection, items));

  /// <summary>Adds the specified items to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="items"/> to.</param>
  /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, ICollection collection, IEnumerable<Guid> items)
    => Utils.ResultOf(this.AddToCollectionAsync(client, collection, items));

  /// <summary>Adds the specified items to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
  /// <param name="entityType">The type of entity stored in the collection identified by <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, EntityType entityType,
                                           CancellationToken cancellationToken, params Guid[] items)
    => this.AddToCollectionAsync(client, collection, entityType, items, cancellationToken);

  /// <summary>Adds the specified item to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="item"/> to.</param>
  /// <param name="entityType">The type of entity stored in the collection identified by <paramref name="collection"/>.</param>
  /// <param name="item">The MBID of the item to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, EntityType entityType, Guid item,
                                           CancellationToken cancellationToken = new()) {
    var submission = new ModifyCollection(HttpMethod.Put, client, collection, entityType).Add(item);
    return this.PerformSubmissionAsync(submission, cancellationToken);
  }

  /// <summary>Adds the specified items to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
  /// <param name="entityType">The type of entity stored in the collection identified by <paramref name="collection"/>.</param>
  /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, EntityType entityType, params Guid[] items)
    => this.AddToCollectionAsync(client, collection, entityType, (IEnumerable<Guid>) items);

  private Task<string> AddToCollectionAsync(string client, Guid collection, EntityType entityType, IEntity item,
                                            CancellationToken cancellationToken = new()) {
    var submission = new ModifyCollection(HttpMethod.Put, client, collection, entityType).Add(item);
    return this.PerformSubmissionAsync(submission, cancellationToken);
  }

  /// <summary>Adds the specified items to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
  /// <param name="entityType">The type of entity stored in the collection identified by <paramref name="collection"/>.</param>
  /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, EntityType entityType, IEnumerable<Guid> items,
                                           CancellationToken cancellationToken = new()) {
    var submission = new ModifyCollection(HttpMethod.Put, client, collection, entityType).Add(items);
    return this.PerformSubmissionAsync(submission, cancellationToken);
  }

  private Task<string> AddToCollectionAsync(string client, Guid collection, EntityType entityType, IEnumerable<IEntity> items,
                                            CancellationToken cancellationToken = new()) {
    var submission = new ModifyCollection(HttpMethod.Put, client, collection, entityType).Add(items);
    return this.PerformSubmissionAsync(submission, cancellationToken);
  }

  /// <summary>Adds the specified items to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="items"/> to.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, CancellationToken cancellationToken,
                                           params Guid[] items)
    => this.AddToCollectionAsync(client, collection.Id, collection.ContentType, items, cancellationToken);

  private Task<string> AddToCollectionAsync(string client, ICollection collection, EntityType entityType, IEntity item,
                                            CancellationToken cancellationToken = new()) {
    var id = collection.Id;
    var type = collection.ContentType;
    if (type != entityType) {
      throw new ArgumentException($"Cannot add {entityType} items to a collection ({id}) of type {type}.", nameof(collection));
    }
    return this.AddToCollectionAsync(client, id, entityType, item, cancellationToken);
  }

  private Task<string> AddToCollectionAsync(string client, ICollection collection, EntityType entityType,
                                            IEnumerable<IEntity> items, CancellationToken cancellationToken = new()) {
    var id = collection.Id;
    var type = collection.ContentType;
    if (type != entityType) {
      throw new ArgumentException($"Cannot add {entityType} items to a collection ({id}) of type {type}.", nameof(collection));
    }
    return this.AddToCollectionAsync(client, id, entityType, items, cancellationToken);
  }

  /// <summary>Adds the specified item to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="item"/> to.</param>
  /// <param name="item">The MBID of the item to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, Guid item,
                                           CancellationToken cancellationToken = new())
    => this.AddToCollectionAsync(client, collection.Id, collection.ContentType, item, cancellationToken);

  /// <summary>Adds the specified items to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="items"/> to.</param>
  /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, params Guid[] items)
    => this.AddToCollectionAsync(client, collection.Id, collection.ContentType, items);

  /// <summary>Adds the specified items to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="items"/> to.</param>
  /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, IEnumerable<Guid> items,
                                           CancellationToken cancellationToken = new())
    => this.AddToCollectionAsync(client, collection.Id, collection.ContentType, items, cancellationToken);

  #endregion

  #region Removing Items

  /// <summary>Removes the specified item from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="item"/> from.</param>
  /// <param name="entityType">The entity type for the collection identified by <paramref name="collection"/>.</param>
  /// <param name="item">The MBID of the item to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, Guid collection, EntityType entityType, Guid item)
    => Utils.ResultOf(this.RemoveFromCollectionAsync(client, collection, entityType, item));

  /// <summary>Removes the specified items from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
  /// <param name="entityType">The entity type for the collection identified by <paramref name="collection"/>.</param>
  /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, Guid collection, EntityType entityType, params Guid[] items)
    => Utils.ResultOf(this.RemoveFromCollectionAsync(client, collection, entityType, items));

  /// <summary>Removes the specified items from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
  /// <param name="entityType">The entity type for the collection identified by <paramref name="collection"/>.</param>
  /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, Guid collection, EntityType entityType, IEnumerable<Guid> items)
    => Utils.ResultOf(this.RemoveFromCollectionAsync(client, collection, entityType, items));

  /// <summary>Removes the specified item from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="item"/> from.</param>
  /// <param name="item">The MBID of the item to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, ICollection collection, Guid item)
    => Utils.ResultOf(this.RemoveFromCollectionAsync(client, collection, item));

  /// <summary>Removes the specified items from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="items"/> from.</param>
  /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, ICollection collection, params Guid[] items)
    => Utils.ResultOf(this.RemoveFromCollectionAsync(client, collection, items));

  /// <summary>Removes the specified items from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="items"/> from.</param>
  /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, ICollection collection, IEnumerable<Guid> items)
    => Utils.ResultOf(this.RemoveFromCollectionAsync(client, collection, items));

  /// <summary>Removes the specified items from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
  /// <param name="entityType">The entity type for the collection identified by <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, EntityType entityType,
                                                CancellationToken cancellationToken, params Guid[] items)
    => this.RemoveFromCollectionAsync(client, collection, entityType, items, cancellationToken);

  /// <summary>Removes the specified item from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="item"/> from.</param>
  /// <param name="entityType">The entity type for the collection identified by <paramref name="collection"/>.</param>
  /// <param name="item">The MBID of the item to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, EntityType entityType, Guid item,
                                                CancellationToken cancellationToken = new()) {
    var submission = new ModifyCollection(HttpMethod.Delete, client, collection, entityType).Add(item);
    return this.PerformSubmissionAsync(submission, cancellationToken);
  }

  /// <summary>Removes the specified items from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
  /// <param name="entityType">The entity type for the collection identified by <paramref name="collection"/>.</param>
  /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, EntityType entityType, params Guid[] items)
    => this.RemoveFromCollectionAsync(client, collection, entityType, (IEnumerable<Guid>) items);

  private Task<string> RemoveFromCollectionAsync(string client, Guid collection, EntityType entityType, IEntity item,
                                                 CancellationToken cancellationToken = new()) {
    var submission = new ModifyCollection(HttpMethod.Delete, client, collection, entityType).Add(item);
    return this.PerformSubmissionAsync(submission, cancellationToken);
  }

  /// <summary>Removes the specified items from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
  /// <param name="entityType">The entity type for the collection identified by <paramref name="collection"/>.</param>
  /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, EntityType entityType, IEnumerable<Guid> items,
                                                CancellationToken cancellationToken = new()) {
    var submission = new ModifyCollection(HttpMethod.Delete, client, collection, entityType).Add(items);
    return this.PerformSubmissionAsync(submission, cancellationToken);
  }

  private Task<string> RemoveFromCollectionAsync(string client, Guid collection, EntityType entityType, IEnumerable<IEntity> items,
                                                 CancellationToken cancellationToken = new()) {
    var submission = new ModifyCollection(HttpMethod.Delete, client, collection, entityType).Add(items);
    return this.PerformSubmissionAsync(submission, cancellationToken);
  }

  /// <summary>Removes the specified items from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="items"/> from.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, CancellationToken cancellationToken,
                                                params Guid[] items)
    => this.RemoveFromCollectionAsync(client, collection.Id, collection.ContentType, items, cancellationToken);

  private Task<string> RemoveFromCollectionAsync(string client, ICollection collection, EntityType entityType, IEntity item,
                                                 CancellationToken cancellationToken = new()) {
    var id = collection.Id;
    var type = collection.ContentType;
    if (type != entityType) {
      throw new ArgumentException($"Cannot remove {entityType} items from a collection ({id}) of type {type}.", nameof(collection));
    }
    return this.RemoveFromCollectionAsync(client, id, entityType, item, cancellationToken);
  }

  private Task<string> RemoveFromCollectionAsync(string client, ICollection collection, EntityType entityType,
                                                 IEnumerable<IEntity> items, CancellationToken cancellationToken = new()) {
    var id = collection.Id;
    var type = collection.ContentType;
    if (type != entityType) {
      throw new ArgumentException($"Cannot remove {entityType} items from a collection ({id}) of type {type}.", nameof(collection));
    }
    return this.RemoveFromCollectionAsync(client, id, entityType, items, cancellationToken);
  }

  /// <summary>Removes the specified item from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="item"/> from.</param>
  /// <param name="item">The MBID of the item to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, Guid item,
                                                CancellationToken cancellationToken = new())
    => this.RemoveFromCollectionAsync(client, collection.Id, collection.ContentType, item, cancellationToken);

  /// <summary>Removes the specified items from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="items"/> from.</param>
  /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, params Guid[] items)
    => this.RemoveFromCollectionAsync(client, collection.Id, collection.ContentType, items);

  /// <summary>Removes the specified items from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="items"/> from.</param>
  /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, IEnumerable<Guid> items,
                                                CancellationToken cancellationToken = new())
    => this.RemoveFromCollectionAsync(client, collection.Id, collection.ContentType, items, cancellationToken);

  #endregion

}
