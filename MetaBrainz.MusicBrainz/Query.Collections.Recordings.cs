using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Submissions;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

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
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string AddToCollection(string client, Guid collection, params IRecording[] recordings)
    => Utils.ResultOf(this.AddToCollectionAsync(client, collection, recordings));

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
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> AddToCollectionAsync(string client, Guid collection, params IRecording[] recordings) {
    var submission = new ModifyCollection(HttpMethod.Put, client, collection, EntityType.Recording).Add(recordings);
    return this.PerformSubmissionAsync(submission);
  }

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
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public string RemoveFromCollection(string client, Guid collection, params IRecording[] recordings)
    => Utils.ResultOf(this.RemoveFromCollectionAsync(client, collection, recordings));

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
  /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
  /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
  public Task<string> RemoveFromCollectionAsync(string client, Guid collection, params IRecording[] recordings) {
    var submission = new ModifyCollection(HttpMethod.Delete, client, collection, EntityType.Recording).Add(recordings);
    return this.PerformSubmissionAsync(submission);
  }

}
