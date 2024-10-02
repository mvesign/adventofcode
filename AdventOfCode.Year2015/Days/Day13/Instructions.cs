using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015.Days.Day13;

public partial class Instructions() : Abstractions.Instructions(year: 2015, day: 13)
{
    private List<Person> _input = [];

    private const string name = "Maikel";

    public override void LoadInput() =>
        _input = ReadAllLines()
            .Select(_ => GeneratedRegex().Match(_).Groups)
            .Select(_ => new Person(_[1].Value, _[4].Value, int.Parse(_[3].Value) * (string.Equals(_[2].Value, "lose") ? -1 : 1)))
            .ToList();

    public override object PerformPartOne() =>
        GetMaxUnits();

    public override object PerformPartTwo()
    {
        _input.Select(_ => _.Name)
            .Distinct()
            .ToList()
            .ForEach(_ =>
            {
                _input.Add(new Person(_, name, 0));
                _input.Add(new Person(name, _, 0));
            });

        return GetMaxUnits();
    }

    private static IEnumerable<IEnumerable<string>> Permute(IEnumerable<string> sequence)
    {
        var list = sequence.ToList();
        if (list.Count == 0)
            yield return Enumerable.Empty<string>();

        var startingIndex = 0;

        foreach (var element in list)
        {
            var index = startingIndex;
            var remaining = list.Where((_, i) => i != index);

            foreach (var remainder in Permute(remaining))
                yield return remainder.Prepend(element);

            startingIndex++;
        }
    }

    private IEnumerable<int> GetUnits(List<string> names)
    {
        // Need to add the first name to the back, to make the circle complete.
        names.Add(names[0]);

        for (var i = 0; i < names.Count - 1; i++)
            yield return _input
                .Where(_ =>
                    _.Name == names[i] && _.Neighbor == names[i + 1]
                    || _.Neighbor == names[i] && _.Name == names[i + 1]
                )
                .Sum(_ => _.Units);
    }

    private int GetMaxUnits() =>
        Permute(
            _input.SelectMany(_ => new[] { _.Name, _.Neighbor }).Distinct()
        )
        .Select(_ => GetUnits(_.ToList()).Sum())
        .Max();

    [GeneratedRegex(@"^(\w+) would (lose|gain) (\d+) happiness units by sitting next to (\w+).$")]
    private static partial Regex GeneratedRegex();
}