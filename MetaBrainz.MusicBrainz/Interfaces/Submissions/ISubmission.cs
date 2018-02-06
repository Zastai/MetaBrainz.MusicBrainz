namespace MetaBrainz.MusicBrainz.Interfaces.Submissions {

  internal interface ISubmission {

    string Client { get; }

    string ContentType { get; }

    string Entity { get; }

    Method Method { get; }

    string RequestBody { get; }

  }

}
