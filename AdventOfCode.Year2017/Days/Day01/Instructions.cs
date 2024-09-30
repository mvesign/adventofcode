using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2017.Days.Day01;

public class Instructions() : Abstractions.Instructions(year: 2017, day: 1)
{
    private int[] _input = [];

    public override void LoadInput()
    {
        _input = ReadAllText()
            .Select(digit => int.Parse($"{digit}"))
            .ToArray();
    }

    public override object PerformPartOne() =>
        GetCaptchaValues((index) =>
        {
            return index + 1 < _input.Length ? (index + 1) : 0;
        })
        .Sum();

    public override object PerformPartTwo() =>
        GetCaptchaValues((index) =>
        {
            var step = _input.Length / 2;
            var nextStep = index + step;
            return nextStep < _input.Length ? nextStep : (nextStep - _input.Length);
        })
        .Sum();

    private IEnumerable<int> GetCaptchaValues(Func<int, int> next)
    {
        for (int index = 0; index < _input.Length; index++)
            yield return _input[index] == _input[next(index)] ? _input[index] : 0;
    }
}