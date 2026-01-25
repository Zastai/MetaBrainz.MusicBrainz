using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions;

/// <summary>A submission request for adding ratings to various entities.</summary>
[PublicAPI]
public sealed class RatingSubmission : XmlSubmission {

  #region Public API

  /// <summary>Adds the specified rating to the specified entity.</summary>
  /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbid"/>; must be an entity that supports ratings.
  /// </param>
  /// <param name="mbid">The MBID of the entity to rate.</param>
  /// <returns>This submission request.</returns>
  public RatingSubmission Add(byte rating, EntityType entityType, Guid mbid) {
    this.AddRating(rating, entityType, mbid);
    return this;
  }

  /// <summary>Adds the specified rating to the specified entities.</summary>
  /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbids"/>; must be an entity that supports ratings.
  /// </param>
  /// <param name="mbids">The MBIDs of the entities to rate.</param>
  /// <returns>This submission request.</returns>
  public RatingSubmission Add(byte rating, EntityType entityType, params Guid[] mbids)
    => this.Add(rating, entityType, mbids.AsSpan());

  /// <summary>Adds the specified rating to the specified entities.</summary>
  /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbids"/>; must be an entity that supports ratings.
  /// </param>
  /// <param name="mbids">The MBIDs of the entities to rate.</param>
  /// <returns>This submission request.</returns>
  public RatingSubmission Add(byte rating, EntityType entityType, params IEnumerable<Guid> mbids) {
    foreach (var mbid in mbids) {
      this.AddRating(rating, entityType, mbid);
    }
    return this;
  }

  /// <summary>Adds the specified rating to the specified entities.</summary>
  /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbids"/>; must be an entity that supports ratings.
  /// </param>
  /// <param name="mbids">The MBIDs of the entities to rate.</param>
  /// <returns>This submission request.</returns>
  public RatingSubmission Add(byte rating, EntityType entityType, params ReadOnlySpan<Guid> mbids) {
    foreach (var mbid in mbids) {
      this.AddRating(rating, entityType, mbid);
    }
    return this;
  }

  /// <summary>Adds the specified rating to the specified entity.</summary>
  /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
  /// <param name="entity">The entity to rate.</param>
  /// <returns>This submission request.</returns>
  public RatingSubmission Add(byte rating, IRatableEntity entity) => this.Add(rating, entity.EntityType, entity.Id);

  /// <summary>Adds the specified rating to the specified entity.</summary>
  /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
  /// <param name="entities">The entities to rate.</param>
  /// <returns>This submission request.</returns>
  public RatingSubmission Add(byte rating, params IRatableEntity[] entities) => this.Add(rating, entities.AsSpan());

  /// <summary>Adds the specified rating to the specified entity.</summary>
  /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
  /// <param name="entities">The entities to rate.</param>
  /// <returns>This submission request.</returns>
  public RatingSubmission Add(byte rating, params ReadOnlySpan<IRatableEntity> entities) {
    foreach (var entity in entities) {
      this.AddRating(rating, entity.EntityType, entity.Id);
    }
    return this;
  }

  /// <summary>Adds the specified rating to the specified entities.</summary>
  /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbids"/>; must be an entity that supports ratings.
  /// </param>
  /// <param name="mbids">The MBIDs of the entities to rate.</param>
  /// <returns>This submission request.</returns>
  public async Task<RatingSubmission> AddAsync(byte rating, EntityType entityType, IAsyncEnumerable<Guid> mbids) {
    await foreach (var mbid in mbids) {
      this.AddRating(rating, entityType, mbid);
    }
    return this;
  }

  #endregion

  #region Internals

  internal RatingSubmission(Query query, string client) : base(query, client, "rating", HttpMethod.Post) { }

  private class RatingMap : Dictionary<Guid, byte>;

  private readonly RatingMap _artists = [ ];

  private readonly RatingMap _events = [ ];

  private readonly RatingMap _labels = [ ];

  private readonly RatingMap _recordings = [ ];

  private readonly RatingMap _releaseGroups = [ ];

  private readonly RatingMap _works = [ ];

  private void AddRating(byte rating, EntityType entityType, Guid mbid) {
    if (rating > 100) {
      throw new ArgumentOutOfRangeException(nameof(rating), rating, "A rating value must be between 0 and 100.");
    }
    var map = entityType switch {
      EntityType.Artist => this._artists,
      EntityType.Event => this._events,
      EntityType.Label => this._labels,
      EntityType.Recording => this._recordings,
      EntityType.ReleaseGroup => this._releaseGroups,
      EntityType.Work => this._works,
      _ => throw new ArgumentOutOfRangeException(nameof(entityType), entityType, "Entities of this type cannot be rated.")
    };
    map[mbid] = rating;
  }

  private protected override void WriteBodyContents(XmlWriter xml) {
    RatingSubmission.WriteBodyContents(xml, this._artists, "artist");
    RatingSubmission.WriteBodyContents(xml, this._events, "event");
    RatingSubmission.WriteBodyContents(xml, this._labels, "label");
    RatingSubmission.WriteBodyContents(xml, this._recordings, "recording");
    RatingSubmission.WriteBodyContents(xml, this._releaseGroups, "release-group");
    RatingSubmission.WriteBodyContents(xml, this._works, "work");
  }

  private static void WriteBodyContents(XmlWriter xml, Dictionary<Guid, byte> items, string element) {
    if (items.Count == 0) {
      return;
    }
    xml.WriteStartElement(element + "-list");
    foreach (var entry in items) {
      xml.WriteStartElement(element);
      xml.WriteAttributeString("id", entry.Key.ToString("D"));
      xml.WriteElementString("user-rating", entry.Value.ToString());
      xml.WriteEndElement();
    }
    xml.WriteEndElement();
  }

  #endregion

}
