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

[user-guide]: https://github.com/Zastai/MetaBrainz.MusicBrainz/blob/main/UserGuide.md

## Debugging

The `OAuth2` and `Query` classes both provide a `TraceSource` that can
be used to configure debug output; their names are
`MetaBrainz.MusicBrainz.OAuth2` and `MetaBrainz.MusicBrainz`,
respectively.

### Configuration

#### In Code

In code, you can enable tracing like follows:

```cs
// Use the default switch, turning it on.
OAuth2.TraceSource.Switch.Level = SourceLevels.All;
Query.TraceSource.Switch.Level = SourceLevels.All;

// Alternatively, use your own switch so multiple things can be
// enabled/disabled at the same time.
var mySwitch = new TraceSwitch("MyAppDebugSwitch", "All");
OAuth2.TraceSource.Switch = mySwitch;
Query.TraceSource.Switch = mySwitch;

// By default, there is a single listener that writes trace events to
// the debug output (typically only seen in an IDE's debugger). You can
// add (and remove) listeners as desired.
var listener = new ConsoleTraceListener {
  Name = "MyAppConsole",
  TraceOutputOptions = TraceOptions.DateTime | TraceOptions.ProcessId,
};
OAuth2.TraceSource.Listeners.Clear();
OAuth2.TraceSource.Listeners.Add(listener);
Query.TraceSource.Listeners.Clear();
Query.TraceSource.Listeners.Add(listener);
```

#### In Configuration

Starting from .NET 7 your application can also be set up to read tracing
configuration from the application configuration file. To do so, the
application needs to add the following to its startup code:

```cs
System.Diagnostics.TraceConfiguration.Register();
```

(Provided by the `System.Configuration.ConfigurationManager` package.)

The application config file can then have a `system.diagnostics` section
where sources, switches and listeners can be configured.

```xml
<configuration>
  <system.diagnostics>
    <sharedListeners>
      <add name="console" type="System.Diagnostics.ConsoleTraceListener" traceOutputOptions="DateTime,ProcessId" />
    </sharedListeners>
    <sources>
      <source name="MetaBrainz.MusicBrainz" switchName="MetaBrainz.MusicBrainz">
        <listeners>
          <add name="console" />
          <add name="mb-log" type="System.Diagnostics.TextWriterTraceListener" initializeData="mb.log" />
        </listeners>
      </source>
      <source name="MetaBrainz.MusicBrainz.OAuth2" switchName="MetaBrainz.MusicBrainz">
        <listeners>
          <add name="console" />
          <add name="mb-oauth2-log" type="System.Diagnostics.TextWriterTraceListener" initializeData="mb.oauth2.log" />
        </listeners>
      </source>
    </sources>
    <switches>
      <add name="MetaBrainz.MusicBrainz" value="All" />
    </switches>
  </system.diagnostics>
</configuration>
```

## Release Notes

These are available [on GitHub][release-notes].

[release-notes]: https://github.com/Zastai/MetaBrainz.MusicBrainz/releases
