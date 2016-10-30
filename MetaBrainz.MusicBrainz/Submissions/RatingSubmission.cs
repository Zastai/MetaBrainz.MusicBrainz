using System;
using System.Collections.Generic;
using System.Xml;

using MetaBrainz.MusicBrainz.Entities;

namespace MetaBrainz.MusicBrainz.Submissions {

  /// <summary>A submission request for adding ratings to various entities.</summary>
  public sealed class RatingSubmission : Submission {

    #region Public API

    /// <summary>Adds the specified rating to the specified entity.</summary>
    /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
    /// <param name="entityType">The type of entity identified by <paramref name="mbid"/>; must be an entity that supports ratings.</param>
    /// <param name="mbid">The MBID of the entity to rate.</param>
    /// <returns>This submission request.</returns>
    public RatingSubmission Add(byte rating, EntityType entityType, Guid mbid) {
      if (rating > 100) throw new ArgumentOutOfRangeException(nameof(rating), rating, "A rating value must be between 0 and 100.");
      switch (entityType) {
        case EntityType.Artist:       this._artRatings[mbid] = rating; break;
        case EntityType.Event:        this._evtRatings[mbid] = rating; break;
        case EntityType.Label:        this._lblRatings[mbid] = rating; break;
        case EntityType.Recording:    this._recRatings[mbid] = rating; break;
        case EntityType.ReleaseGroup: this._rlgRatings[mbid] = rating; break;
        case EntityType.Work:         this._wrkRatings[mbid] = rating; break;
        default:
          throw new ArgumentOutOfRangeException(nameof(entityType), entityType, "Entities of this type cannot be rated.");
      }
      return this;
    }

    /// <summary>Adds the specified rating to the specified entities.</summary>
    /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
    /// <param name="entityType">The type of entity identified by <paramref name="mbids"/>; must be an entity that supports ratings.</param>
    /// <param name="mbids">The MBIDs of the entities to rate.</param>
    /// <returns>This submission request.</returns>
    public RatingSubmission Add(byte rating, EntityType entityType, params Guid[] mbids) {
      if (rating > 100) throw new ArgumentOutOfRangeException(nameof(rating), rating, "A rating value must be between 0 and 100.");
      foreach (var mbid in mbids)
        this.Add(rating, entityType, mbid);
      return this;
    }

    /// <summary>Adds the specified rating to the specified entity.</summary>
    /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
    /// <param name="entity">The entity to rate.</param>
    /// <returns>This submission request.</returns>
    public RatingSubmission Add(byte rating, IRatedEntity entity) {
      if (rating > 100) throw new ArgumentOutOfRangeException(nameof(rating), rating, "A rating value must be between 0 and 100.");
      if (entity == null) throw new ArgumentNullException(nameof(entity));
      return this.Add(rating, entity.EntityType, entity.MbId);
    }

    /// <summary>Adds the specified rating to the specified entity.</summary>
    /// <param name="rating">The rating to add (1-100), or 0 to remove the rating.</param>
    /// <param name="entities">The entities to rate.</param>
    /// <returns>This submission request.</returns>
    public RatingSubmission Add(byte rating, params IRatedEntity[] entities) {
      if (rating > 100) throw new ArgumentOutOfRangeException(nameof(rating), rating, "A rating value must be between 0 and 100.");
      if (entities == null) throw new ArgumentNullException(nameof(entities));
      foreach (var item in entities) {
        if (item == null)
          continue;
        this.Add(rating, item.EntityType, item.MbId);
      }
      return this;
    }

    #endregion

    #region Internals

    internal RatingSubmission(Query query, string client) : base(query, client, "rating", "POST") { }

    private readonly Dictionary<Guid, byte> _artRatings = new Dictionary<Guid, byte>();
    private readonly Dictionary<Guid, byte> _evtRatings = new Dictionary<Guid, byte>();
    private readonly Dictionary<Guid, byte> _lblRatings = new Dictionary<Guid, byte>();
    private readonly Dictionary<Guid, byte> _recRatings = new Dictionary<Guid, byte>();
    private readonly Dictionary<Guid, byte> _rlgRatings = new Dictionary<Guid, byte>();
    private readonly Dictionary<Guid, byte> _wrkRatings = new Dictionary<Guid, byte>();

    internal override string RequestBody {
      get {
        using (var sw = new U8StringWriter()) {
          using (var xml = XmlWriter.Create(sw)) {
            xml.WriteStartDocument();
            xml.WriteStartElement("", "metadata", "http://musicbrainz.org/ns/mmd-2.0#");
            RatingSubmission.Write(xml, this._artRatings, "artist");
            RatingSubmission.Write(xml, this._evtRatings, "event");
            RatingSubmission.Write(xml, this._lblRatings, "label");
            RatingSubmission.Write(xml, this._recRatings, "recording");
            RatingSubmission.Write(xml, this._rlgRatings, "release-group");
            RatingSubmission.Write(xml, this._wrkRatings, "work");
            xml.WriteEndElement();
          }
          return sw.ToString();
        }
      }
    }

    private static void Write(XmlWriter xml, IDictionary<Guid, byte> items, string element) {
      if (items == null || items.Count == 0)
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
