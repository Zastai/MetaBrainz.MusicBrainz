# MetaBrainz.MusicBrainz [![Build Status](https://img.shields.io/appveyor/build/zastai/metabrainz-musicbrainz)](https://ci.appveyor.com/project/Zastai/metabrainz-musicbrainz) [![NuGet Version](https://img.shields.io/nuget/v/MetaBrainz.MusicBrainz)](https://www.nuget.org/packages/MetaBrainz.MusicBrainz)

This is a .NET implementation of libmusicbrainz, wrapping the [MusicBrainz v2 API](https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2).
An attempt has been made to keep the same basic class hierarchy, but this library is based on the JSON interface, not the XML one, so there will be differences.
In addition, interfaces, not classes, are used for the public API (to allow more flexibility for the internals).

This also contains OAuth2 functionality.
