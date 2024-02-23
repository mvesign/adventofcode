using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015.Days.Day12;

#pragma warning disable CS8600
#pragma warning disable CS8604
#pragma warning disable CS8622

public partial class Instructions(int year, int day) : Abstractions.Instructions(year, day)
{
    private string _input = string.Empty;

    public override object PerformPartOne()
        => GeneratedRegex()
            .Matches(_input)
            .Select(_ => int.Parse(_.Value))
            .Sum();

    public override object PerformPartTwo()
        => SumNumbers(
            JsonSerializer.Deserialize<JsonNode>(_input)
        );

    protected override void LoadInput(string filePath)
        => _input = File.ReadAllText(filePath);

    private long SumNumbers(JsonNode jsonNode) =>
        jsonNode switch
        {
            JsonObject jsonObject => !ContainsRed(jsonObject) ? jsonObject.Where(_ => _.Value != null).Sum(_ => SumNumbers(_.Value)) : 0,
            JsonArray jsonArray => jsonArray.Sum(SumNumbers),
            JsonValue jsonValue => jsonValue.TryGetValue(out int value) ? value : 0,
            _ => 0
        };

    private static bool ContainsRed(JsonObject jsonObject)
        => jsonObject
            .Select(_ => _.Value)
            .OfType<JsonValue>()
            .Any(_ => _.TryGetValue(out string value) && string.Equals(value, "red"));

    [GeneratedRegex(@"[\-0-9]+")]
    private static partial Regex GeneratedRegex();
}

#pragma warning restore CS8600
#pragma warning restore CS8604
#pragma warning restore CS8622