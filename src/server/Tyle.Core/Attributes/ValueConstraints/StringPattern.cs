using System.Text.RegularExpressions;

namespace Tyle.Core.Attributes.ValueConstraints;

public class StringPattern : IValueConstraint
{
    public string Pattern { get; }

    /// <summary>
    /// Creates a new string pattern constraint.
    /// </summary>
    /// <param name="pattern">The string representing the pattern that string values must adhere to.</param>
    public StringPattern(string pattern)
    {
        Pattern = pattern;
    }
}
