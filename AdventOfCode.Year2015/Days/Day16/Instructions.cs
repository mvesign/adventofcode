using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015.Days.Day16;

public partial class Instructions(int year, int day) : Abstractions.Instructions(year, day)
{
    private InputLine[] _input = [];
    private readonly Section[] _sections = [
        new ("children", 3),
        new ("cats", 7, true),
        new ("samoyeds", 2),
        new ("pomeranians", 3, Fewer: true),
        new ("akitas", 0),
        new ("vizslas", 0),
        new ("goldfish", 5, Fewer: true),
        new ("trees", 3, true),
        new ("cars", 2),
        new ("perfumes", 1)
    ];

    public override object PerformPartOne()
        => _input
            .Single(_ => _.Sections.All(kvp => _sections.First(s => s.Name == kvp.Key).Value == kvp.Value))
            .Number;

    public override object PerformPartTwo()
        => _input
            .Single(_ => _.Sections.All(kvp =>
            {
                var section = _sections.First(s => s.Name == kvp.Key);
                return !section.Greater && !section.Fewer && section.Value == kvp.Value
                    || section.Greater && section.Value < kvp.Value
                    || section.Fewer && section.Value > kvp.Value;
            }))
            .Number;

    protected override void LoadInput(string filePath)
    {
        _input = File.ReadAllLines(filePath)
            .Select(_ => FirstGeneratedRegex().Match(_).Groups)
            .Select(_ => new {Number = int.Parse(_[1].Value), Input = _[2].Value.Replace(",", string.Empty)})
            .Select(_ => new {_.Number, Groups = SecondGeneratedRegex().Matches(_.Input).Select(match => match.Groups)})
            .Select(_ => new InputLine(
                _.Number,
                _.Groups
                    .Select(group => new {Section = group[2].Captures[0].Value, Value = int.Parse(group[3].Captures[0].Value)})
                    .ToDictionary(key => key.Section, value => value.Value)
            ))
            .ToArray();
    }

    [GeneratedRegex(@"^Sue (\d+): ([\w:, ]+)")]
    private static partial Regex FirstGeneratedRegex();

    [GeneratedRegex(@"((\w+): (\d+))+")]
    private static partial Regex SecondGeneratedRegex();
}
