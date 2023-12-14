using System.Net.Http;

namespace MetaBrainz.MusicBrainz.Interfaces.Submissions;

internal interface ISubmission {

  string Client { get; }

  string ContentType { get; }

  string Entity { get; }

  HttpMethod Method { get; }

  string? RequestBody { get; }

}
