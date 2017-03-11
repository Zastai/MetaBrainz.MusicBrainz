using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Submissions;

namespace MetaBrainz.MusicBrainz {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public sealed partial class Query {

    #region Adding Items

    /// <summary>Adds the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="entityType">The type of entity stored in the collection identified by <paramref name="collection"/>.</param>
    /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, EntityType entityType, params Guid[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, entityType).Add(items));

    /// <summary>Adds the specified areas to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The areas to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IArea[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, EntityType.Area).Add(items));

    /// <summary>Adds the specified artists to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The artists to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IArtist[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, EntityType.Artist).Add(items));

    /// <summary>Adds the specified events to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The events to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IEvent[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, EntityType.Event).Add(items));

    /// <summary>Adds the specified instruments to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The instruments to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IInstrument[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, EntityType.Instrument).Add(items));

    /// <summary>Adds the specified labels to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The labels to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params ILabel[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, EntityType.Label).Add(items));

    /// <summary>Adds the specified places to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The places to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IPlace[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, EntityType.Place).Add(items));

    /// <summary>Adds the specified recordings to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The recordings to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IRecording[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, EntityType.Recording).Add(items));

    /// <summary>Adds the specified releases to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The releases to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IRelease[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, EntityType.Release).Add(items));

    /// <summary>Adds the specified release groups to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The release groups to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IReleaseGroup[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, EntityType.ReleaseGroup).Add( items));

    /// <summary>Adds the specified series to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The series to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params ISeries[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, EntityType.Series).Add(items));

    /// <summary>Adds the specified works to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The works to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IWork[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, EntityType.Work).Add(items));

    /// <summary>Adds the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> and/or <paramref name="collection"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, ICollection collection, params Guid[] items) {
      if (collection == null) throw new ArgumentNullException(nameof(collection));
      return this.PerformSubmission(new ModifyCollection("PUT", client, collection.MbId, collection.ContentType).Add(items));
    }

    /// <summary>Adds the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The items to add to <paramref name="collection"/>. They should be of the appropriate type for the collection.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> and/or <paramref name="collection"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, ICollection collection, params IEntity[] items) {
      if (collection == null) throw new ArgumentNullException(nameof(collection));
      return this.PerformSubmission(new ModifyCollection("PUT", client, collection.MbId, collection.ContentType).Add(items));
    }

    #endregion

    #region Removing Items

    /// <summary>Removes the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="entityType">The entity type for the collection identified by <paramref name="collection"/>.</param>
    /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, EntityType entityType, params Guid[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, entityType).Add(items));

    /// <summary>Removes the specified areas to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The areas to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IArea[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, EntityType.Area).Add(items));

    /// <summary>Removes the specified artists to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The artists to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IArtist[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, EntityType.Artist).Add(items));

    /// <summary>Removes the specified events to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The events to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IEvent[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, EntityType.Event).Add(items));

    /// <summary>Removes the specified instruments to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The instruments to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IInstrument[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, EntityType.Instrument).Add(items));

    /// <summary>Removes the specified labels to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The labels to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params ILabel[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, EntityType.Label).Add(items));

    /// <summary>Removes the specified places to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The places to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IPlace[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, EntityType.Place).Add(items));

    /// <summary>Removes the specified recordings to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The recordings to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IRecording[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, EntityType.Recording).Add(items));

    /// <summary>Removes the specified releases to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The releases to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IRelease[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, EntityType.Release).Add(items));

    /// <summary>Removes the specified release groups to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The release groups to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IReleaseGroup[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, EntityType.ReleaseGroup).Add( items));

    /// <summary>Removes the specified series to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The series to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params ISeries[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, EntityType.Series).Add(items));

    /// <summary>Removes the specified works to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The works to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IWork[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, EntityType.Work).Add(items));

    /// <summary>Removes the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> and/or <paramref name="collection"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, ICollection collection, params Guid[] items) {
      if (collection == null) throw new ArgumentNullException(nameof(collection));
      return this.PerformSubmission(new ModifyCollection("DELETE", client, collection.MbId, collection.ContentType).Add(items));
    }

    /// <summary>Removes the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The items to remove from <paramref name="collection"/>. They should be of the appropriate type for the collection.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> and/or <paramref name="collection"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, ICollection collection, params IEntity[] items) {
      if (collection == null) throw new ArgumentNullException(nameof(collection));
      return this.PerformSubmission(new ModifyCollection("DELETE", client, collection.MbId, collection.ContentType).Add(items));
    }

    #endregion

  }

}
