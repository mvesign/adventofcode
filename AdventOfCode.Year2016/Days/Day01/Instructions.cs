using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2016.Days.Day01;

public partial class Instructions() : Abstractions.Instructions(year: 2016, day: 1)
{
    private const string StartDirection = "N";
    
    private static Dictionary<string, string[]> _directions = new()
    {
        {"N", ["W", "E"]},
        {"E", ["N", "S"]},
        {"S", ["E", "W"]},
        {"W", ["S", "N"]},
    };

    private (string Action, int Steps)[] _input = [];
    private int[] _progress = [0, 0];
    private string _direction = StartDirection;
    
    public override void LoadInput()
    {
        _input = ReadAllText()
            .Split(", ")
            .Select(_ => GeneratedRegex().Match(_).Groups)
            .Select(_ => (_[1].Value, int.Parse(_[2].Value)))
            .ToArray();
    }

    public override object PerformPartOne()
    {
        foreach (var (Action, Steps) in _input)
        {
            UpdateDirection(Action);
            _ = _direction switch
            {
                "N" => _progress[0] += Steps,
                "E" => _progress[1] += Steps,
                "S" => _progress[0] -= Steps,
                _ => _progress[1] -= Steps
            };
        }

        return _progress.Sum(number => number < 0 ? -number : number);
    }

    public override void Reset()
    {
        _progress = [0, 0];
        _direction = StartDirection;
    }

    public override object PerformPartTwo() 
    {
        throw new NotImplementedException();
    }

    private void UpdateDirection(string action) =>
        _direction = _directions[_direction][action == "L" ? 0: 1];
    
    [GeneratedRegex(@"^(L|R)(\d+)$")]
    private static partial Regex GeneratedRegex();
}