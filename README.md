# MusicBrainz
[![Build Status](https://travis-ci.org/Zastai/MusicBrainz.svg?branch=master)](https://travis-ci.org/Zastai/MusicBrainz)

These are .NET implementations of libmusicbrainz, libcoverart and libdiscid.
All will use the same major & minor revision as the corresponding C/C++ libraries, but their own patch level.

NuGet packages will be created for all libraries.

## MetaBrainz.MusicBrainz.dll

This assembly corresponds to the libmusicbrainz library (wrapping the [MusicBrainz v2 API](https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2)).
An attempt will be made to keep the same class hierarchy.

## MetaBrainz.MusicBrainz.CoverArt.dll

This assembly corresponds to the libcoverart library (wrapping the [CoverArtArchive API](https://musicbrainz.org/doc/Cover_Art_Archive/API)).
An attempt has been made to keep the same basic class hierarchy.

## MetaBrainz.MusicBrainz.DiscId.dll

This assembly corresponds to the libdiscid library.

It uses PInvoke to access devices so is platform-dependent; currently, Windows, Linux and BSD (FreeBSD, NetBSD and OpenBSD) are supported (assuming .NET on Windows and Mono on the rest).

Support for Solaris is unlikely, because there does not seem to be any easy way to get Mono to work on it.
Support for OSX is similarly unlikely, because I have no access to a system.

When CoreCLR is released, I'll look into supporting that too (hopefully its PInvoke behaviour is Mono-compatible).

### DiscId.exe

This is a small sample program using MetaBrainz.MusicBrainz.DiscId.dll.
