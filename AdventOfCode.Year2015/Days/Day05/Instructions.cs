using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015.Days.Day05;

public class Instructions(int year, int day) : Abstractions.Instructions(year, day)
{
    private string[] _input = [];

    private const string vowels = "aeiou";
    
    private readonly string[] disallowed = ["ab", "cd", "pq", "xy"];

    public override object PerformPartOne()
        => _input.Count(_ =>
            !disallowed.Any(_.Contains)
            && _.Count(c => vowels.Contains(c)) >= 3
            && Enumerable.Range(0, _.Length - 1).Any(c => _[c] == _[c + 1])
        );

    public override object PerformPartTwo()
        => _input.Count(_ =>
            Enumerable.Range(0, _.Length - 1).Any(c => _.IndexOf(_.Substring(c, 2), c + 2, StringComparison.OrdinalIgnoreCase) >= 0)
            && Enumerable.Range(0, _.Length - 2).Any(c => _[c] == _[c + 2])
        );

    protected override void LoadInput(string filePath)
        => _input = File.ReadAllLines(filePath);
}
