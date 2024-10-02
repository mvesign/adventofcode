using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2017.Days.Day02;

public class Instructions() : Abstractions.Instructions(year: 2017, day: 2)
{
    private int[][] _input = [];

    public override void LoadInput() =>
        _input = ReadAllLines()
            .Select(line =>
                line
                    .Split('\t')
                    .Select(number => int.Parse(number))
                    .ToArray())
            .ToArray();

    public override object PerformPartOne() =>
        _input.Sum(numbers => numbers.Max() - numbers.Min());

    public override object PerformPartTwo() 
    {
        return GetChecksumNumbers().Sum();

        IEnumerable<int> GetChecksumNumbers()
        {
            foreach(var numbers in _input)
                for (var i = 0; i < numbers.Length; ++i)
                    for (var j = 0; j < numbers.Length; ++j)
                        if (i != j && numbers[j] % numbers[i] == 0)
                        {
                            yield return numbers[j] / numbers[i];
                            break;
                        }
        }
    }
}