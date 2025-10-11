using System.Text.Json;

namespace MetaBrainz.MusicBrainz.Json;

/// <summary>An exception thrown when a JSON object is missing a required property.</summary>
public sealed class MissingPropertyException(string name) : JsonException($"Expected property '{name}' not found or null.");
