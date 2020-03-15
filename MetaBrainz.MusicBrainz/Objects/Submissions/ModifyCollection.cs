using System;
using System.Collections.Generic;
using System.Text;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Submissions;

namespace MetaBrainz.MusicBrainz.Objects.Submissions {

  internal sealed class ModifyCollection : ISubmission {

    public ModifyCollection(Method method, string client, Guid collection, EntityType entityType) {
      this._method = method;
      this._client = client;
      if (string.IsNullOrWhiteSpace(client))
        throw new ArgumentException("The client ID must not be blank.", nameof(client));
      this._request = new StringBuilder(16 * 1024);
      this._request.Append("collection/").Append(collection.ToString("D")).Append('/').Append(MapType(entityType)).Append('/');
    }

    public ModifyCollection Add(IEnumerable<Guid> items) {
      foreach (var item in items)
        this._request.Append(item.ToString("D")).Append(';');
      return this;
    }

    public ModifyCollection Add<T>(IEnumerable<T> items) where T : IEntity {
      foreach (var item in items)
        this._request.Append(item.MbId.ToString("D")).Append(';');
      return this;
    }

    private readonly Method        _method;
    private readonly string        _client;
    private readonly StringBuilder _request;

    string ISubmission.Client => this._client;
    string ISubmission.Entity => this._request.ToString();
    Method ISubmission.Method => this._method;

    string? ISubmission.ContentType { get; } = null;
    string? ISubmission.RequestBody { get; } = null;

    private static string MapType(EntityType entityType) {
      return entityType switch {
        EntityType.Area         => "areas",
        EntityType.Artist       => "artists",
        EntityType.Event        => "events",
        EntityType.Instrument   => "instruments",
        EntityType.Label        => "labels",
        EntityType.Place        => "places",
        EntityType.Recording    => "recordings",
        EntityType.Release      => "releases",
        EntityType.ReleaseGroup => "release-groups",
        EntityType.Series       => "series",
        EntityType.Work         => "works",
        _ => throw new ArgumentOutOfRangeException(nameof(entityType), entityType,"The specified entity type cannot be stored in a collection.")
      };
    }

  }

}
