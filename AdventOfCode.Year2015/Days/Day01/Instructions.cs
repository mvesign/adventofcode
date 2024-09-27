using System.Linq;

namespace AdventOfCode.Year2015.Days.Day01;

public class Instructions() : Abstractions.Instructions(year: 2015, day: 1)
{
    private string _input = string.Empty;

    public override void LoadInput() =>
        _input = ReadAllText();

    public override object PerformPartOne() =>
        _input.Sum(_ => _ == '(' ? 1 : -1);

    public override object PerformPartTwo()
    {
        var floor = 0;
        var position = 0;
        foreach (var parentheses in _input)
        {
            floor += parentheses == '(' ? 1 : -1;
            position++;
            if (floor == -1)
                break;
        }

        return position;
    }
}