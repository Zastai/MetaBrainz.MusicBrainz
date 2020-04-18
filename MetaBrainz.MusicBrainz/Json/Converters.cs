using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

using MetaBrainz.Common.Json.Converters;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Json.Readers;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz.Json {

  internal static class Converters {

    private static JsonConverter[] _toBeReplacedByReaders = {
        // Mappers for interfaces that appear in scalar properties.
        // @formatter:off
        new InterfaceConverter<IArea,               Area              >(),
        new InterfaceConverter<IArtist,             Artist            >(),
        new InterfaceConverter<ICoordinates,        Coordinates       >(),
        new InterfaceConverter<ICoverArtArchive,    CoverArtArchive   >(),
        new InterfaceConverter<IEvent,              Event             >(),
        new InterfaceConverter<IInstrument,         Instrument        >(),
        new InterfaceConverter<ILabel,              Label             >(),
        new InterfaceConverter<ILifeSpan,           LifeSpan          >(),
        new InterfaceConverter<IPlace,              Place             >(),
        new InterfaceConverter<IRating,             Rating            >(),
        new InterfaceConverter<IRecording,          Recording         >(),
        new InterfaceConverter<IRelease,            Release           >(),
        new InterfaceConverter<IReleaseGroup,       ReleaseGroup      >(),
        new InterfaceConverter<ISeries,             Series            >(),
        new InterfaceConverter<ITextRepresentation, TextRepresentation>(),
        new InterfaceConverter<ITrack,              Track             >(),
        new InterfaceConverter<IUrl,                Url               >(),
        new InterfaceConverter<IUserRating,         UserRating        >(),
        new InterfaceConverter<IWork,               Work              >(),
        // @formatter:on
        // Mappers for interfaces that appear in array properties.
        // @formatter:off
        new ReadOnlyListOfInterfaceConverter<IAlias,         Alias        >(),
        new ReadOnlyListOfInterfaceConverter<ICollection,    Collection   >(),
        new ReadOnlyListOfInterfaceConverter<IDisc,          Disc         >(),
        new ReadOnlyListOfInterfaceConverter<ILabelInfo,     LabelInfo    >(),
        new ReadOnlyListOfInterfaceConverter<IMedium,        Medium       >(),
        new ReadOnlyListOfInterfaceConverter<INameCredit,    NameCredit   >(),
        new ReadOnlyListOfInterfaceConverter<IRecording,     Recording    >(),
        new ReadOnlyListOfInterfaceConverter<IRelationship,  Relationship >(),
        new ReadOnlyListOfInterfaceConverter<IRelease,       Release      >(),
        new ReadOnlyListOfInterfaceConverter<IReleaseEvent,  ReleaseEvent >(),
        new ReadOnlyListOfInterfaceConverter<IReleaseGroup,  ReleaseGroup >(),
        new ReadOnlyListOfInterfaceConverter<ISimpleTrack,   SimpleTrack  >(),
        new ReadOnlyListOfInterfaceConverter<ITag,           Tag          >(),
        new ReadOnlyListOfInterfaceConverter<ITrack,         Track        >(),
        new ReadOnlyListOfInterfaceConverter<IUserTag,       UserTag      >(),
        new ReadOnlyListOfInterfaceConverter<IWork,          Work         >(),
        new ReadOnlyListOfInterfaceConverter<IWorkAttribute, WorkAttribute>(),
        // @formatter:on
    };

    public static IEnumerable<JsonConverter> Readers {
      get {
        yield return PartialDateReader.Instance;
        foreach (var removeMe in Converters._toBeReplacedByReaders)
          yield return removeMe;
        // This one tries to create useful types for a field of type 'object' (used for all unknown properties)
        yield return AnyObjectReader.Instance;
      }
    }

    public static IEnumerable<JsonConverter> Writers => Enumerable.Empty<JsonConverter>();

  }

}
