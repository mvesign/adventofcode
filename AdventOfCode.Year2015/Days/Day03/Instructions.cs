﻿using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2015.Days.Day03;

public class Instructions(int year, int day) : Abstractions.Instructions(year, day)
{
    private readonly List<(int, int)> _houses = [];
    private string _input = string.Empty;

    public override object PerformPartOne()
    {
        var current = (0, 0);
    
        _houses.Clear();
        
        foreach (var direction in _input)
            current = ProcessDirection(current, direction);

        return _houses.Count;
    }

    public override object PerformPartTwo()
    {
        var santa = (0, 0);
        var roboSanta = (0, 0);
        var santasTurn = false;

        _houses.Clear();

        foreach (var direction in _input)
            if (santasTurn ^= true)
                santa = ProcessDirection(santa, direction);
            else
                roboSanta = ProcessDirection(roboSanta, direction);

        return _houses.Count;
    }

    protected override void LoadInput(string filePath)
        => _input = File.ReadAllText(filePath);

    private (int x, int y) ProcessDirection((int x, int y) current, char direction)
    {
        if (!_houses.Contains(current))
            _houses.Add(current);

        return NextHouse(current.x, current.y, direction);
    }

    private static (int x, int y) NextHouse(int x, int y, char direction)
        => direction switch
        {
            '^' => (x, y + 1),
            '>' => (x + 1, y),
            'v' => (x, y - 1),
            '<' => (x - 1, y),
            _ => (x, y)
        };
}
