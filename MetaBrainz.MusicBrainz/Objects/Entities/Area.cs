using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Area : Entity, IFoundArea {

    public override EntityType EntityType => EntityType.Area;

    public IReadOnlyList<IAlias> Aliases => this.TheAliases;

    [JsonPropertyName("aliases")]
    public Alias[] TheAliases { get; set; }

    [JsonPropertyName("annotation")]
    public string Annotation { get; set; }

    [JsonPropertyName("disambiguation")]
    public string Disambiguation { get; set; }

    public IReadOnlyList<ITag> Genres => this.TheGenres;

    [JsonPropertyName("genres")]
    public Tag[] TheGenres { get; set; }

    [JsonPropertyName("iso-3166-1-codes")]
    public IReadOnlyList<string> Iso31661Codes { get; set; }

    [JsonPropertyName("iso-3166-2-codes")]
    public IReadOnlyList<string> Iso31662Codes { get; set; }

    [JsonPropertyName("iso-3166-3-codes")]
    public IReadOnlyList<string> Iso31663Codes { get; set; }

    public ILifeSpan LifeSpan => this.TheLifeSpan;

    [JsonPropertyName("life-span")]
    public LifeSpan TheLifeSpan { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public IReadOnlyList<IRelationship> Relationships => this.TheRelationships;

    [JsonPropertyName("relations")]
    public Relationship[] TheRelationships { get; set; }

    // The name is serialized as 'sort-name' too, probably for historical reasons.
    [JsonPropertyName("sort-name")]
    public string SortName { get; set; }

    public IReadOnlyList<ITag> Tags => this.TheTags;

    [JsonPropertyName("tags")]
    public Tag[] TheTags { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("type-id")]
    public Guid? TypeId { get; set; }

    public IReadOnlyList<IUserTag> UserGenres => this.TheUserGenres;

    [JsonPropertyName("user-genres")]
    public UserTag[] TheUserGenres { get; set; }

    public IReadOnlyList<IUserTag> UserTags => this.TheUserTags;

    [JsonPropertyName("user-tags")]
    public UserTag[] TheUserTags { get; set; }

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - relationships are presented as a "relation-list" structure with additional indirection
    // => special property (SearchRelationList) added to "unwrap" the list

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class RelationList {

      [JsonPropertyName("relations")]
      public Relationship[] Items { get; set; }

      public static Relationship[] Unwrap(RelationList[] wrappers) {
        if (wrappers == null)
          return null;
        var count = wrappers.Sum(wrapper => wrapper.Items.Length);
        var relationships = new Relationship[count];
        var pos = 0;
        foreach (var wrapper in wrappers) {
          var items = wrapper.Items;
          Array.Copy(items, 0, relationships, pos, items.Length);
          pos += items.Length;
        }
        return relationships;
      }

    }

    [JsonPropertyName("relation-list")]
    public RelationList[] SearchRelationList {
      // Without this getter, this property does not get deserialized!
      get => null;
      set => this.TheRelationships = RelationList.Unwrap(value);
    }

    #endregion

    public override string ToString() {
      var text = this.Name ?? string.Empty;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += " (" + this.Disambiguation + ")";
      if (this.Type != null)
        text += " (" + this.Type + ")";
      return text;
    }

  }

}
