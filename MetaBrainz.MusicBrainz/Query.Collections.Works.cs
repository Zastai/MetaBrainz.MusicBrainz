using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  #region Adding Items

  /// <summary>Adds the specified works to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="works"/> to.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="works">The works to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, CancellationToken cancellationToken,
                                           params IWork[] works)
    => this.AddToCollectionAsync(client, collection, EntityType.Work, works, cancellationToken);

  /// <summary>Adds the specified work to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="work"/> to.</param>
  /// <param name="work">The work to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, IWork work,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Work, work, cancellationToken);

  /// <summary>Adds the specified works to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="works"/> to.</param>
  /// <param name="works">The works to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, params IWork[] works)
    => this.AddToCollectionAsync(client, collection, EntityType.Work, works);

  /// <summary>Adds the specified works to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="works"/> to.</param>
  /// <param name="works">The works to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, IEnumerable<IWork> works,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Work, works, cancellationToken);

  /// <summary>Adds the specified works to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="works"/> to.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="works">The works to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, CancellationToken cancellationToken,
                                           params IWork[] works)
    => this.AddToCollectionAsync(client, collection, EntityType.Work, works, cancellationToken);

  /// <summary>Adds the specified work to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="work"/> to.</param>
  /// <param name="work">The work to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, IWork work,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Work, work, cancellationToken);

  /// <summary>Adds the specified works to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="works"/> to.</param>
  /// <param name="works">The works to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, params IWork[] works)
    => this.AddToCollectionAsync(client, collection, EntityType.Work, works);

  /// <summary>Adds the specified works to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="works"/> to.</param>
  /// <param name="works">The works to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, IEnumerable<IWork> works,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Work, works, cancellationToken);

  #endregion

  #region Removing Items

  /// <summary>Removes the specified works from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="works"/> from.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="works">The works to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, CancellationToken cancellationToken,
                                                params IWork[] works)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Work, works, cancellationToken);

  /// <summary>Removes the specified work from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="work"/> from.</param>
  /// <param name="work">The work to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, IWork work,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Work, work, cancellationToken);

  /// <summary>Removes the specified works from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="works"/> from.</param>
  /// <param name="works">The works to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, params IWork[] works)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Work, works);

  /// <summary>Removes the specified works from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="works"/> from.</param>
  /// <param name="works">The works to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, IEnumerable<IWork> works,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Work, works, cancellationToken);

  /// <summary>Removes the specified works from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="works"/> from.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="works">The works to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, CancellationToken cancellationToken,
                                                params IWork[] works)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Work, works, cancellationToken);

  /// <summary>Removes the specified work from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="work"/> from.</param>
  /// <param name="work">The work to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, IWork work,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Work, work, cancellationToken);

  /// <summary>Removes the specified works from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="works"/> from.</param>
  /// <param name="works">The works to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, params IWork[] works)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Work, works);

  /// <summary>Removes the specified works from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="works"/> from.</param>
  /// <param name="works">The works to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, IEnumerable<IWork> works,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Work, works, cancellationToken);

  #endregion

}
