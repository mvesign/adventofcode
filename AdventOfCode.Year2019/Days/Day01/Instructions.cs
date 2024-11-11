using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2019.Days.Day01;

public class Instructions() : Abstractions.Instructions(year: 2019, day: 1)
{
    private decimal[] _requirements = [];

    public override void LoadInput() =>
        _requirements = ReadAllLines()
            .Select(decimal.Parse)
            .ToArray();

    public override object PerformPartOne() =>
        _requirements.Sum(requirement => Math.Floor(requirement / 3) - 2);

    public override object PerformPartTwo() 
    {
        return _requirements.Sum(requirement => CalculateFuel(requirement).Sum());;

        IEnumerable<decimal> CalculateFuel(decimal mass)
        {
            while (mass > 0)
            {
                mass = Math.Floor(mass / 3) - 2;
                yield return mass > 0 ? mass : 0;
            }
        }
    }
}