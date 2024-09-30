using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2017.Days.Day03;

public class Instructions() : Abstractions.Instructions(year: 2017, day: 3)
{
    private const int Input = 347991;

    private static readonly int[][] _adjacents =
    [
        [1, 1], [1, 0], [1, -1], [0, 1], [0, -1], [-1, 1], [-1, 0], [-1, -1]
    ];

    private int _direction = 0;
    private int _distance = 1;
    private int[] _location = [0, 0];

    public override void Reset()
    {
        _direction = 0;
        _distance = 1;
        _location = [0, 0];
    }

    public override object PerformPartOne() 
    {
        return GetPosition().Sum(number => Math.Abs(0 - number));

        int[] GetPosition()
        {
            var position = 1;

            while (true)
            {   
                for (int i = 0; i < 2; ++i)
                {
                    for (int j = 0; j < _distance; j++)
                    {
                        if (position == Input)
                            return _location;

                        position++;

                        Move();
                    }

                    _direction++;
                    _direction %= 4;
                }

                _distance++;
            }
        }
    }

    public override object PerformPartTwo() 
    {
        var positions = new Dictionary<(int, int), int>
        {
            { LocationToKey(), 1 }
        };

        while (true)
        {
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < _distance; ++j)
                {
                    if (positions[LocationToKey()] >= Input)
                        return positions[LocationToKey()];

                    Move();

                    positions.Add(
                        LocationToKey(),
                        _adjacents
                            .Select(a => GetValue(_location[0] + a[0], _location[1] + a[1]))
                            .Sum());
                }

                _direction++;
                _direction %= 4;
            }

            _distance++;
        }

        int GetValue(int keyX, int keyY) =>
            positions.TryGetValue((keyX, keyY), out int value) ? value : 0;

        (int, int) LocationToKey() =>
            (_location[0], _location[1]);
    }

    private void Move() =>
        _ = _direction switch
        {
            0 => _location[0]++,   // Left
            1 => _location[1]++,   // Up
            2 => _location[0]--,   // Right
            _ => _location[1]--    // Down
        };
}