using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

using MetaBrainz.Common;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Submissions;

namespace MetaBrainz.MusicBrainz.Objects.Submissions.Collections;

/// <summary>A submission request for adding or removing items to a collection.</summary>
[PublicAPI]
public abstract class CollectionModificationBase<T, E> where T : CollectionModificationBase<T, E> where E : IEntity {

  #region Public API

  /// <summary>Includes an item in the request.</summary>
  /// <param name="item">The item to include in the request.</param>
  /// <returns>This collection modification helper.</returns>
  public T Include(E item) {
    this.IncludeItem(item);
    return (T) this;
  }

  /// <summary>Includes an item in the request.</summary>
  /// <param name="item">The MBID of the item to include in the request.</param>
  /// <returns>This collection modification helper.</returns>
  public T Include(Guid item) {
    this.IncludeItem(item);
    return (T) this;
  }

  /// <summary>Includes a number of items in the request.</summary>
  /// <param name="items">The items to include in the request.</param>
  /// <returns>This collection modification helper.</returns>
  public T Include(params IEnumerable<E> items) {
    foreach (var item in items) {
      this.IncludeItem(item);
    }
    return (T) this;
  }

  /// <summary>Includes a number of items in the request.</summary>
  /// <param name="items">The MBID of the items to include in the request.</param>
  /// <returns>This collection modification helper.</returns>
  public T Include(params IEnumerable<Guid> items) {
    foreach (var item in items) {
      this.Include(item);
    }
    return (T) this;
  }

  /// <summary>Includes a number of items in the request.</summary>
  /// <param name="items">The items to include in the request.</param>
  /// <returns>This collection modification helper.</returns>
  public T Include(params ReadOnlySpan<E> items) {
    foreach (var item in items) {
      this.IncludeItem(item);
    }
    return (T) this;
  }

  /// <summary>Includes a number of items in the request.</summary>
  /// <param name="items">The MBID of the items to include in the request.</param>
  /// <returns>This collection modification helper.</returns>
  public T Include(params ReadOnlySpan<Guid> items) {
    foreach (var item in items) {
      this.IncludeItem(item);
    }
    return (T) this;
  }

  /// <summary>Includes a number of items in the request.</summary>
  /// <param name="items">The items to include in the request.</param>
  /// <returns>This collection modification helper.</returns>
  public async Task<T> IncludeAsync(IAsyncEnumerable<E> items) {
    await foreach (var item in items) {
      this.IncludeItem(item.Id);
    }
    return (T) this;
  }

  /// <summary>Includes a number of items in the request.</summary>
  /// <param name="items">The MBID of the items to include in the request.</param>
  /// <returns>This collection modification helper.</returns>
  public async Task<T> IncludeAsync(IAsyncEnumerable<Guid> items) {
    await foreach (var item in items) {
      this.IncludeItem(item);
    }
    return (T) this;
  }

  /// <summary>Clears the list of items without submitting any operation.</summary>
  public void Clear() => this._request.Clear().Append(this._baseRequest);

  /// <summary>Submits a request to add the items to the collection asynchronously.</summary>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  /// <remarks>Calling this method clears the list of items.</remarks>
  public async Task<string> SubmitAdditionAsync(CancellationToken cancellationToken = default) {
    var submission = new Submission {
      Client = this._client,
      Entity = this._request.ToString(),
      Method = HttpMethod.Put,
    };
    this.Clear();
    return await this._query.PerformSubmissionAsync(submission, cancellationToken).ConfigureAwait(false);
  }

  /// <summary>Submits a request to delete the items from the collection asynchronously.</summary>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>A message describing the result (usually "OK").</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  /// <remarks>Calling this method clears the list of items.</remarks>
  public async Task<string> SubmitRemovalAsync(CancellationToken cancellationToken = default) {
    var submission = new Submission {
      Client = this._client,
      Entity = this._request.ToString(),
      Method = HttpMethod.Delete,
    };
    this.Clear();
    return await this._query.PerformSubmissionAsync(submission, cancellationToken).ConfigureAwait(false);
  }

  #endregion

  #region Internals

  private sealed class Submission : ISubmission {

    public required string Client { get; init; }

    public string ContentType => "text/plain";

    public required string Entity { get; init; }

    public required HttpMethod Method { get; init; }

    public string? RequestBody => null;

  }

  private readonly string _baseRequest;

  private readonly string _client;

  private readonly Guid _id;

  private readonly Query _query;

  private readonly StringBuilder _request = new(1024);

  private readonly EntityType _type;

  internal CollectionModificationBase(Query query, string client, Guid id, EntityType type) {
    if (string.IsNullOrWhiteSpace(client)) {
      throw new ArgumentException("The client ID must not be blank.", nameof(client));
    }
    this._client = client;
    this._id = id;
    this._query = query;
    this._type = type;
    var typeName = type switch {
      EntityType.Area => "areas",
      EntityType.Artist => "artists",
      EntityType.Event => "events",
      EntityType.Instrument => "instruments",
      EntityType.Label => "labels",
      EntityType.Place => "places",
      EntityType.Recording => "recordings",
      EntityType.Release => "releases",
      EntityType.ReleaseGroup => "release-groups",
      EntityType.Series => "series",
      EntityType.Work => "works",
      _ => throw new ArgumentOutOfRangeException(nameof(type), type, "The specified entity type cannot be stored in a collection.")
    };
    this._baseRequest = $"collection/{id:D}/{typeName}/";
    this._request.Append(this._baseRequest);
  }

  private void IncludeItem(Guid item) => this._request.Append(item.ToString("D")).Append(';');

  private void IncludeItem(E item) {
    var itemType = item.EntityType;
    if (itemType != this._type) {
      var msg = $"Cannot add an entity ({item.Id}) of type {itemType} to a collection ({this._id}) of type {this._type}.";
      throw new ArgumentException(msg, nameof(item));
    }
    this.IncludeItem(item.Id);
  }

  #endregion

}
