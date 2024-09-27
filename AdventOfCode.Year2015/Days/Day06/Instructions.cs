using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015.Days.Day06;

public class Instructions() : Abstractions.Instructions(year: 2015, day: 6)
{
    private Instruction[] _input = [];

    private const string pattern = @"^(.+) (\d+),(\d+) through (\d+),(\d+)$";

    public override void LoadInput() =>
        _input = ReadAllLines()
            .Select(_ => Regex.Match(_, pattern).Groups)
            .Select(_ => new Instruction(ParseType(_[1].Value), int.Parse(_[2].Value), int.Parse(_[3].Value), int.Parse(_[4].Value), int.Parse(_[5].Value)))
            .ToArray();

    public override object PerformPartOne()
    {
        var grid = new bool[1000 * 1000];

        foreach (var instruction in _input)
            for (var row = instruction.FromX; row <= instruction.ToX; row++)
            for (var col = instruction.FromY; col <= instruction.ToY; col++)
                grid[1000 * row + col] = instruction.Type != 0 && (instruction.Type == 1 || !grid[1000 * row + col]);

        return grid.Count(_ => _);
    }

    public override object PerformPartTwo()
    {
        var grid = new int[1000 * 1000];

        foreach (var instruction in _input)
            for (var row = instruction.FromX; row <= instruction.ToX; row++)
            for (var col = instruction.FromY; col <= instruction.ToY; col++)
                if (instruction.Type == 0)
                    grid[1000 * row + col] -= grid[1000 * row + col] > 0 ? 1 : 0;
                else
                    grid[1000 * row + col] += instruction.Type == 1 ? 1 : 2;

        return grid.Sum();
    }

    private static int ParseType(string type) =>
        type switch
        {
            "turn off" => 0,
            "turn on" => 1,
            "toggle" => 2,
            _ => throw new Exception("ERROR!") // This will never happen, but we need a default.
        };
}