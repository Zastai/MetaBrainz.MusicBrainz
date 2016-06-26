# MusicBrainz [![Build Status](https://travis-ci.org/Zastai/MusicBrainz.svg?branch=master)](https://travis-ci.org/Zastai/MusicBrainz)

These are .NET implementations of libmusicbrainz, libcoverart and libdiscid.
Each will have its own NuGet package.

## MetaBrainz.MusicBrainz.dll [![NuGet version](https://badge.fury.io/nu/MetaBrainz.MusicBrainz.svg)](https://badge.fury.io/nu/MetaBrainz.MusicBrainz)

This assembly corresponds to the libmusicbrainz library (wrapping the [MusicBrainz v2 API](https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2)).
An attempt has been made to keep the same basic class hierarchy.

This also contains OAuth2 functionality.

## MetaBrainz.MusicBrainz.CoverArt.dll [![NuGet version](https://badge.fury.io/nu/MetaBrainz.MusicBrainz.CoverArt.svg)](https://badge.fury.io/nu/MetaBrainz.MusicBrainz.CoverArt)

This assembly corresponds to the libcoverart library (wrapping the [CoverArtArchive API](https://musicbrainz.org/doc/Cover_Art_Archive/API)).
An attempt has been made to keep the same basic class hierarchy.

## MetaBrainz.MusicBrainz.DiscId.dll [![NuGet version](https://badge.fury.io/nu/MetaBrainz.MusicBrainz.DiscId.svg)](https://badge.fury.io/nu/MetaBrainz.MusicBrainz.DiscId)

This assembly corresponds to the libdiscid library.
The main point of divergence at this point is that fewer platforms are supported (see below), and that this library supports retrieval of CD-TEXT information.

It uses PInvoke to access devices so is platform-dependent; currently, Windows, Linux and BSD (FreeBSD, NetBSD and OpenBSD) are supported
(assuming .NET on Windows and Mono on the rest).

Support for Solaris is unlikely, because there does not seem to be any easy way to get Mono to work on it.
Support for OSX is similarly unlikely, because I have no access to a system.

When CoreCLR is released, I'll look into supporting that too (hopefully its PInvoke behaviour is Mono-compatible).

### DiscId.exe

This is a small sample program using MetaBrainz.MusicBrainz.DiscId.dll.
