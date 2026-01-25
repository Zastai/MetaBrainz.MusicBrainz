using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Submissions;

/// <summary>A submission request for modifying tags on various entities.</summary>
[PublicAPI]
public sealed class TagSubmission : XmlSubmission {

  #region Public API

  /// <summary>Votes for the specified tags on the specified entity.</summary>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbid"/>; must be an entity that supports tags.
  /// </param>
  /// <param name="mbid">The MBID of the entity to tag.</param>
  /// <param name="vote">The vote to apply to the tags.</param>
  /// <param name="tags">The tags to vote for.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(EntityType entityType, Guid mbid, TagVote vote, params IEnumerable<string> tags) {
    foreach (var tag in tags) {
      this.AddVote(tag, vote, entityType, mbid);
    }
    return this;
  }

  /// <summary>Votes for the specified tags on the specified entity.</summary>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbid"/>; must be an entity that supports tags.
  /// </param>
  /// <param name="mbid">The MBID of the entity to tag.</param>
  /// <param name="vote">The vote to apply to the tags.</param>
  /// <param name="tags">The tags to vote for.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(EntityType entityType, Guid mbid, TagVote vote, params ReadOnlySpan<string> tags) {
    foreach (var tag in tags) {
      this.AddVote(tag, vote, entityType, mbid);
    }
    return this;
  }

  /// <summary>Votes for the specified tags on the specified entity.</summary>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbid"/>; must be an entity that supports tags.
  /// </param>
  /// <param name="mbid">The MBID of the entity to tag.</param>
  /// <param name="vote">The vote to apply to the tags.</param>
  /// <param name="tags">The tags to vote for.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(EntityType entityType, Guid mbid, TagVote vote, params string[] tags)
    => this.Add(entityType, mbid, vote, tags.AsSpan());

  /// <summary>Votes for the specified tags on the specified entity.</summary>
  /// <param name="entity">The entity to tag.</param>
  /// <param name="vote">The vote to apply to the tags.</param>
  /// <param name="tags">The tags to vote for.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(ITaggableEntity entity, TagVote vote, params IEnumerable<string> tags)
    => this.Add(entity.EntityType, entity.Id, vote, tags);

  /// <summary>Votes for the specified tags on the specified entity.</summary>
  /// <param name="entity">The entity to tag.</param>
  /// <param name="vote">The vote to apply to the tags.</param>
  /// <param name="tags">The tags to vote for.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(ITaggableEntity entity, TagVote vote, params ReadOnlySpan<string> tags)
    => this.Add(entity.EntityType, entity.Id, vote, tags);

  /// <summary>Votes for the specified tags on the specified entity.</summary>
  /// <param name="entity">The entity to tag.</param>
  /// <param name="vote">The vote to apply to the tags.</param>
  /// <param name="tags">The tags to vote for.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(ITaggableEntity entity, TagVote vote, params string[] tags)
    => this.Add(entity.EntityType, entity.Id, vote, tags);

  /// <summary>Votes for the specified tag on the specified entity.</summary>
  /// <param name="tag">The tag to vote for.</param>
  /// <param name="vote">The vote to apply to the tag.</param>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbid"/>; must be an entity that supports tags.
  /// </param>
  /// <param name="mbid">The MBID of the entity to tag.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(string tag, TagVote vote, EntityType entityType, Guid mbid) {
    this.AddVote(tag, vote, entityType, mbid);
    return this;
  }

  /// <summary>Votes for the specified tag on the specified entities.</summary>
  /// <param name="tag">The tag to vote for.</param>
  /// <param name="vote">The vote to apply to the tag.</param>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbids"/>; must be an entity that supports tags.
  /// </param>
  /// <param name="mbids">The MBIDs of the entities to tag.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(string tag, TagVote vote, EntityType entityType, params Guid[] mbids)
    => this.Add(tag, vote, entityType, mbids.AsSpan());

  /// <summary>Votes for the specified tag on the specified entities.</summary>
  /// <param name="tag">The tag to vote for.</param>
  /// <param name="vote">The vote to apply to the tag.</param>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbids"/>; must be an entity that supports tags.
  /// </param>
  /// <param name="mbids">The MBIDs of the entities to tag.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(string tag, TagVote vote, EntityType entityType, params IEnumerable<Guid> mbids) {
    foreach (var mbid in mbids) {
      this.AddVote(tag, vote, entityType, mbid);
    }
    return this;
  }

  /// <summary>Votes for the specified tag on the specified entities.</summary>
  /// <param name="tag">The tag to vote for.</param>
  /// <param name="vote">The vote to apply to the tag.</param>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbids"/>; must be an entity that supports tags.
  /// </param>
  /// <param name="mbids">The MBIDs of the entities to tag.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(string tag, TagVote vote, EntityType entityType, params ReadOnlySpan<Guid> mbids) {
    foreach (var mbid in mbids) {
      this.AddVote(tag, vote, entityType, mbid);
    }
    return this;
  }

  /// <summary>Votes for the specified tag on the specified entities.</summary>
  /// <param name="tag">The tag to vote for.</param>
  /// <param name="vote">The vote to apply to the tag.</param>
  /// <param name="entities">The entities to tag.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(string tag, TagVote vote, params IEnumerable<ITaggableEntity> entities) {
    foreach (var item in entities) {
      this.AddVote(tag, vote, item.EntityType, item.Id);
    }
    return this;
  }

  /// <summary>Votes for the specified tag on the specified entity.</summary>
  /// <param name="tag">The tag to vote for.</param>
  /// <param name="vote">The vote to apply to the tag.</param>
  /// <param name="entity">The entity to tag.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(string tag, TagVote vote, ITaggableEntity entity) => this.Add(tag, vote, entity.EntityType, entity.Id);

  /// <summary>Votes for the specified tag on the specified entities.</summary>
  /// <param name="tag">The tag to vote for.</param>
  /// <param name="vote">The vote to apply to the tag.</param>
  /// <param name="entities">The entities to tag.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(string tag, TagVote vote, params ITaggableEntity[] entities) => this.Add(tag, vote, entities.AsSpan());

  /// <summary>Votes for the specified tag on the specified entities.</summary>
  /// <param name="tag">The tag to vote for.</param>
  /// <param name="vote">The vote to apply to the tag.</param>
  /// <param name="entities">The entities to tag.</param>
  /// <returns>This submission request.</returns>
  public TagSubmission Add(string tag, TagVote vote, params ReadOnlySpan<ITaggableEntity> entities) {
    foreach (var item in entities) {
      this.AddVote(tag, vote, item.EntityType, item.Id);
    }
    return this;
  }

  /// <summary>Votes for the specified tags on the specified entity.</summary>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbid"/>; must be an entity that supports tags.
  /// </param>
  /// <param name="mbid">The MBID of the entity to tag.</param>
  /// <param name="vote">The vote to apply to the tags.</param>
  /// <param name="tags">The tags to vote for.</param>
  /// <returns>This submission request.</returns>
  public async Task<TagSubmission> AddAsync(EntityType entityType, Guid mbid, TagVote vote, IAsyncEnumerable<string> tags) {
    await foreach (var tag in tags) {
      this.AddVote(tag, vote, entityType, mbid);
    }
    return this;
  }

  /// <summary>Votes for the specified tags on the specified entity.</summary>
  /// <param name="entity">The entity to tag.</param>
  /// <param name="vote">The vote to apply to the tags.</param>
  /// <param name="tags">The tags to vote for.</param>
  /// <returns>This submission request.</returns>
  public Task<TagSubmission> AddAsync(ITaggableEntity entity, TagVote vote, IAsyncEnumerable<string> tags)
    => this.AddAsync(entity.EntityType, entity.Id, vote, tags);

  /// <summary>Votes for the specified tag on the specified entities.</summary>
  /// <param name="tag">The tag to vote for.</param>
  /// <param name="vote">The vote to apply to the tag.</param>
  /// <param name="entityType">
  /// The type of entity identified by <paramref name="mbids"/>; must be an entity that supports tags.
  /// </param>
  /// <param name="mbids">The MBIDs of the entities to tag.</param>
  /// <returns>This submission request.</returns>
  public async Task<TagSubmission> AddAsync(string tag, TagVote vote, EntityType entityType, IAsyncEnumerable<Guid> mbids) {
    await foreach (var mbid in mbids) {
      this.AddVote(tag, vote, entityType, mbid);
    }
    return this;
  }

  /// <summary>Votes for the specified tag on the specified entities.</summary>
  /// <param name="tag">The tag to vote for.</param>
  /// <param name="vote">The vote to apply to the tag.</param>
  /// <param name="entities">The entities to tag.</param>
  /// <returns>This submission request.</returns>
  public async Task<TagSubmission> AddAsync(string tag, TagVote vote, IAsyncEnumerable<ITaggableEntity> entities) {
    await foreach (var item in entities) {
      this.AddVote(tag, vote, item.EntityType, item.Id);
    }
    return this;
  }

  #endregion

  #region Internals

  internal TagSubmission(Query query, string client) : base(query, client, "tag", HttpMethod.Post) { }

  private class VoteMap : Dictionary<string, TagVote>;

  private class TagMap : Dictionary<Guid, VoteMap>;

  private readonly TagMap _areas = new();

  private readonly TagMap _artists = new();

  private readonly TagMap _events = new();

  private readonly TagMap _instruments = new();

  private readonly TagMap _labels = new();

  private readonly TagMap _places = new();

  private readonly TagMap _recordings = new();

  private readonly TagMap _releases = new();

  private readonly TagMap _releaseGroups = new();

  private readonly TagMap _series = new();

  private readonly TagMap _works = new();

  private void AddVote(string tag, TagVote vote, EntityType entityType, Guid mbid) {
    var tags = entityType switch {
      EntityType.Area => this._areas,
      EntityType.Artist => this._artists,
      EntityType.Event => this._events,
      EntityType.Instrument => this._instruments,
      EntityType.Label => this._labels,
      EntityType.Place => this._places,
      EntityType.Recording => this._recordings,
      EntityType.Release => this._releases,
      EntityType.ReleaseGroup => this._releaseGroups,
      EntityType.Series => this._series,
      EntityType.Work => this._works,
      _ => throw new ArgumentOutOfRangeException(nameof(entityType), entityType, "Entities of this type cannot be tagged.")
    };
    VoteMap? votes;
    while (!tags.TryGetValue(mbid, out votes)) {
      if (tags.TryAdd(mbid, votes = [])) {
        break;
      }
    }
    votes[tag] = vote;
  }

  private protected override void WriteBodyContents(XmlWriter xml) {
    TagSubmission.WriteBodyContents(xml, this._areas, "area");
    TagSubmission.WriteBodyContents(xml, this._artists, "artist");
    TagSubmission.WriteBodyContents(xml, this._events, "event");
    TagSubmission.WriteBodyContents(xml, this._instruments, "instrument");
    TagSubmission.WriteBodyContents(xml, this._labels, "label");
    TagSubmission.WriteBodyContents(xml, this._places, "place");
    TagSubmission.WriteBodyContents(xml, this._recordings, "recording");
    TagSubmission.WriteBodyContents(xml, this._releases, "release");
    TagSubmission.WriteBodyContents(xml, this._releaseGroups, "release-group");
    TagSubmission.WriteBodyContents(xml, this._series, "series");
    TagSubmission.WriteBodyContents(xml, this._works, "work");
  }

  private static void WriteBodyContents(XmlWriter xml, TagMap items, string element) {
    if (items.Count == 0) {
      return;
    }
    xml.WriteStartElement(element + "-list");
    foreach (var entry in items) {
      xml.WriteStartElement(element);
      xml.WriteAttributeString("id", entry.Key.ToString("D"));
      xml.WriteStartElement("user-tag-list");
      foreach (var tag in entry.Value) {
        xml.WriteStartElement("user-tag");
        xml.WriteAttributeString("vote", tag.Value switch {
          TagVote.Down => "downvote",
          TagVote.Up => "upvote",
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
