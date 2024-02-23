using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015.Days.Day18;

public class Instructions(int year, int day) : Abstractions.Instructions(year, day)
{
    private string[] _input = [];

    private const int depth = 100;
    private const int steps = 100;

    public override object PerformPartOne()
    {
        var lights = ParseLines(_input);

        for (var step = 0; step < steps; step++)
        {
            var copyLights = new bool[depth, depth];
            for (var i = 0; i < depth; i++)
            for (var j = 0; j < depth; j++)
            {
                var neighbors = GetActiveNeighbors(lights, i, j);
                copyLights[i, j] =
                    lights[i, j] && new[] { 2, 3 }.Contains(neighbors)
                    || !lights[i, j] && neighbors == 3;
            }
            lights = copyLights;
        }

        return CountActiveLights(lights);
    }

    public override object PerformPartTwo()
    {
        var lights = ParseLines(_input);

        for (var step = 0; step < steps; step++)
        {
            var copyLights = new bool[depth, depth];
            for (var i = 0; i < depth; i++)
            for (var j = 0; j < depth; j++)
            {
                var neighbors = GetActiveNeighbors(lights, i, j);
                copyLights[i, j] =
                    lights[i, j] && new[] { 2, 3 }.Contains(neighbors)
                    || !lights[i, j] && neighbors == 3;
            }

            copyLights[0, 0] = true;
            copyLights[depth - 1, 0] = true;
            copyLights[0, depth - 1] = true;
            copyLights[depth - 1, depth - 1] = true;

            lights = copyLights;
        }

        return CountActiveLights(lights);
    }

    protected override void LoadInput(string filePath)
        => _input = File.ReadAllLines(filePath);

    private static bool[,] ParseLines(IReadOnlyList<string> lines)
    {
        var lightsArray = new bool[depth, depth];

        for (var i = 0; i < depth; i++)
        for (var j = 0; j < depth; j++)
            lightsArray[i, j] = lines[i][j].Equals('#');
        
        return lightsArray;
    }

    private static int GetActiveNeighbors(bool[,] lights, int i, int j)
        => new[]
        {
            // One row above
            i > 0 && j > 0 && lights[i - 1, j - 1],
            i > 0 && lights[i - 1, j],
            i > 0 && j < depth - 1 && lights[i - 1, j + 1],
            // Same row
            j > 0 && lights[i, j - 1],
            j < depth - 1 && lights[i, j + 1],
            // One row below
            i < depth - 1 && j > 0 && lights[i + 1, j - 1],
            i < depth - 1 && lights[i + 1, j],
            i < depth - 1 && j < depth - 1 && lights[i + 1, j + 1]
        }
        .Count(_ => _);

    private static int CountActiveLights(bool[,] lights)
        => lights
            .Cast<bool>()
            .Count(_ => _);
}
