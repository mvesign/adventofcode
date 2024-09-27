using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015.Days.Day11;

public partial class Instructions() : Abstractions.Instructions(year: 2015, day: 11)
{
    private string _input = string.Empty;

    public override void LoadInput() =>
        _input = ReadAllText();

    public override object PerformPartOne()
    {
        // Store the first incremented and valid password, so we can reuse is for the second part.
        _input = Perform(_input);
        return _input;
    }

    public override object PerformPartTwo()
        => Perform(_input); // And here we just return it, because we no longer need to reuse it.
    
    private static string Perform(string input)
    {
        do
        {
            input = IncrementPassword(input);
        } while (!IsValidPassword(input));

        return input;
    }

    private static string IncrementPassword(string input)
    {
        var runnerUpCharacters = input.Remove(input.Length - 1);
        var nextCharacter = IncrementCharacter(input[^1]);

        return (nextCharacter == 'a' ? IncrementPassword(runnerUpCharacters) : runnerUpCharacters) + nextCharacter;
    }

    private static char IncrementCharacter(char input) =>
        input == 'z' ? 'a' : (char)(Convert.ToUInt16(input) + 1);

    private static bool IsValidPassword(string input) =>
        ContainStraightIncreasingSymbols(input)
        && ContainsRestrictedCharacters(input)
        && ContainsTwoNonOverlappingPairs(input);

    private static bool ContainStraightIncreasingSymbols(string input) =>
        Enumerable.Range(0, input.Length - 2)
            .Select(_ => input.Substring(_, 3))
            .Any(_ => _[0] + 1 == _[1] && _[1] + 1 == _[2]);

    private static bool ContainsRestrictedCharacters(string input) =>
        input.IndexOfAny(['i', 'o', 'l']) < 0;

    private static bool ContainsTwoNonOverlappingPairs(string input) =>
        GeneratedRegex().IsMatch(input);

    [GeneratedRegex(@"([a-z])\1.*([a-z])\2")]
    private static partial Regex GeneratedRegex();
}