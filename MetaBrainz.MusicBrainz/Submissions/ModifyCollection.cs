using System;
using System.Collections.Generic;
using System.Text;

using MetaBrainz.MusicBrainz.Entities;

namespace MetaBrainz.MusicBrainz.Submissions {

  internal sealed class ModifyCollection : ISubmission {

    public ModifyCollection(string method, string client, Guid collection, CollectionEntityType entityType) {
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

    public ModifyCollection Add<T>(IEnumerable<T> items) where T : IMbEntity {
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

    private static string MapType(CollectionEntityType entityType) {
      switch (entityType) {
        case CollectionEntityType.Area:         return "areas";
        case CollectionEntityType.Artist:       return "artists";
        case CollectionEntityType.Event:        return "events";
        case CollectionEntityType.Instrument:   return "instruments";
        case CollectionEntityType.Label:        return "labels";
        case CollectionEntityType.Place:        return "places";
        case CollectionEntityType.Recording:    return "recordings";
        case CollectionEntityType.Release:      return "releases";
        case CollectionEntityType.ReleaseGroup: return "release-groups";
        case CollectionEntityType.Series:       return "series";
        case CollectionEntityType.Work:         return "works";
        default:                                return "things";
      }
    }

  }

}
