# MetaBrainz.MusicBrainz [![Build Status][CI-S]][CI-L] [![NuGet Version][NuGet-S]][NuGet-L]

This is a .NET implementation of libmusicbrainz, wrapping the
[MusicBrainz v2 API][APIv2].
An attempt has been made to keep the same basic class hierarchy, but this library is based on the JSON interface, not the XML one,
so there will be differences.
In addition, interfaces, not classes, are used for the public API (to allow more flexibility for the internals).

This also contains OAuth2 functionality.

## Documentation

A [user guide][UserGuide] is available, which explains the main functionality of the library, with some examples.

## Release Notes

These are available [on GitHub][GHReleases].

[CI-S]: https://img.shields.io/appveyor/build/zastai/metabrainz-musicbrainz
[CI-L]: https://ci.appveyor.com/project/Zastai/metabrainz-musicbrainz

[NuGet-S]: https://img.shields.io/nuget/v/MetaBrainz.MusicBrainz
[NuGet-L]: https://www.nuget.org/packages/MetaBrainz.MusicBrainz

[APIv2]: https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2
[UserGuide]: https://github.com/Zastai/MetaBrainz.MusicBrainz/blob/main/UserGuide.md
[GHReleases]: https://github.com/Zastai/MetaBrainz.MusicBrainz/releases
