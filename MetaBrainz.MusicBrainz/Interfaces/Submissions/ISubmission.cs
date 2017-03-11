namespace MetaBrainz.MusicBrainz.Interfaces.Submissions {

  internal interface ISubmission {

    string Client { get; }

    string ContentType { get; }

    string Entity { get; }

    string Method { get; }

    string RequestBody { get; }

  }

}
