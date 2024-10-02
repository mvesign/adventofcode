using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015.Days.Day02;

public class Instructions() : Abstractions.Instructions(year: 2015, day: 2)
{
    private string[] _input = [];

    public override void LoadInput() =>
        _input = ReadAllLines();

    public override object PerformPartOne() =>
         _input
            .Select(_ => _.Split("x").Select(int.Parse).ToArray())
            .Select(_ => new[] { _[0] * _[1], _[1] * _[2], _[0] * _[2] })
            .Select(_ => 2 * _.Sum() + _.Min())
            .Sum();

    public override object PerformPartTwo() =>
        _input.Select(_ => _.Split('x'))
            .Select(_ => _.Select(int.Parse))
            .Select(_ => _.OrderBy(x => x).ToArray())
            .Select(_ => 2 * _[0] + 2 * _[1] + _[0] * _[1] * _[2])
            .Sum();
}