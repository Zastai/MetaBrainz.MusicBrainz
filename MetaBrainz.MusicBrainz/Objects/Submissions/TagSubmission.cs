using System;
using System.Collections.Generic;
using System.Xml;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions {

  /// <summary>A submission request for modifying tags on various entities.</summary>
  [PublicAPI]
  public sealed class TagSubmission : Submission {

    #region Public API

    /// <summary>Votes for the specified tags on the specified entity.</summary>
    /// <param name="entityType">The type of entity identified by <paramref name="mbid"/>; must be an entity that supports tags.</param>
    /// <param name="mbid">The MBID of the entity to tag.</param>
    /// <param name="vote">The vote to apply to the tags.</param>
    /// <param name="tags">The tags to vote for.</param>
    /// <returns>This submission request.</returns>
    public TagSubmission Add(EntityType entityType, Guid mbid, TagVote vote, params string[] tags) {
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
      var map = this.GetMap(entityType);
      if (!map.TryGetValue(mbid, out var tagVote))
        map.Add(mbid, tagVote = new VoteMap());
      tagVote[tag] = vote;
      return this;
    }

    /// <summary>Votes for the specified tag on the specified entities.</summary>
    /// <param name="tag">The tag to vote for.</param>
    /// <param name="vote">The vote to apply to the tag.</param>
    /// <param name="entityType">The type of entity identified by <paramref name="mbids"/>; must be an entity that supports tags.</param>
    /// <param name="mbids">The MBIDs of the entities to tag.</param>
    /// <returns>This submission request.</returns>
    public TagSubmission Add(string tag, TagVote vote, EntityType entityType, params Guid[] mbids) {
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
      return this.Add(tag, vote, entity.EntityType, entity.MbId);
    }

    /// <summary>Votes for the specified tag on the specified entities.</summary>
    /// <param name="tag">The tag to vote for.</param>
    /// <param name="vote">The vote to apply to the tag.</param>
    /// <param name="entities">The entities to tag.</param>
    /// <returns>This submission request.</returns>
    public TagSubmission Add(string tag, TagVote vote, params ITaggableEntity[] entities) {
      foreach (var item in entities) {
        if (item == null)
          continue;
        this.Add(tag, vote, item.EntityType, item.MbId);
      }
      return this;
    }

    #endregion

    #region Internals

    internal TagSubmission(Query query, string client) : base(query, client, "tag", Method.POST) { }

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
      return entityType switch {
        EntityType.Area         => this._areas,
        EntityType.Artist       => this._artists,
        EntityType.Event        => this._events,
        EntityType.Instrument   => this._instruments,
        EntityType.Label        => this._labels,
        EntityType.Place        => this._places,
        EntityType.Recording    => this._recordings,
        EntityType.Release      => this._releases,
        EntityType.ReleaseGroup => this._releaseGroups,
        EntityType.Series       => this._series,
        EntityType.Work         => this._works,
        _ => throw new ArgumentOutOfRangeException(nameof(entityType), entityType, "Entities of this type cannot be tagged.")
      };
    }

    internal override string RequestBody {
      get {
        using var sw = new U8StringWriter();
        using (var xml = XmlWriter.Create(sw)) {
          xml.WriteStartDocument();
          xml.WriteStartElement("", "metadata", "http://musicbrainz.org/ns/mmd-2.0#");
          Write(xml, this._areas,         "area");
          Write(xml, this._artists,       "artist");
          Write(xml, this._events,        "event");
          Write(xml, this._instruments,   "instrument");
          Write(xml, this._labels,        "label");
          Write(xml, this._places,        "place");
          Write(xml, this._recordings,    "recording");
          Write(xml, this._releases,      "release");
          Write(xml, this._releaseGroups, "release-group");
          Write(xml, this._series,        "series");
          Write(xml, this._works,         "work");
          xml.WriteEndElement();
        }
        return sw.ToString();
      }
    }

    private static void Write(XmlWriter xml, TagMap items, string element) {
      if (items.Count == 0)
        return;
      xml.WriteStartElement(element + "-list");
      foreach (var entry in items) {
        xml.WriteStartElement(element);
        xml.WriteAttributeString("id", entry.Key.ToString("D"));
        xml.WriteStartElement("user-tag-list");
        foreach (var tag in entry.Value) {
          xml.WriteStartElement("user-tag");
          xml.WriteAttributeString("vote", tag.Value switch {
            TagVote.Down     => "downvote",
            TagVote.Up       => "upvote",
            TagVote.Withdraw => "withdraw",
            _ => throw new InvalidOperationException($"Encountered an invalid tag vote ({tag.Value}).")
          });
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
