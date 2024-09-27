using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015.Days.Day19;

public partial class Instructions() : Abstractions.Instructions(year: 2015, day: 19)
{
    private Replacement[] _input = [];
    private string _molecule = string.Empty;

    public override void LoadInput()
    {
        var lines = ReadAllLines();
        _molecule = lines[^1];
        _input = lines
            .Select(_ => GeneratedRegex().Match(_).Groups)
            .Where(_ => _[0].Success)
            .Select(_ => new Replacement(_[1].Value, _[2].Value))
            .ToArray();
    }

    public override object PerformPartOne() =>
        _input
            .Select(ReplaceMolecule)
            .SelectMany(_ => _)
            .Distinct()
            .Count();

    public override object PerformPartTwo()
    {
        var target = _molecule;
        var mutations = 0;

        while (target != "e")
        {
            var tmp = target;
            foreach (var replacement in _input)
            {
                var index = target.IndexOf(replacement.Value, StringComparison.Ordinal);
                if (index >= 0)
                {
                    target = string.Concat(target.AsSpan(0, index), replacement.Key, target.AsSpan(index + replacement.Value.Length));
                    mutations++;
                }
            }

            if (tmp == target)
            {
                target = _molecule;
                mutations = 0;
                _input = Shuffle(_input).ToArray();
            }
        }
        return mutations;
    }

    IEnumerable<string> ReplaceMolecule(Replacement replacement) =>
        Regex.Matches(_molecule, replacement.Key)
            .Select(_ => new Regex(replacement.Key).Replace(_molecule, replacement.Value, 1, _.Index));

    static IEnumerable<T> Shuffle<T>(IEnumerable<T> source) =>
        source.OrderBy(_ => new Random().Next());
    
    [GeneratedRegex(@"^(\w+) => (\w+)$")]
    private static partial Regex GeneratedRegex();
}