using System;

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IWorkAttribute : ITypedResource {

    Guid? ValueId { get; }

    string Text { get; }

  }

}
