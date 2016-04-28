using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle        ("MetaBrainz.MusicBrainz")]
[assembly: AssemblyDescription  (".NET Implementation of libmusicbrainz")]
[assembly: AssemblyCompany      ("MetaBrainz")]
[assembly: AssemblyProduct      ("libmusicbrainz")]
[assembly: AssemblyCopyright    ("Copyright © Tim Van Holder 2016")]

[assembly: ComVisible(false)]

// This should be MAJOR.MINOR, matching libmusicbrainz.
[assembly: AssemblyVersion    ("5.1")]

// This should be MAJOR.MINOR.PATCHLEVEL, with MAJOR.MINOR matching libmusicbrainz and PATCHLEVEL as a release sequence number (not following libmusicbrainz).
[assembly: AssemblyFileVersion("5.1.0")]

// This should match AssemblyFileVersion, plus optional extra tags (such as a pre-release indicator).
[assembly: AssemblyInformationalVersion("5.1.0 (pre-release)")]
