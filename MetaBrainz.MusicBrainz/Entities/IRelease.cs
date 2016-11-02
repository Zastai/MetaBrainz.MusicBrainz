using System;
using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IRelease : IMbEntity, IAnnotatedEntity, IRelatableEntity, ITaggableEntity, ITitledEntity {

    IEnumerable<INameCredit> ArtistCredit { get; }

    string Asin { get; }

    string BarCode { get; }

    IEnumerable<ICollection> Collections { get; }

    string Country { get; }

    ICoverArtArchive CoverArtArchive { get; }

    /// <summary>The earliest release date for the release.</summary>
    PartialDate Date { get; }

    IEnumerable<ILabelInfo> LabelInfo { get; }

    IEnumerable<IMedium> Media { get; }

    string Packaging { get; }

    Guid? PackagingId { get; }

    string Quality { get; }

    IEnumerable<IReleaseEvent> ReleaseEvents { get; }

    IReleaseGroup ReleaseGroup { get; }

    string Status { get; }

    Guid? StatusId { get; }

    ITextRepresentation TextRepresentation { get; }

  }

}
