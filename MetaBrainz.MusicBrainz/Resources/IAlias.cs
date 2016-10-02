using System;

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IAlias : ITextResource, ITypedResource {

    DateTime? BeginDate { get; }

    DateTime? EndDate { get; }

    string Locale { get; }

    string Primary { get; }

    string SortName { get; }

  }

}
