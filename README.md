# MetaBrainz.MusicBrainz [![Build Status][CI-S]][CI-L] [![NuGet Version][NuGet-S]][NuGet-L]

This is a .NET implementation of libmusicbrainz, wrapping the
[MusicBrainz v2 API][api-reference].

[MusicBrainz][home] is an open music encyclopedia.

An attempt has been made to keep the same basic class hierarchy, but this library is based on the JSON interface, not the XML one,
so there will be differences.
In addition, interfaces, not classes, are used for the public API (to allow more flexibility for the internals).

This also contains OAuth2 functionality.

[CI-S]: https://github.com/Zastai/MetaBrainz.MusicBrainz/actions/workflows/build.yml/badge.svg
[CI-L]: https://github.com/Zastai/MetaBrainz.MusicBrainz/actions/workflows/build.yml

[NuGet-S]: https://img.shields.io/nuget/v/MetaBrainz.MusicBrainz
[NuGet-L]: https://nuget.org/packages/MetaBrainz.MusicBrainz

[api-reference]: https://musicbrainz.org/doc/MusicBrainz_API
[home]: https://musicbrainz.org/

## Documentation

A [user guide][user-guide] is available, which explains the main functionality of the library, with some examples.

[user-guide]: https://github.com/Zastai/MetaBrainz.MusicBrainz/blob/main/user-guide/UserGuide.md

## Release Notes

These are available [on GitHub][release-notes].

[release-notes]: https://github.com/Zastai/MetaBrainz.MusicBrainz/releases
