using System;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Json.Readers;

namespace MetaBrainz.MusicBrainz;

/// <summary>A partial date. Can contain any or all of year, month and day.</summary>
[JsonConverter(typeof(PartialDateReader))]
[PublicAPI]
public sealed class PartialDate : IComparable<PartialDate>, IEquatable<PartialDate> {

  #region Constructors

  /// <summary>Creates a new partial date with the specified year/month/day components.</summary>
  /// <param name="year">The year component, if any.</param>
  /// <param name="month">The month component, if any.</param>
  /// <param name="day">The day component, if any.</param>
  /// <exception cref="ArgumentOutOfRangeException">
  /// When <paramref name="month"/> and/or <paramref name="day"/> have an invalid value.
  /// </exception>
  public PartialDate(int? year = null, int? month = null, int? day = null) {
    if (year.HasValue && (year.Value < PartialDate.MinYear || year.Value > PartialDate.MaxYear)) {
      var msg = $"The year, if specified, should be between {PartialDate.MinYear} and {PartialDate.MaxYear}.";
      throw new ArgumentOutOfRangeException(nameof(year), year, msg);
    }
    if (month.HasValue && (month.Value < 1 || month.Value > 12)) {
      throw new ArgumentOutOfRangeException(nameof(month), month, "The month, if specified, should be between 1 and 12.");
    }
    if (day.HasValue) {
      var maxDays = 31;
      if (month.HasValue) {
        if (year.HasValue) {
          maxDays = DateTime.DaysInMonth(year.Value, month.Value);
        }
        else if (month.Value == 2) {
          maxDays = 29;
        }
        else if (month.Value == 4 || month.Value == 6 || month.Value == 9 || month.Value == 11) {
          maxDays = 30;
        }
      }
      if (day.Value < 1 || day.Value > maxDays) {
        const string msg = "The day, if specified, should be between 1 and 28/29/30/31 (depending on the month).";
        throw new ArgumentOutOfRangeException(nameof(day), day, msg);
      }
    }
    this.Year  = year;
    this.Month = month;
    this.Day   = day;
  }

  private static readonly Regex Format =
    new(@"\A([?]+|[0-9]{1,4})(?:-([?]+|0?[1-9]|1[0-2])(?:-([?]+|0?[1-9]|[12][0-9]|3[01]))?)?\Z");

  /// <summary>Creates a new partial date based on the given string representation.</summary>
  /// <param name="text">
  /// The text form for the date.
  /// Should be in the same form as produced by <see cref="ToString()"/>, i.e. <c>YYYY[-MM[-DD]]</c>, with question marks used for
  /// unspecified parts.
  /// </param>
  public PartialDate(string? text) {
    if (text == null) {
      return; // ok, null
    }
    text = text.Trim();
    if (text.Length == 0) {
      return; // ok, empty
    }
    var match = PartialDate.Format.Match(text);
    var ok = match.Success;
    if (ok) {
      if (match.Groups[1].Success) {
        if (!match.Groups[1].Value.Contains("?")) {
          this.Year = int.Parse(match.Groups[1].Value, NumberStyles.None);
        }
      }
      if (match.Groups[2].Success) {
        if (!match.Groups[2].Value.Contains("?")) {
          this.Month = int.Parse(match.Groups[2].Value, NumberStyles.None);
        }
      }
      if (match.Groups[3].Success) {
        if (!match.Groups[3].Value.Contains("?")) {
          var day = int.Parse(match.Groups[3].Value, NumberStyles.None);
          var maxDays = 31;
          if (this.Month.HasValue) {
            if (this.Year.HasValue) {
              maxDays = DateTime.DaysInMonth(this.Year.Value, this.Month.Value);
            }
            else if (this.Month.Value == 2) {
              maxDays = 29;
            }
            else if (this.Month.Value == 4 || this.Month.Value == 6 || this.Month.Value == 9 || this.Month.Value == 11) {
              maxDays = 30;
            }
          }
          if (day > maxDays) {
            ok = false;
          }
          else {
            this.Day = day;
          }
        }
      }
    }
    if (!ok) {
      throw new FormatException("The specified partial date string is not of the form YYYY[-MM[-DD]] (with question marks for " +
                                "unspecified parts).");
    }
  }

  #endregion

  #region Constants

  /// <summary>An empty partial date.</summary>
  public static readonly PartialDate Empty = new();

  /// <summary>The smallest value allowed for a partial date's year component.</summary>
  public static readonly int MinYear = DateTime.MinValue.Year;

  /// <summary>The largest value allowed for a partial date's year component.</summary>
  public static readonly int MaxYear = DateTime.MaxValue.Year;

  #endregion

  #region Properties

  /// <summary>The year component, if any, of this partial date.</summary>
  public int? Year { get; }

  /// <summary>The month component, if any, of this partial date.</summary>
  public int? Month { get; }

  /// <summary>The day component, if any, of this partial date.</summary>
  public int? Day { get; }

  /// <summary>A flag indicating whether or not this partial date is empty.</summary>
  public bool IsEmpty => !(this.Year.HasValue || this.Month.HasValue || this.Day.HasValue);

  /// <summary>
  /// The nearest complete (Gregorian) date/time value for this partial date.
  /// An unspecified year, month or day is considered to be 1.
  /// </summary>
  public DateTime NearestDate => new(this.Year ?? 1, this.Month ?? 1, this.Day ?? 1);

  #endregion

  #region Methods

  /// <summary>Converts this partial date to a string representation.</summary>
  /// <returns>A string of the form <c>YYYY[-MM[-DD]]</c>, with question marks used for unspecified parts.</returns>
  public override string ToString() {
    var sb = new StringBuilder();
    if (this.Year.HasValue) {
      sb.Append($"{this.Year.Value:D4}");
    }
    if (this.Month.HasValue) {
      if (sb.Length < 4) {
        sb.Append("????");
      }
      sb.Append($"-{this.Month.Value:D2}");
    }
    if (this.Day.HasValue) {
      if (sb.Length < 4) {
        sb.Append("????");
      }
      if (sb.Length < 7) {
        sb.Append("-??");
      }
      sb.Append($"-{this.Day.Value:D2}");
    }
    return sb.ToString();
  }

  #endregion

  #region Operators

  #region IComparable

  /// <summary>Compares two partial dates.</summary>
  /// <param name="other">The partial date to compare to this one.</param>
  /// <returns>
  /// -1 if this partial date precedes <paramref name="other"/>, 1 if <paramref name="other"/> precedes this partial date, and 0
  /// otherwise.
  /// </returns>
  public int CompareTo(PartialDate? other) {
    if (other is null) {
      return +1;
    }
    if (this.Year.HasValue) {
      if (other.Year.HasValue) { // YYYY vs YYYY
        if (this.Year.Value < other.Year.Value) {
          return -1;
        }
        if (this.Year.Value > other.Year.Value) {
          return +1;
        }
      }
      else {
        return 0; // can't usefully compare YYYY vs ????
      }
    }
    else if (other.Year.HasValue) {
      return 0; // can't usefully compare ???? vs YYYY
    }
    if (this.Month.HasValue) {
      if (other.Month.HasValue) { // YYYY-MM vs YYYY-MM
        if (this.Month.Value < other.Month.Value) {
          return -1;
        }
        if (this.Month.Value > other.Month.Value) {
          return +1;
        }
      }
      else {
        return 0; // can't usefully compare YYYY-MM vs YYYY-??
      }
    }
    else if (other.Month.HasValue) {
      return 0; // can't usefully compare YYYY-?? vs YYYY-MM
    }
    if (this.Day.HasValue && other.Day.HasValue) { // YYYY-MM-DD vs YYYY-MM-DD
      if (this.Day.Value < other.Day.Value) {
        return -1;
      }
      if (this.Day.Value > other.Day.Value) {
        return +1;
      }
    }
    return 0;
  }

  /// <summary>Compares two partial dates.</summary>
  /// <param name="lhs">The first partial date to compare.</param>
  /// <param name="rhs">The seconds partial date to compare .</param>
  /// <returns>
  /// <see langword="true"/> if <paramref name="lhs"/> precedes <paramref name="rhs"/>; <see langword="false"/> otherwise.
  /// </returns>
  public static bool operator<(PartialDate? lhs, PartialDate? rhs) => lhs is null ? rhs is not null : lhs.CompareTo(rhs) < 0;

  /// <summary>Compares two partial dates.</summary>
  /// <param name="lhs">The first partial date to compare.</param>
  /// <param name="rhs">The seconds partial date to compare .</param>
  /// <returns>
  /// <see langword="true"/> if <paramref name="rhs"/> does not precede <paramref name="lhs"/>; <see langword="false"/> otherwise.
  /// </returns>
  public static bool operator<=(PartialDate? lhs, PartialDate? rhs) => lhs is null || lhs.CompareTo(rhs) <= 0;

  /// <summary>Compares two partial dates.</summary>
  /// <param name="lhs">The first partial date to compare.</param>
  /// <param name="rhs">The seconds partial date to compare .</param>
  /// <returns>
  /// <see langword="true"/> if <paramref name="lhs"/> does not precede <paramref name="rhs"/>; <see langword="false"/> otherwise.
  /// </returns>
  public static bool operator>=(PartialDate? lhs, PartialDate? rhs) => lhs is null ? rhs is null : lhs.CompareTo(rhs) >= 0;

  /// <summary>Compares two partial dates.</summary>
  /// <param name="lhs">The first partial date to compare.</param>
  /// <param name="rhs">The seconds partial date to compare .</param>
  /// <returns>
  /// <see langword="true"/> if <paramref name="rhs"/> precedes <paramref name="lhs"/>; <see langword="false"/> otherwise.
  /// </returns>
  public static bool operator>(PartialDate? lhs, PartialDate? rhs) => lhs is not null && lhs.CompareTo(rhs) > 0;

  #endregion

  #region IEquatable

  /// <summary>Determines whether or not a given object is a partial date with the same contents as this one.</summary>
  /// <param name="obj">The object to compare to this one.</param>
  /// <returns>
  /// <see langword="true"/> if <paramref name="obj"/> is a <see cref="PartialDate"/> and has the same contents as this one;
  /// <see langword="false"/> otherwise.
  /// </returns>
  public override bool Equals(object? obj) => this.Equals(obj as PartialDate);

  /// <summary>Determines whether or not two partial dates have the same contents.</summary>
  /// <param name="other">The partial date to compare to this one.</param>
  /// <returns>
  /// <see langword="true"/> if the two partial dates have the same contents; <see langword="false"/> otherwise.
  /// </returns>
  public bool Equals(PartialDate? other) {
    if (other is null) {
      return false;
    }
    return (this.Year == other.Year) && (this.Month == other.Month) && (this.Day == other.Day);
  }

  /// <summary>Gets a hash code for this partial date.</summary>
  /// <returns>
  /// The value of this partial date as an integer of the form <c>YYYYMMDD</c>, with 0 used for unspecified components.
  /// </returns>
  public override int GetHashCode()
    => (((this.Year.GetValueOrDefault() * 100) + this.Month.GetValueOrDefault()) * 100) + this.Day.GetValueOrDefault();

  /// <summary>Determines whether or not two partial dates have the same contents.</summary>
  /// <param name="lhs">The first partial date to compare.</param>
  /// <param name="rhs">The seconds partial date to compare .</param>
  /// <returns>
  /// <see langword="true"/> if <paramref name="lhs"/> and <paramref name="rhs"/> have the same contents; <see langword="false"/>
  /// otherwise.
  /// </returns>
  public static bool operator==(PartialDate? lhs, PartialDate? rhs) => lhs?.Equals(rhs) ?? rhs is null;

  /// <summary>Determines whether or not two partial dates have the same contents.</summary>
  /// <param name="lhs">The first partial date to compare.</param>
  /// <param name="rhs">The seconds partial date to compare .</param>
  /// <returns>
  /// <see langword="true"/> if <paramref name="lhs"/> and <paramref name="rhs"/> do not have have the same contents;
  /// <see langword="false"/> otherwise.
  /// </returns>
  public static bool operator!=(PartialDate? lhs, PartialDate? rhs) => !(lhs == rhs);

  #endregion

  #endregion

}
