using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2019.Days.Day02;

public class Instructions() : Abstractions.Instructions(year: 2019, day: 2)
{
    private int[] _states = [];

    public override void LoadInput() =>
        _states = ReadAllText()
            .Split(',')
            .Select(int.Parse)
            .ToArray();

    public override object PerformPartOne()
    {
        _states[1] = 12;
        _states[2] = 2;

        ProcessIntcode(_states);

        return _states[0];

        static void ProcessIntcode(int[] codes)
        {
            for(var position = 0; position + 4 < codes.Length; position += 4)
            {
                var value = codes[position];
                var noun = codes[position + 1];
                var verb = codes[position + 2];
                var result = codes[position + 3];

                if (value == 99)
                    return;

                codes[result] = value == 1
                    ? codes[noun] + codes[verb]
                    : value == 2
                        ? codes[noun] * codes[verb]
                        : codes[result];
            }
        }
    }

    public override object PerformPartTwo() 
    {
        return 0;
    }
}