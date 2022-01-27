using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  #region Adding Items

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, Guid collection, ISeries series)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, series));

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, Guid collection, params ISeries[] series)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, series));

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, Guid collection, IEnumerable<ISeries> series)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, series));

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, ICollection collection, ISeries series)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, series));

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, ICollection collection, params ISeries[] series)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, series));

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, ICollection collection, IEnumerable<ISeries> series)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, series));

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="series"/> to.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, CancellationToken cancellationToken,
                                           params ISeries[] series)
    => this.AddToCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, ISeries series,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, params ISeries[] series)
    => this.AddToCollectionAsync(client, collection, EntityType.Series, series);

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, IEnumerable<ISeries> series,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="series"/> to.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, CancellationToken cancellationToken,
                                           params ISeries[] series)
    => this.AddToCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, ISeries series,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, params ISeries[] series)
    => this.AddToCollectionAsync(client, collection, EntityType.Series, series);

  /// <summary>Adds the specified series to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="series"/> to.</param>
  /// <param name="series">The series to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, IEnumerable<ISeries> series,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  #endregion

  #region Removing Items

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, Guid collection, ISeries series)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, series));

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, Guid collection, params ISeries[] series)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, series));

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, Guid collection, IEnumerable<ISeries> series)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, series));

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, ICollection collection, ISeries series)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, series));

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, ICollection collection, params ISeries[] series)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, series));

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, ICollection collection, IEnumerable<ISeries> series)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, series));

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="series"/> from.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, CancellationToken cancellationToken,
                                                params ISeries[] series)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, ISeries series,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, params ISeries[] series)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Series, series);

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, IEnumerable<ISeries> series,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="series"/> from.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, CancellationToken cancellationToken,
                                                params ISeries[] series)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, ISeries series,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, params ISeries[] series)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Series, series);

  /// <summary>Removes the specified series from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="series"/> from.</param>
  /// <param name="series">The series to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, IEnumerable<ISeries> series,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Series, series, cancellationToken);

  #endregion

}
