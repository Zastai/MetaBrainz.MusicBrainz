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

    [JsonConverter(typeof(JsonInterfaceListConverter<IAlias, Alias>))]
    [JsonPropertyName("aliases")]
    public IReadOnlyList<IAlias>? Aliases { get; set; }

    [JsonPropertyName("annotation")]
    public string? Annotation { get; set; }

    [JsonPropertyName("disambiguation")]
    public string? Disambiguation { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<ITag, Tag>))]
    [JsonPropertyName("genres")]
    public IReadOnlyList<ITag>? Genres  { get; set; }

    [JsonPropertyName("iso-3166-1-codes")]
    public IReadOnlyList<string>? Iso31661Codes { get; set; }

    [JsonPropertyName("iso-3166-2-codes")]
    public IReadOnlyList<string>? Iso31662Codes { get; set; }

    [JsonPropertyName("iso-3166-3-codes")]
    public IReadOnlyList<string>? Iso31663Codes { get; set; }

    [JsonConverter(typeof(JsonInterfaceConverter<ILifeSpan, LifeSpan>))]
    [JsonPropertyName("life-span")]
    public ILifeSpan? LifeSpan { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IRelationship, Relationship>))]
    [JsonPropertyName("relations")]
    public IReadOnlyList<IRelationship>? Relationships { get; set; }

    // The name is serialized as 'sort-name' too, probably for historical reasons.
    [JsonPropertyName("sort-name")]
    public string? SortName { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<ITag, Tag>))]
    [JsonPropertyName("tags")]
    public IReadOnlyList<ITag>? Tags { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("type-id")]
    public Guid? TypeId { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IUserTag, UserTag>))]
    [JsonPropertyName("user-genres")]
    public IReadOnlyList<IUserTag>? UserGenres { get; set; }

    [JsonConverter(typeof(JsonInterfaceListConverter<IUserTag, UserTag>))]
    [JsonPropertyName("user-tags")]
    public IReadOnlyList<IUserTag>? UserTags  { get; set; }

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - relationships are presented as a "relation-list" structure with additional indirection
    // => special property (SearchRelationList) added to "unwrap" the list

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class RelationList {

      [JsonPropertyName("relations")]
      public Relationship[]? Items { get; set; }

      public static Relationship[]? Unwrap(RelationList[]? wrappers) {
        if (wrappers == null)
          return null;
        var count = wrappers.Sum(wrapper => wrapper.Items?.Length ?? 0);
        var relationships = new Relationship[count];
        var pos = 0;
        foreach (var wrapper in wrappers) {
          var items = wrapper.Items;
          if (items == null)
            continue;
          Array.Copy(items, 0, relationships, pos, items.Length);
          pos += items.Length;
        }
        return relationships;
      }

    }

    [JsonPropertyName("relation-list")]
    public RelationList[]? SearchRelationList {
      // Without this getter, this property does not get deserialized!
      get => null;
      set => this.Relationships = RelationList.Unwrap(value);
    }

    #endregion

    public override string ToString() {
      var text = string.Empty;
      if (this.SearchScore.HasValue)
        text += $"[Score: {this.SearchScore.Value}] ";
      if (this.Name != null)
        text += this.Name;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += $" ({this.Disambiguation})";
      if (this.Type != null)
        text += $" ({this.Type})";
      return text;
    }

  }

}
