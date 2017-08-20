using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using MetaBrainz.MusicBrainz.Objects.Searches;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Area : SearchResult, IFoundArea {

    public EntityType EntityType => EntityType.Area;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public IReadOnlyList<IAlias> Aliases => this._aliases;

    [JsonProperty("aliases", Required = Required.DisallowNull)]
    private Alias[] _aliases = null;

    [JsonProperty("annotation", Required = Required.Default)]
    public string Annotation { get; private set; }

    [JsonProperty("disambiguation", Required = Required.DisallowNull)]
    public string Disambiguation { get; private set; }

    [JsonProperty("iso-3166-1-codes", Required = Required.DisallowNull)]
    public IReadOnlyList<string> Iso31661Codes { get; private set; }

    [JsonProperty("iso-3166-2-codes", Required = Required.DisallowNull)]
    public IReadOnlyList<string> Iso31662Codes { get; private set; }

    [JsonProperty("iso-3166-3-codes", Required = Required.DisallowNull)]
    public IReadOnlyList<string> Iso31663Codes { get; private set; }

    public ILifeSpan LifeSpan => this._lifeSpan;

    [JsonProperty("life-span", Required = Required.DisallowNull)]
    private LifeSpan _lifeSpan = null;

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    public IReadOnlyList<IRelationship> Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    public IReadOnlyList<ITag> Tags => this._tags;

    [JsonProperty("tags", Required = Required.DisallowNull)]
    private Tag[] _tags = null;

    [JsonProperty("type", Required = Required.Default)]
    public string Type { get; private set; }

    [JsonProperty("type-id", Required = Required.Default)]
    public Guid? TypeId { get; private set; }

    public IReadOnlyList<IUserTag> UserTags => this._userTags;

    [JsonProperty("user-tags", Required = Required.DisallowNull)]
    private UserTag[] _userTags = null;

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - the disambiguation comment is not serialized when not set (instead of being serialized as an empty string)
    // => Adjusted the Required flags for affected properties (to allow their omission).
    // - relationships are presented as a "relation-list" structure with additional indirection
    // => special setter-only property (SearchRelationList) added to "unwrap" the list

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    [JsonObject(MemberSerialization.OptIn)]
    private sealed class RelationList {

      [JsonProperty("relations")] public Relationship[] Items = null;

      public static Relationship[] Unwrap(RelationList[] wrappers) {
        if (wrappers == null)
          return null;
        var relcount = 0;
        foreach (var wrapper in wrappers)
          relcount += wrapper.Items.Length;
        var rels = new Relationship[relcount];
        var pos = 0;
        foreach (var wrapper in wrappers) {
          var items = wrapper.Items;
          Array.Copy(items, 0, rels, pos, items.Length);
          pos += items.Length;
        }
        return rels;
      }

    }

    [JsonProperty("relation-list")]
    private RelationList[] SearchRelationList {
      set => this._relationships = RelationList.Unwrap(value);
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

    // The name is serialized as 'sort-name' too, probably for historical reasons. Ignore it.
    #pragma warning disable 169
    [JsonProperty("sort-name")] private string _sortName;
    #pragma warning restore 169

  }

}
