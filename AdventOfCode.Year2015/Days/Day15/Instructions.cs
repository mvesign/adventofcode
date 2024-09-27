using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015.Days.Day15;

public partial class Instructions() : Abstractions.Instructions(year: 2015, day: 15)
{
    private Ingredient[] _input = [];

    private const int maxIngredients = 100;
    private const int targetCalories = 500;

    public override void LoadInput() =>
        _input = ReadAllLines()
            .Select(_ => GeneratedRegex().Match(_).Groups)
            .Select(_ => new Ingredient(int.Parse(_[2].Value), int.Parse(_[3].Value), int.Parse(_[4].Value), int.Parse(_[5].Value), int.Parse(_[6].Value)))
            .ToArray();

    public override object PerformPartOne() =>
        Distribute()
            .Select(_ => GetCookieIngredients(_input, _))
            .Max(_ => _.Score);

    public override object PerformPartTwo() =>
        Distribute()
            .Select(_ => GetCookieIngredients(_input, _))
            .Where(_ => _.Calories == targetCalories)
            .Max(_ => _.Score);

    private static IEnumerable<int[]> Distribute()
    {
        for (var first = 0; first <= maxIngredients; first++)
            for (var second = 0; second <= maxIngredients - first; second++)
                for (var third = 0; third <= maxIngredients - first - second; third++)
                    yield return new[] { first, second, third, maxIngredients - first - second - third };
    }

    private static Ingredient GetCookieIngredients(IEnumerable<Ingredient> ingredients, IEnumerable<int> distribution) =>
        ingredients
            .Zip(distribution, (ingredient, number) => ingredient * number)
            .Aggregate((x, y) => x + y);

    [GeneratedRegex(@"^(\w+): capacity ([\-\d]+), durability ([\-\d]+), flavor ([\-\d]+), texture ([\-\d]+), calories ([\-\d]+)")]
    private static partial Regex GeneratedRegex();
}