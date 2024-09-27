using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2015.Days.Day17;

public class Instructions() : Abstractions.Instructions(year: 2015, day: 17)
{
    private int[] _input = [];

    private const int liters = 150;

    public override void LoadInput() =>
        _input = ReadAllLines()
            .Select(int.Parse)
            .ToArray();

    public override object PerformPartOne() =>
        GetCombinations([], _input, liters).ToArray().Length;

    public override object PerformPartTwo()
    {
        var combinations = GetCombinations([], _input, liters).ToArray();
        var minimumNumberOfContainers = combinations.Min(_ => _.Count);
        return combinations.Count(_ => _.Count == minimumNumberOfContainers);
    }

    private static IEnumerable<List<int>> GetCombinations(List<int> combination, int[] containers, int remaining)
    {
        for (var i = 0; i < containers.Length; i++)
        {
            var container = containers[i];
            if (container > remaining)
                continue;

            var copyCombination = combination.ToList();
            copyCombination.Add(container);

            if (container == remaining)
                yield return copyCombination;

            foreach (var combinations in GetCombinations(copyCombination, containers.Skip(i + 1).ToArray(), liters - copyCombination.Sum()))
                yield return combinations;
        }
    }
}