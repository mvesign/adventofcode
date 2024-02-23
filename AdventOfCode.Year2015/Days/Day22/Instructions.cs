using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015.Days.Day22;

public class Instructions(int year, int day) : Abstractions.Instructions(year, day)
{
    private string[] _input = [];

    public override object PerformPartOne()
    {
        throw new NotImplementedException();
    }

    public override object PerformPartTwo()
    {
        throw new NotImplementedException();
    }

    protected override void LoadInput(string filePath)
    {
        _input = File.ReadAllLines(filePath);
    }
}
