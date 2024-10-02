using System;
using System.Linq;

namespace AdventOfCode.Year2017.Days.Day05;

public class Instructions() : Abstractions.Instructions(year: 2017, day: 5)
{
    private int[] _input = [];

    public override void LoadInput() =>
        Reset();

    public override void Reset() =>
        _input = ReadAllLines()
            .Select(int.Parse)
            .ToArray();

    public override object PerformPartOne() =>
        CalculateSteps((int offset) => 1);

    public override object PerformPartTwo() =>
        CalculateSteps((offset) => offset >= 3 ? -1 : 1);

    private int CalculateSteps(Func<int, int> getOffset)
    {
        int steps = 0;

        for (int current = 0; current < _input.Length; ++steps)
        {
            var offset =_input[current];

            _input[current] += getOffset(offset);

            current += offset;
        }

        return steps;
    }
}