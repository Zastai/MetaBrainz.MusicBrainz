using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class Genre : JsonBasedObject, IGenre {

    public Genre(Guid id, string name) {
      this.Id = id;
      this.Name = name;
    }

    public Guid Id { get; }

    public string? Disambiguation { get; set; }

    public string Name { get; }

    public int? VoteCount { get; set; }

    public override string ToString() {
      var text = this.Name;
      if (this.VoteCount.HasValue)
        text += $" (votes: {this.VoteCount})";
      return text;
    }

  }

}
