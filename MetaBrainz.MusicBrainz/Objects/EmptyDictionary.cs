using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MetaBrainz.MusicBrainz.Objects;

/// <summary>An empty dictionary.</summary>
/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
internal sealed class EmptyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue> where TKey : notnull {

  private EmptyDictionary() { }

  public static readonly EmptyDictionary<TKey, TValue> Instance = new();

  /// <inheritdoc />
  public bool ContainsKey(TKey key) => false;

  /// <inheritdoc />
  public int Count => 0;

  /// <inheritdoc />
  public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Enumerable.Empty<KeyValuePair<TKey, TValue>>().GetEnumerator();

  /// <inheritdoc />
  IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

  /// <inheritdoc />
  public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) {
    value = default;
    return false;
  }

  /// <inheritdoc />
  public IEnumerable<TKey> Keys { get; } = [];

  /// <inheritdoc />
  public override string ToString() => "{ }";

  /// <inheritdoc />
  public IEnumerable<TValue> Values { get; } = [];

  /// <inheritdoc />
  public TValue this[TKey key] => throw new KeyNotFoundException("This dictionary is empty.");

}
