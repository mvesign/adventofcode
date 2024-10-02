using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2017.Days.Day07;

public partial class Instructions() : Abstractions.Instructions(year: 2017, day: 7)
{
    private Program[] _programs = [];

    public override void LoadInput()
    {
        _programs = ReadAllLines()
            .Select(program =>
            {
                var match = GeneratedRegex().Match(program);

                return new Program(
                    match.Groups["name"].Value,
                    int.Parse(match.Groups["weigth"].Value),
                    match.Groups["programs"].Value.Replace(" ", "").Split(',').Where(value => !string.IsNullOrWhiteSpace(value)).ToArray()
                );
            })
            .ToArray();
    }

    public override object PerformPartOne()
    {
        var childPrograms = _programs
            .Where(program => program.Programs.Length != 0)
            .SelectMany(program => program.Programs);

        var bottomProgram = _programs.Select(p => p.Name).Except(childPrograms).FirstOrDefault();
        return bottomProgram!;
    }

    public override object PerformPartTwo() 
    {
        //TODO
        return string.Empty;
    }

    [GeneratedRegex(@"^(?<name>\w+)\s[(](?<weigth>\d+)[)](\s->\s(?<programs>[\w,\s]+))*$")]
    private static partial Regex GeneratedRegex();
}