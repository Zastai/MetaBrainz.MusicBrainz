using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  #if NETFX_LT_4_5
  using AliasList        = IEnumerable<IAlias>;
  using RelationshipList = IEnumerable<IRelationship>;
  using StringList       = IEnumerable<string>;
  using TagList          = IEnumerable<ITag>;
  using UserTagList      = IEnumerable<IUserTag>;
  #else
  using AliasList        = IReadOnlyList<IAlias>;
  using RelationshipList = IReadOnlyList<IRelationship>;
  using StringList       = IReadOnlyList<string>;
  using TagList          = IReadOnlyList<ITag>;
  using UserTagList      = IReadOnlyList<IUserTag>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Area : IArea {

    public EntityType EntityType => EntityType.Area;

    [JsonProperty("id", Required = Required.Always)]
    public Guid MbId { get; private set; }

    public AliasList Aliases => this._aliases;

    [JsonProperty("aliases", Required = Required.DisallowNull)]
    private Alias[] _aliases = null;

    [JsonProperty("annotation", Required = Required.Default)]
    public string Annotation { get; private set; }

    [JsonProperty("disambiguation", Required = Required.Always)]
    public string Disambiguation { get; private set; }

    [JsonProperty("iso-3166-1-codes", Required = Required.DisallowNull)]
    public StringList Iso31661Codes { get; private set; }

    [JsonProperty("iso-3166-2-codes", Required = Required.DisallowNull)]
    public StringList Iso31662Codes { get; private set; }

    [JsonProperty("iso-3166-3-codes", Required = Required.DisallowNull)]
    public StringList Iso31663Codes { get; private set; }

    public ILifeSpan LifeSpan => this._lifeSpan;

    [JsonProperty("life-span", Required = Required.DisallowNull)]
    private LifeSpan _lifeSpan = null;

    [JsonProperty("name", Required = Required.Always)]
    public string Name { get; private set; }

    public RelationshipList Relationships => this._relationships;

    [JsonProperty("relations", Required = Required.DisallowNull)]
    private Relationship[] _relationships = null;

    public TagList Tags => this._tags;

    [JsonProperty("tags", Required = Required.DisallowNull)]
    private Tag[] _tags = null;

    [JsonProperty("type", Required = Required.Default)]
    public string Type { get; private set; }

    [JsonProperty("type-id", Required = Required.Default)]
    public Guid? TypeId { get; private set; }

    public UserTagList UserTags => this._userTags;

    [JsonProperty("user-tags", Required = Required.DisallowNull)]
    private UserTag[] _userTags = null;

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
