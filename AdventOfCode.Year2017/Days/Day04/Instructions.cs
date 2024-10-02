using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2017.Days.Day04;

public class Instructions() : Abstractions.Instructions(year: 2017, day: 4)
{
    private string[][] _input = [];

    public override void LoadInput() =>
        _input = ReadAllLines()
            .Select(line => line.Split(' '))
            .ToArray();

    public override object PerformPartOne()
    {
        return CountValidLines().Sum();

        IEnumerable<int> CountValidLines()
        {
            foreach (var words in _input)
                yield return words.Distinct().Count() == words.Length ? 1 : 0;
        }
    }

    public override object PerformPartTwo() 
    {
        return CountValidLines().Sum();

        IEnumerable<int> CountValidLines()
        {
            foreach (var words in _input)
            {
                var ordendWords = words.Select(word => string.Concat(word.OrderBy(c => c)));
                
                yield return ordendWords.Distinct().Count() == ordendWords.Count() ? 1 : 0;
            }
        }
    }
}