using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Objects {

  internal sealed class MessageOrError : JsonBasedObject {

    public string? Error;

    public string? Help;

    public string? Message;

  }

}
