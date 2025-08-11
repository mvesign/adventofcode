using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2015.Days.Day23;

internal class Instructions() : Abstractions.Instructions(2015, 23)
{
    private string[][] _instructions = [];
    private Dictionary<string, int> _registers = new()
    {
        { "a", 0 },
        { "b", 0 }
    };

    public override void LoadInput() =>
        _instructions = ReadAllLines()
            .Select(line => line.Split(' ').Select(value => value.Trim(',')).ToArray())
            .ToArray();

    public override void Reset() =>
        _registers = new Dictionary<string, int>
        {
            { "a", 1 },
            { "b", 0 }
        };

    public override object PerformPartOne() =>
        Process();

    public override object PerformPartTwo() =>
        Process();

    private int Process()
    {
        var index = 0;
        while (index < _instructions.Length)
        {
            var instruction = _instructions[index];
            switch (instruction[0])
            {
                case "hlf":
                    _registers[instruction[1]] /= 2;
                    index++;
                    break;
                case "tpl":
                    _registers[instruction[1]] *= 3;
                    index++;
                    break;
                case "inc":
                    _registers[instruction[1]]++;
                    index++;
                    break;
                case "jmp":
                    index += int.Parse(instruction[1]);
                    break;
                case "jie":
                    index += (_registers[instruction[1]] % 2 == 0)
                        ? int.Parse(instruction[2])
                        : 1;
                    break;
                case "jio":
                    index += _registers[instruction[1]] == 1
                        ? int.Parse(instruction[2])
                        : 1;
                    break;
            }
        }

        return _registers["b"];
    }
}
