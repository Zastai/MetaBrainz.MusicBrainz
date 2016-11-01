using System;
using System.Collections.Generic;
using System.Xml;

using MetaBrainz.MusicBrainz.Entities;

namespace MetaBrainz.MusicBrainz.Submissions {

  /// <summary>A submission request for modifying tags on various entities.</summary>
  public sealed class TagSubmission : Submission {

    #region Public API

    /// <summary>Votes for the specified tags on the specified entity.</summary>
    /// <param name="entityType">The type of entity identified by <paramref name="mbid"/>; must be an entity that supports tags.</param>
    /// <param name="mbid">The MBID of the entity to tag.</param>
    /// <param name="vote">The vote to apply to the tags.</param>
    /// <param name="tags">The tags to vote for.</param>
    /// <returns>This submission request.</returns>
    public TagSubmission Add(EntityType entityType, Guid mbid, TagVote vote, params string[] tags) {
      if (tags == null) throw new ArgumentNullException(nameof(tags));
      foreach (var tag in tags)
        this.Add(tag, vote, entityType, mbid);
      return this;
    }

    /// <summary>Votes for the specified tags on the specified entity.</summary>
    /// <param name="entity">The entity to tag.</param>
    /// <param name="vote">The vote to apply to the tags.</param>
    /// <param name="tags">The tags to vote for.</param>
    /// <returns>This submission request.</returns>
    public TagSubmission Add(ITaggableEntity entity, TagVote vote, params string[] tags) {
      if (entity == null) throw new ArgumentNullException(nameof(entity));
      if (tags   == null) throw new ArgumentNullException(nameof(tags));
      foreach (var tag in tags)
        this.Add(tag, vote, entity.EntityType, entity.MbId);
      return this;
    }

    /// <summary>Votes for the specified tag on the specified entity.</summary>
    /// <param name="tag">The tag to vote for.</param>
    /// <param name="vote">The vote to apply to the tag.</param>
    /// <param name="entityType">The type of entity identified by <paramref name="mbid"/>; must be an entity that supports tags.</param>
    /// <param name="mbid">The MBID of the entity to tag.</param>
    /// <returns>This submission request.</returns>
    public TagSubmission Add(string tag, TagVote vote, EntityType entityType, Guid mbid) {
      if (tag == null) throw new ArgumentNullException(nameof(tag));
      var map = this.GetMap(entityType);
      VoteMap tagvote;
      if (!map.TryGetValue(mbid, out tagvote))
        map.Add(mbid, tagvote = new VoteMap());
      tagvote[tag] = vote;
      return this;
    }

    /// <summary>Votes for the specified tag on the specified entities.</summary>
    /// <param name="tag">The tag to vote for.</param>
    /// <param name="vote">The vote to apply to the tag.</param>
    /// <param name="entityType">The type of entity identified by <paramref name="mbids"/>; must be an entity that supports tags.</param>
    /// <param name="mbids">The MBID of the entity to tag.</param>
    /// <returns>This submission request.</returns>
    public TagSubmission Add(string tag, TagVote vote, EntityType entityType, params Guid[] mbids) {
      if (tag   == null) throw new ArgumentNullException(nameof(tag));
      if (mbids == null) throw new ArgumentNullException(nameof(mbids));
      foreach (var mbid in mbids)
        this.Add(tag, vote, entityType, mbid);
      return this;
    }

    /// <summary>Votes for the specified tag on the specified entity.</summary>
    /// <param name="tag">The tag to vote for.</param>
    /// <param name="vote">The vote to apply to the tag.</param>
    /// <param name="entity">The entity to tag.</param>
    /// <returns>This submission request.</returns>
    public TagSubmission Add(string tag, TagVote vote, ITaggableEntity entity) {
      if (tag    == null) throw new ArgumentNullException(nameof(tag));
      if (entity == null) throw new ArgumentNullException(nameof(entity));
      return this.Add(tag, vote, entity.EntityType, entity.MbId);
    }

    /// <summary>Votes for the specified tag on the specified entities.</summary>
    /// <param name="tag">The tag to vote for.</param>
    /// <param name="vote">The vote to apply to the tag.</param>
    /// <param name="entities">The entities to tag.</param>
    /// <returns>This submission request.</returns>
    public TagSubmission Add(string tag, TagVote vote, params ITaggableEntity[] entities) {
      if (tag      == null) throw new ArgumentNullException(nameof(tag));
      if (entities == null) throw new ArgumentNullException(nameof(entities));
      foreach (var item in entities) {
        if (item == null)
          continue;
        this.Add(tag, vote, item.EntityType, item.MbId);
      }
      return this;
    }

    #endregion

    #region Internals

    internal TagSubmission(Query query, string client) : base(query, client, "tag", "POST") { }

    private class VoteMap : Dictionary<string, TagVote> { }

    private class TagMap : Dictionary<Guid, VoteMap> { }

    private readonly TagMap _areas         = new TagMap();
    private readonly TagMap _artists       = new TagMap();
    private readonly TagMap _events        = new TagMap();
    private readonly TagMap _instruments   = new TagMap();
    private readonly TagMap _labels        = new TagMap();
    private readonly TagMap _places        = new TagMap();
    private readonly TagMap _recordings    = new TagMap();
    private readonly TagMap _releases      = new TagMap();
    private readonly TagMap _releaseGroups = new TagMap();
    private readonly TagMap _series        = new TagMap();
    private readonly TagMap _works         = new TagMap();

    private TagMap GetMap(EntityType entityType) {
      switch (entityType) {
        case EntityType.Area:         return this._areas;
        case EntityType.Artist:       return this._artists;
        case EntityType.Event:        return this._events;
        case EntityType.Instrument:   return this._instruments;
        case EntityType.Label:        return this._labels;
        case EntityType.Place:        return this._places;
        case EntityType.Recording:    return this._recordings;
        case EntityType.Release:      return this._releases;
        case EntityType.ReleaseGroup: return this._releaseGroups;
        case EntityType.Series:       return this._series;
        case EntityType.Work:         return this._works;
        default:
          throw new ArgumentOutOfRangeException(nameof(entityType), entityType, "Entities of this type cannot be tagged.");
      }
    }

    internal override string RequestBody {
      get {
        using (var sw = new U8StringWriter()) {
          using (var xml = XmlWriter.Create(sw)) {
            xml.WriteStartDocument();
            xml.WriteStartElement("", "metadata", "http://musicbrainz.org/ns/mmd-2.0#");
            TagSubmission.Write(xml, this._areas,         "area");
            TagSubmission.Write(xml, this._artists,       "artist");
            TagSubmission.Write(xml, this._events,        "event");
            TagSubmission.Write(xml, this._instruments,   "instrument");
            TagSubmission.Write(xml, this._labels,        "label");
            TagSubmission.Write(xml, this._places,        "place");
            TagSubmission.Write(xml, this._recordings,    "recording");
            TagSubmission.Write(xml, this._releases,      "release");
            TagSubmission.Write(xml, this._releaseGroups, "release-group");
            TagSubmission.Write(xml, this._series,        "series");
            TagSubmission.Write(xml, this._works,         "work");
            xml.WriteEndElement();
          }
          return sw.ToString();
        }
      }
    }

    private static void Write(XmlWriter xml, TagMap items, string element) {
      if (items == null || items.Count == 0)
        return;
      xml.WriteStartElement(element + "-list");
      foreach (var entry in items) {
        xml.WriteStartElement(element);
        xml.WriteAttributeString("id", entry.Key.ToString("D"));
        xml.WriteStartElement("user-tag-list");
        foreach (var tag in entry.Value) {
          xml.WriteStartElement("user-tag");
          switch (tag.Value) {
            case TagVote.Up:       xml.WriteAttributeString("vote", "upvote");   break;
            case TagVote.Down:     xml.WriteAttributeString("vote", "downvote"); break;
            case TagVote.Withdraw: xml.WriteAttributeString("vote", "withdraw"); break;
          }
          xml.WriteElementString("name", tag.Key);
          xml.WriteEndElement();
        }
        xml.WriteEndElement();
        xml.WriteEndElement();
      }
      xml.WriteEndElement();
    }

    #endregion

  }

}
