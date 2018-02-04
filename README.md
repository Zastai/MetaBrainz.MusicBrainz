# MusicBrainz [![Build Status](https://ci.appveyor.com/api/projects/status/idw9x7x6iymwdhjh?svg=true)](https://ci.appveyor.com/project/Zastai/musicbrainz)

These are .NET implementations of libmusicbrainz, libcoverart and libdiscid.
Each will have its own NuGet package.

## MetaBrainz.MusicBrainz.dll [![NuGet Version](https://badge.fury.io/nu/MetaBrainz.MusicBrainz.svg)](https://badge.fury.io/nu/MetaBrainz.MusicBrainz)

This assembly corresponds to the libmusicbrainz library (wrapping the [MusicBrainz v2 API](https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2)).
An attempt has been made to keep the same basic class hierarchy, but this library is based on the JSON interface, not the XML one, so there will be differences.
In addition, interfaces, not classes, are used for the public API (to allow more flexibility for the internals).

This also contains OAuth2 functionality.

## MetaBrainz.MusicBrainz.CoverArt.dll [![NuGet Version](https://badge.fury.io/nu/MetaBrainz.MusicBrainz.CoverArt.svg)](https://badge.fury.io/nu/MetaBrainz.MusicBrainz.CoverArt)

This assembly corresponds to the libcoverart library (wrapping the [CoverArtArchive API](https://musicbrainz.org/doc/Cover_Art_Archive/API)).
An attempt has been made to keep the same basic class hierarchy.

## MetaBrainz.MusicBrainz.DiscId.dll [![NuGet Version](https://badge.fury.io/nu/MetaBrainz.MusicBrainz.DiscId.svg)](https://badge.fury.io/nu/MetaBrainz.MusicBrainz.DiscId)

This assembly corresponds to the libdiscid library.
The main point of divergence at this point is that fewer platforms are supported (see below), and that this library supports retrieval of CD-TEXT information.

It uses PInvoke to access devices so is platform-dependent; currently, Windows, Linux and BSD (FreeBSD, NetBSD and OpenBSD) are supported.
However, things should work regardless of the host implementation (.NET Framework, Mono or .NET Core).

Support for Solaris is unlikely, because there does not seem to be any easy way to get Mono to work on it.
Support for OSX is similarly unlikely, because I have no access to a system.

### DiscId.exe

This is a small sample program using MetaBrainz.MusicBrainz.DiscId.dll.
It mostly corresponds to the `Show-MusicBrainz-TableOfContents` powershell cmdlet (see below).

### Powershell Module

A Powershell module is provided as well.

To make it available, create a `WindowsPowershell\Modules\MetaBrainz.MusicBrainz.DiscId` inside your "My Documents" folder.
Then place both MetaBrainz.MusicBrainz.DiscId.dll and MetaBrainz.MusicBrainz.DiscId.psm1 in that directory.
Still to come: help files and an installer (possibly via chocolatey).

| Command | Description |
| ------- | ----------- |
| `Get-MusicBrainz-AvailableDevices`  | Returns the list of available devices. |
| `Get-MusicBrainz-AvailableFeatures` | Returns the available disc read features. |
| `Get-MusicBrainz-DefaultDevice`     | Returns the default device. |
| `Get-MusicBrainz-DiscId`            | Reads a device's table of contents and returns the Disc ID for it. |
| `Get-MusicBrainz-SubmissionUrl`     | Reads a device's table of contents and returns the MusicBrainz submission URL for it. |
| `Show-MusicBrainz-TableOfContents`  | Shows information about a device's table of contents. |
