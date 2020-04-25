using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class UserTag : JsonBasedObject, IUserTag {

    public UserTag(string name) {
      this.Name = name;
    }

    public string Name { get; }

    public override string ToString() => this.Name;

  }

}
