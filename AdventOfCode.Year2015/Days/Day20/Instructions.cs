using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2015.Days.Day20;

public class Instructions() : Abstractions.Instructions(year: 2015, day: 20)
{
    private const int _input = 29000000;
    private const int _numberOfHouses = 1000000;

    public override object PerformPartOne()
    {
        const int presentsPerElf = 10;

        var houses = new int[_numberOfHouses];
    
        for (var elf = 1; elf < _numberOfHouses; elf++)
            for (var houseNumber = elf; houseNumber < _numberOfHouses; houseNumber += elf)
                houses[houseNumber] += elf * presentsPerElf;

        return GetLowestHouseNumber(houses);
    }

    public override object PerformPartTwo()
    {
        const int presentsPerElf = 11;

        var houses = new int[_numberOfHouses];

        for (var elf = 1; elf < _numberOfHouses; elf++)
            for (int houseNumber = elf, visited = 0; houseNumber < _numberOfHouses && visited < 50; houseNumber += elf, visited++)
                houses[houseNumber] += elf * presentsPerElf;

        return GetLowestHouseNumber(houses);
    }

    private static int GetLowestHouseNumber(IEnumerable<int> houses) =>
        houses
            .Select((value, index) => new
            {
                Index = index,
                Value = value
            })
            .Where(_ => _.Value >= _input)
            .Min(_ => _.Index);
}