using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A MusicBrainz release.</summary>
  [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IRelease : IEntity, IAnnotatedEntity, IRelatableEntity, ITaggableEntity, ITitledEntity {

    /// <summary>The artist credit for the release.</summary>
    IEnumerable<INameCredit> ArtistCredit { get; }

    /// <summary>The Amazon Standard Identification Number (ASIN) associated with the release.</summary>
    string Asin { get; }

    /// <summary>The "barcode" (i.e. the UPC or EAN) associated with the release.</summary>
    string BarCode { get; }

    /// <summary>The collections containing the release, if any.</summary>
    IEnumerable<ICollection> Collections { get; }

    /// <summary>The ISO 3166-1 code for the (primary) country associated with the release.</summary>
    string Country { get; }

    /// <summary>Information about the release's covert art on the Cover Art Archive (CAA).</summary>
    ICoverArtArchive CoverArtArchive { get; }

    /// <summary>The earliest release date for the release.</summary>
    PartialDate Date { get; }

    /// <summary>The label information for the release.</summary>
    IEnumerable<ILabelInfo> LabelInfo { get; }

    /// <summary>The media comprising the release.</summary>
    IEnumerable<IMedium> Media { get; }

    /// <summary>The packaging for the release, expressed as text.</summary>
    string Packaging { get; }

    /// <summary>The packaging for the release, expressed as an MBID.</summary>
    Guid? PackagingId { get; }

    /// <summary>The data quality for the release.</summary>
    string Quality { get; }

    /// <summary>The release events for the release.</summary>
    IEnumerable<IReleaseEvent> ReleaseEvents { get; }

    /// <summary>The release group the release belongs to.</summary>
    IReleaseGroup ReleaseGroup { get; }

    /// <summary>The status of the release, expressed as text.</summary>
    string Status { get; }

    /// <summary>The status of the release, expressed as an MBID.</summary>
    Guid? StatusId { get; }

    /// <summary>The release's textual representation.</summary>
    ITextRepresentation TextRepresentation { get; }

  }

}
