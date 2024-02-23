using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015.Days.Day08;

public partial class Instructions(int year, int day) : Abstractions.Instructions(year, day)
{
    private string[] _input = [];

    public override object PerformPartOne()
        => _input
        .Select(x =>
            new
            {
                Original = x,
                Raw = GeneratedRegex().Replace(
                    x.Substring(1, x.Length - 2) // Need to remove both quotes, at the start and end.
                    .Replace("\\\"", "\"") // Representing the lone double-quote character
                    .Replace("\\\\", "?"), "?"
                )
            }
        )
        .Sum(x => x.Original.Length - x.Raw.Length);

    public override object PerformPartTwo()
        => _input
        .Select(x =>
            new
            {
                Original = x,
                Escaped =
                    "\"" + // Add the beginning double quote character
                    x.Replace("\\", "\\\\") // Representing the single backslash
                        .Replace("\"", "\\\"") // Representing the lone double-quote character
                    + "\"" // Add the ending double quote character
            }
        )
        .Sum(x => x.Escaped.Length - x.Original.Length);

    protected override void LoadInput(string filePath)
        => _input = File.ReadAllLines(filePath);

    [GeneratedRegex(@"\\x[0-9a-f]{2}")]
    private static partial Regex GeneratedRegex();
}
