using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2018.Days.Day01;

public class Instructions() : Abstractions.Instructions(year: 2018, day: 1)
{
    private int[] _frequencies = [];

    public override void LoadInput() =>
        _frequencies = ReadAllLines()
            .Select(int.Parse)
            .ToArray();

    public override object PerformPartOne() =>
        _frequencies.Sum();

    public override object PerformPartTwo() 
    {
        var frequency = 0;
        var results = new HashSet<int>();
        for (int i = 0; ; i++)
        {
            if (!results.Add(frequency))
                break;

            frequency += _frequencies[i % _frequencies.Length];
        }

        return frequency;
    }
}