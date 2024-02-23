using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015.Days.Day09;

public partial class Instructions(int year, int day) : Abstractions.Instructions(year, day)
{
    private Route[] _input = [];

    public override object PerformPartOne()
        => GetDistances().Min();

    public override object PerformPartTwo()
        => GetDistances().Max();

    protected override void LoadInput(string filePath)
        => _input = File.ReadAllLines(filePath)
            .Select(_ => GeneratedRegex().Match(_).Groups)
            .Select(_ => new Route(_[1].Value, _[2].Value, int.Parse(_[3].Value)))
            .ToArray();

    private int[] GetDistances()
    {
        var permutations = Permute(
            _input.SelectMany(_ => new [] {_.From, _.To}).Distinct()
        );

        return permutations
            .Select(_ => GetDistances(_.ToArray(), _input).Sum())
            .ToArray();
    }

    private static IEnumerable<IEnumerable<string>> Permute(IEnumerable<string> sequence)
    {
        var list = sequence.ToList();
        if (list.Count == 0)
            yield return Enumerable.Empty<string>();

        var startingIndex = 0;

        foreach (var element in list)
        {
            var index = startingIndex;
            var remaining = list.Where((_, i) => i != index);

            foreach (var remainder in Permute(remaining))
                yield return remainder.Prepend(element);

            startingIndex++;
        }
    }

    private static IEnumerable<int> GetDistances(IReadOnlyList<string> cities, Route[] routeArray)
    {
        for (var i = 0; i < cities.Count - 1; i++)
            yield return routeArray.First(_ => 
                    _.From == cities[i] && _.To == cities[i + 1]
                    || _.To == cities[i] && _.From == cities[i + 1]
                )
                .Distance;
    }

    [GeneratedRegex(@"^(\w+) to (\w+) = (\d+)$")]
    private static partial Regex GeneratedRegex();
}
