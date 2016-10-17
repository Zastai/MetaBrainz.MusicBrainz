using System;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IWorkAttribute : ITypedEntity {

    string Value { get; }

    Guid? ValueId { get; }

  }

}
