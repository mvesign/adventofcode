using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Year2015.Days.Day04;

public class Instructions() : Abstractions.Instructions(year: 2015, day: 4)
{
    private string _input = string.Empty;

    public override void LoadInput() =>
        _input = ReadAllText();

    public override object PerformPartOne() =>
        Calculate(new string('0', 5));

    public override object PerformPartTwo() =>
        Calculate(new string('0', 6));

    private int Calculate(string startingValue)
    {
        for (var count = 0;; count++)
            if (
                BitConverter.ToString(
                    MD5.HashData(Encoding.UTF8.GetBytes($"{_input}{count}"))
                )
                .Replace("-", "")
                .StartsWith(startingValue)
            )
                return count;
    }
}