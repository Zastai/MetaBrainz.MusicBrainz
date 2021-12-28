using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Submissions;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

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
  /// <param name="items">
  /// The items to add to <paramref name="collection"/>. They should be of the appropriate type for the collection.
  /// </param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, ICollection collection, params IEntity[] items)
    => Utils.ResultOf(this.AddToCollectionAsync(client, collection, items));

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
  public Task<string> AddToCollectionAsync(string client, Guid collection, EntityType entityType, params Guid[] items) {
    var submission = new ModifyCollection(HttpMethod.Put, client, collection, entityType).Add(items);
    return this.PerformSubmissionAsync(submission);
  }

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
  public Task<string> AddToCollectionAsync(string client, ICollection collection, params Guid[] items) {
    var submission = new ModifyCollection(HttpMethod.Put, client, collection.Id, collection.ContentType).Add(items);
    return this.PerformSubmissionAsync(submission);
  }

  /// <summary>Adds the specified items to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="items"/> to.</param>
  /// <param name="items">
  /// The items to add to <paramref name="collection"/>. They should be of the appropriate type for the collection.
  /// </param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, params IEntity[] items) {
    var submission = new ModifyCollection(HttpMethod.Put, client, collection.Id, collection.ContentType).Add(items);
    return this.PerformSubmissionAsync(submission);
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
  public string RemoveFromCollection(string client, Guid collection, EntityType entityType, params Guid[] items)
    => Utils.ResultOf(this.RemoveFromCollectionAsync(client, collection, entityType, items));

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
  /// <param name="items">
  /// The items to remove from <paramref name="collection"/>. They should be of the appropriate type for the collection.
  /// </param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, ICollection collection, params IEntity[] items)
    => Utils.ResultOf(this.RemoveFromCollectionAsync(client, collection, items));

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
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, EntityType entityType, params Guid[] items) {
    var submission = new ModifyCollection(HttpMethod.Delete, client, collection, entityType).Add(items);
    return this.PerformSubmissionAsync(submission);
  }

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
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, params Guid[] items) {
    var submission = new ModifyCollection(HttpMethod.Delete, client, collection.Id, collection.ContentType).Add(items);
    return this.PerformSubmissionAsync(submission);
  }

  /// <summary>Removes the specified items from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="items"/> from.</param>
  /// <param name="items">
  /// The items to remove from <paramref name="collection"/>. They should be of the appropriate type for the collection.
  /// </param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, params IEntity[] items) {
    var submission = new ModifyCollection(HttpMethod.Delete, client, collection.Id, collection.ContentType).Add(items);
    return this.PerformSubmissionAsync(submission);
  }

}
