using System;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IAlias : ITypedEntity {

    string BeginDate { get; }

    string EndDate { get; }

    bool? Ended { get; }

    string Locale { get; }

    string Name { get; }

    bool? Primary { get; }

    string SortName { get; }

  }

}
