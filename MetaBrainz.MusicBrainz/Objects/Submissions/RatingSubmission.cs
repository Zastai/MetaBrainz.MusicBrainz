using System;
using System.Collections.Generic;
using System.Xml;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions {

  /// <summary>A submission request for adding ratings to various entities.</summary>
  [PublicAPI]
  public sealed class RatingSubmission : Submission {

    #region Public API

    /// <summary>Adds the specified rating to the specified entity.</summary>
    /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
    /// <param name="entityType">The type of entity identified by <paramref name="mbid"/>; must be an entity that supports ratings.</param>
    /// <param name="mbid">The MBID of the entity to rate.</param>
    /// <returns>This submission request.</returns>
    public RatingSubmission Add(byte rating, EntityType entityType, Guid mbid) {
      if (rating > 100)
        throw new ArgumentOutOfRangeException(nameof(rating), rating, "A rating value must be between 0 and 100.");
      var map = this.GetMap(entityType);
      map[mbid] = rating;
      return this;
    }

    /// <summary>Adds the specified rating to the specified entities.</summary>
    /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
    /// <param name="entityType">The type of entity identified by <paramref name="mbids"/>; must be an entity that supports ratings.</param>
    /// <param name="mbids">The MBIDs of the entities to rate.</param>
    /// <returns>This submission request.</returns>
    public RatingSubmission Add(byte rating, EntityType entityType, params Guid[] mbids) {
      foreach (var mbid in mbids)
        this.Add(rating, entityType, mbid);
      return this;
    }

    /// <summary>Adds the specified rating to the specified entity.</summary>
    /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
    /// <param name="entity">The entity to rate.</param>
    /// <returns>This submission request.</returns>
    public RatingSubmission Add(byte rating, IRatableEntity entity) {
      return this.Add(rating, entity.EntityType, entity.MbId);
    }

    /// <summary>Adds the specified rating to the specified entity.</summary>
    /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
    /// <param name="entities">The entities to rate.</param>
    /// <returns>This submission request.</returns>
    public RatingSubmission Add(byte rating, params IRatableEntity[] entities) {
      foreach (var entity in entities) {
        if (entity == null)
          continue;
        this.Add(rating, entity.EntityType, entity.MbId);
      }
      return this;
    }

    #endregion

    #region Internals

    internal RatingSubmission(Query query, string client) : base(query, client, "rating", Method.POST) { }

    private class RatingMap : Dictionary<Guid, byte> { }

    private readonly RatingMap _artists       = new RatingMap();
    private readonly RatingMap _events        = new RatingMap();
    private readonly RatingMap _labels        = new RatingMap();
    private readonly RatingMap _recordings    = new RatingMap();
    private readonly RatingMap _releaseGroups = new RatingMap();
    private readonly RatingMap _works         = new RatingMap();

    private RatingMap GetMap(EntityType entityType) {
      return entityType switch {
        EntityType.Artist       => this._artists,
        EntityType.Event        => this._events,
        EntityType.Label        => this._labels,
        EntityType.Recording    => this._recordings,
        EntityType.ReleaseGroup => this._releaseGroups,
        EntityType.Work         => this._works,
        _ => throw new ArgumentOutOfRangeException(nameof(entityType), entityType, "Entities of this type cannot be rated.")
      };
    }

    internal override string RequestBody {
      get {
        using var sw = new U8StringWriter();
        using (var xml = XmlWriter.Create(sw)) {
          xml.WriteStartDocument();
          xml.WriteStartElement("", "metadata", "http://musicbrainz.org/ns/mmd-2.0#");
          Write(xml, this._artists,       "artist");
          Write(xml, this._events,        "event");
          Write(xml, this._labels,        "label");
          Write(xml, this._recordings,    "recording");
          Write(xml, this._releaseGroups, "release-group");
          Write(xml, this._works,         "work");
          xml.WriteEndElement();
        }
        return sw.ToString();
      }
    }

    private static void Write(XmlWriter xml, RatingMap items, string element) {
      if (items.Count == 0)
        return;
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

}
