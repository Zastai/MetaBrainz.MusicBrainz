namespace MetaBrainz.MusicBrainz.Submissions {

  internal interface ISubmission {

    string Client { get; }

    string Entity { get; }

    string Method { get; }

    string RequestBody { get; }

    string Submit();

  }

}
