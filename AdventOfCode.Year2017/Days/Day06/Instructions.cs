using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2017.Days.Day06;

public class Instructions() : Abstractions.Instructions(year: 2017, day: 6)
{
    private List<string> _occurences = [];
    private int[] _banks = [];
    public string _input = string.Empty;

    public override void LoadInput()
    {
        _input = ReadAllText();
        _occurences = [_input];
        _banks = _input
            .Split('\t')
            .Select(int.Parse)
            .ToArray();
    }

    public override object PerformPartOne()
    {
        while (true)
        {
            var index = Array.IndexOf(_banks, _banks.Max());
            var value = _banks[index];

            _banks[index] = 0;
            for (; value > 0; --value)
            {
                index = index + 1 == _banks.Length ? 0 : index + 1;
                _banks[index]++;
            }

            _input = string.Join("\t", _banks);
            if (_occurences.Contains(_input))
                break;

            _occurences.Add(_input);
        }

        return _occurences.Count;
    }

    public override object PerformPartTwo() =>
        // No need to recalculate anything, just return the expected value.
        _occurences.Count - _occurences.IndexOf(_input);
}