using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024.Days.Day01;

public partial class Instructions() : Abstractions.Instructions(year: 2024, day: 1)
{
    private (int, int)[] _locationIdentifiers = [];

    public override void LoadInput() =>
        _locationIdentifiers = ReadAllLines()
            .Select(line => GeneratedRegex().Match(line).Groups)
            .Select(line => (int.Parse(line[1].Value), int.Parse(line[2].Value)))
            .ToArray();

    public override object PerformPartOne()
    {
        var leftIds = _locationIdentifiers.Select(id => id.Item1).OrderBy(id => id).ToArray();
        var rightIds = _locationIdentifiers.Select(id => id.Item2).OrderBy(id => id).ToArray();

        return GetDifferences().Sum();

        IEnumerable<int> GetDifferences()
        {
            for (var index = 0; index < leftIds.Length; index++)
                yield return Math.Abs(leftIds[index] - rightIds[index]);
        }
    }

    public override object PerformPartTwo() =>
        _locationIdentifiers
            .Select(leftId => leftId.Item1 * _locationIdentifiers.Count(rightId => rightId.Item2 == leftId.Item1))
            .Sum();

    [GeneratedRegex(@"(\d+)\s+(\d+)")]
    private static partial Regex GeneratedRegex();
}