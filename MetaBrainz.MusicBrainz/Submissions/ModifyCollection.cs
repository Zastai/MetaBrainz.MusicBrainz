using System;
using System.Collections.Generic;
using System.Text;

using MetaBrainz.MusicBrainz.Entities;

namespace MetaBrainz.MusicBrainz.Submissions {

  internal sealed class ModifyCollection : ISubmission {

    public ModifyCollection(string method, string client, Guid collection, EntityType entityType) {
      if (method == null) throw new ArgumentNullException(nameof(method));
      if (client == null) throw new ArgumentNullException(nameof(client));
      if (string.IsNullOrWhiteSpace(client)) throw new ArgumentException("The client ID must not be blank.", nameof(client));
      this._method = method;
      this._client = client;
      this._request = new StringBuilder(16 * 1024);
      this._request.Append("collection/").Append(collection.ToString("D")).Append('/').Append(ModifyCollection.MapType(entityType)).Append('/');
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

    private readonly string        _method;
    private readonly string        _client;
    private readonly StringBuilder _request;

    string ISubmission.Client => this._client;

    string ISubmission.Entity => this._request.ToString();

    string ISubmission.Method => this._method;

    string ISubmission.RequestBody => null;

    private static string MapType(EntityType entityType) {
      switch (entityType) {
        case EntityType.Area:         return "areas";
        case EntityType.Artist:       return "artists";
        case EntityType.Event:        return "events";
        case EntityType.Instrument:   return "instruments";
        case EntityType.Label:        return "labels";
        case EntityType.Place:        return "places";
        case EntityType.Recording:    return "recordings";
        case EntityType.Release:      return "releases";
        case EntityType.ReleaseGroup: return "release-groups";
        case EntityType.Series:       return "series";
        case EntityType.Work:         return "works";
        default:
          throw new ArgumentOutOfRangeException(nameof(entityType), entityType, "The specified entity type cannot be stored in a collection.");
      }
    }

  }

}
