using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2015.Days.Day24;

internal class Instructions() : Abstractions.Instructions(2015, 24)
{
    private List<long> _input = [];
    private long _weightPerGroup;
    private int _bestNumberOfPackagesPerGroup;

    public override void LoadInput() =>
        _input = ReadAllLines()
            .Select(long.Parse)
            .ToList();

    public override object PerformPartOne() =>
        CalculateBestQuantumEntanglement(3);

    public override object PerformPartTwo() =>
        CalculateBestQuantumEntanglement(4);

    private long CalculateBestQuantumEntanglement(int packagesPerGroup)
    {
        _weightPerGroup = _input.Sum() / packagesPerGroup;
        _bestNumberOfPackagesPerGroup = 1 + _input.Count / packagesPerGroup;

        return Distribute([], _input)
            .Select(g => new
            {
                g.Count,
                QuantumEntanglement = g.Aggregate((a, b) => a * b)
            })
            .OrderBy(g => g.Count)
            .ThenBy(g => g.QuantumEntanglement)
            .First()
            .QuantumEntanglement;
    }

    private IEnumerable<List<long>> Distribute(List<long> packages, List<long> packagePool)
    {
        // By keep track of the best number of packages per group, we can skip a lot of options.
        if (packages.Count >= _bestNumberOfPackagesPerGroup)
            yield break;

        var remaining = _weightPerGroup - packages.Sum();
        for (var index = 0; index < packagePool.Count; index++)
        {
            if (packagePool[index] > remaining)
                continue;

            var package = packagePool[index];
            var newUsed = packages.ToList();
            newUsed.Add(package);
            if (package == remaining)
            {
                if (packages.Count < _bestNumberOfPackagesPerGroup)
                    _bestNumberOfPackagesPerGroup = packages.Count;

                yield return packages;
            }
            else
            {
                var newPool = packagePool.Skip(index + 1).ToList();
                foreach (var distributed in Distribute(newUsed, newPool))
                {
                    yield return distributed;
                }
            }
        }
    }
}