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

  /// <summary>Adds the specified recording to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="recording"/> to.</param>
  /// <param name="recording">The recording to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string AddToCollection(string client, Guid collection, IRecording recording)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, recording));

  /// <summary>Adds the specified recordings to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="recordings"/> to.</param>
  /// <param name="recordings">The recordings to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string AddToCollection(string client, Guid collection, params IRecording[] recordings)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, recordings));

  /// <summary>Adds the specified recordings to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="recordings"/> to.</param>
  /// <param name="recordings">The recordings to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string AddToCollection(string client, Guid collection, IEnumerable<IRecording> recordings)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, recordings));

  /// <summary>Adds the specified recording to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="recording"/> to.</param>
  /// <param name="recording">The recording to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string AddToCollection(string client, ICollection collection, IRecording recording)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, recording));

  /// <summary>Adds the specified recordings to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="recordings"/> to.</param>
  /// <param name="recordings">The recordings to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string AddToCollection(string client, ICollection collection, params IRecording[] recordings)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, recordings));

  /// <summary>Adds the specified recordings to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="recordings"/> to.</param>
  /// <param name="recordings">The recordings to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string AddToCollection(string client, ICollection collection, IEnumerable<IRecording> recordings)
    => AsyncUtils.ResultOf(this.AddToCollectionAsync(client, collection, recordings));

  /// <summary>Adds the specified recordings to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="recordings"/> to.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="recordings">The recordings to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, CancellationToken cancellationToken,
                                           params IRecording[] recordings)
    => this.AddToCollectionAsync(client, collection, EntityType.Recording, recordings, cancellationToken);

  /// <summary>Adds the specified recording to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="recording"/> to.</param>
  /// <param name="recording">The recording to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, IRecording recording,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Recording, recording, cancellationToken);

  /// <summary>Adds the specified recordings to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="recordings"/> to.</param>
  /// <param name="recordings">The recordings to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, params IRecording[] recordings)
    => this.AddToCollectionAsync(client, collection, EntityType.Recording, recordings);

  /// <summary>Adds the specified recordings to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to add <paramref name="recordings"/> to.</param>
  /// <param name="recordings">The recordings to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, IEnumerable<IRecording> recordings,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Recording, recordings, cancellationToken);

  /// <summary>Adds the specified recordings to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="recordings"/> to.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="recordings">The recordings to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, CancellationToken cancellationToken,
                                           params IRecording[] recordings)
    => this.AddToCollectionAsync(client, collection, EntityType.Recording, recordings, cancellationToken);

  /// <summary>Adds the specified recording to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="recording"/> to.</param>
  /// <param name="recording">The recording to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, IRecording recording,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Recording, recording, cancellationToken);

  /// <summary>Adds the specified recordings to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="recordings"/> to.</param>
  /// <param name="recordings">The recordings to add to <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, params IRecording[] recordings)
    => this.AddToCollectionAsync(client, collection, EntityType.Recording, recordings);

  /// <summary>Adds the specified recordings to the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to add <paramref name="recordings"/> to.</param>
  /// <param name="recordings">The recordings to add to <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> AddToCollectionAsync(string client, ICollection collection, IEnumerable<IRecording> recordings,
                                           CancellationToken cancellationToken = default)
    => this.AddToCollectionAsync(client, collection, EntityType.Recording, recordings, cancellationToken);

  #endregion

  #region Removing Items

  /// <summary>Removes the specified recording from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="recording"/> from.</param>
  /// <param name="recording">The recording to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string RemoveFromCollection(string client, Guid collection, IRecording recording)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, recording));

  /// <summary>Removes the specified recordings from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="recordings"/> from.</param>
  /// <param name="recordings">The recordings to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string RemoveFromCollection(string client, Guid collection, params IRecording[] recordings)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, recordings));

  /// <summary>Removes the specified recordings from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="recordings"/> from.</param>
  /// <param name="recordings">The recordings to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string RemoveFromCollection(string client, Guid collection, IEnumerable<IRecording> recordings)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, recordings));

  /// <summary>Removes the specified recording from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="recording"/> from.</param>
  /// <param name="recording">The recording to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string RemoveFromCollection(string client, ICollection collection, IRecording recording)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, recording));

  /// <summary>Removes the specified recordings from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="recordings"/> from.</param>
  /// <param name="recordings">The recordings to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string RemoveFromCollection(string client, ICollection collection, params IRecording[] recordings)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, recordings));

  /// <summary>Removes the specified recordings from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="recordings"/> from.</param>
  /// <param name="recordings">The recordings to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public string RemoveFromCollection(string client, ICollection collection, IEnumerable<IRecording> recordings)
    => AsyncUtils.ResultOf(this.RemoveFromCollectionAsync(client, collection, recordings));

  /// <summary>Removes the specified recordings from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="recordings"/> from.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="recordings">The recordings to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, CancellationToken cancellationToken,
                                                params IRecording[] recordings)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Recording, recordings, cancellationToken);

  /// <summary>Removes the specified recording from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="recording"/> from.</param>
  /// <param name="recording">The recording to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, IRecording recording,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Recording, recording, cancellationToken);

  /// <summary>Removes the specified recordings from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="recordings"/> from.</param>
  /// <param name="recordings">The recordings to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, params IRecording[] recordings)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Recording, recordings);

  /// <summary>Removes the specified recordings from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The MBID of the collection to remove <paramref name="recordings"/> from.</param>
  /// <param name="recordings">The recordings to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, IEnumerable<IRecording> recordings,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Recording, recordings, cancellationToken);

  /// <summary>Removes the specified recordings from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="recordings"/> from.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <param name="recordings">The recordings to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, CancellationToken cancellationToken,
                                                params IRecording[] recordings)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Recording, recordings, cancellationToken);

  /// <summary>Removes the specified recording from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="recording"/> from.</param>
  /// <param name="recording">The recording to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, IRecording recording,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Recording, recording, cancellationToken);

  /// <summary>Removes the specified recordings from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="recordings"/> from.</param>
  /// <param name="recordings">The recordings to remove from <paramref name="collection"/>.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, params IRecording[] recordings)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Recording, recordings);

  /// <summary>Removes the specified recordings from the specified collection.</summary>
  /// <param name="client">
  /// The ID of the client software making this request.<br/>
  /// This has to be the application's name and version number.
  /// The recommended format is &quot;<c>application-version</c>&quot;, where <c>version</c> does not contain a dash.<br/>
  /// </param>
  /// <param name="collection">The collection to remove <paramref name="recordings"/> from.</param>
  /// <param name="recordings">The recordings to remove from <paramref name="collection"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, ICollection collection, IEnumerable<IRecording> recordings,
                                                CancellationToken cancellationToken = default)
    => this.RemoveFromCollectionAsync(client, collection, EntityType.Recording, recordings, cancellationToken);

  #endregion

}
