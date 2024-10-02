using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2018.Days.Day02;

public class Instructions() : Abstractions.Instructions(year: 2018, day: 2)
{
    private string[] _identifiers = [];

    public override void LoadInput() =>
        _identifiers = [.. ReadAllLines()];

    public override object PerformPartOne() 
    {
        var doubles = 0;
        var triples = 0;

        foreach (var identifier in _identifiers)
        {
            var groups = identifier
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());

            doubles += groups.Values.Any(v => v == 2) ? 1 : 0;
            triples += groups.Values.Any(v => v == 3) ? 1 : 0;
        }

        return doubles * triples;
    }

    public override object PerformPartTwo() 
    {
        for (var i = 0; i + 1 < _identifiers.Length; i++)
        {
            var current = _identifiers[i];
            for (var j = i + 1; j < _identifiers.Length; j++)
            {
                var next = _identifiers[j];
                var matching = string.Empty;
                for (var position = 0; position < current.Length; position++)
                    if (current[position] == next[position])
                        matching += current[position];
                
                if (matching.Length == current.Length - 1)
                    return matching;
            }
        }

        return "UNKNOWN";
    }
}