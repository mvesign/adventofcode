using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018.Days.Day03;

public partial class Instructions() : Abstractions.Instructions(year: 2018, day: 3)
{
    private Claim[] _claims = [];

    public override void LoadInput() =>
        _claims = ReadAllLines()
            .Select(ToClaim)
            .ToArray();

    //TODO Not correct yet
    public override object PerformPartOne() 
    {
        var positions = new Dictionary<long, int>();

        foreach (var claim in _claims)
        {
            for (var x = claim.Start; x < claim.Start + claim.Width; x++)
            {
                for (var y = claim.End; y < claim.End + claim.Height; y++)
                {
                    var key = y * 1000 + x;
                    if (!positions.ContainsKey(key))
                        positions.Add(key, 1);
                    else
                        positions[key]++;
                }
            }
        }

        return positions.Count(p => p.Value > 1);
    }

    //TODO Not correct yet
    public override object PerformPartTwo() 
    {
        var grid = new int[1000, 1000];
        var positions = new List<int>();

        foreach (var claim in _claims)
        {
            positions.Add(claim.Id);
            
            for (var i = 0; i < claim.Width; i++)
            {
                for (var j = 0; j < claim.Height; j++)
                {
                    var previousId = grid[claim.Start + i, claim.End + j];
                    if (previousId != 0)
                    {
                        positions.Remove(claim.Id);
                        positions.Remove(previousId);
                        continue;
                    }

                    grid[claim.Start + i, claim.End + j] = claim.Id;
                }
            }
        }

        return positions.First();
    }

    private static Claim ToClaim(string claim)
    {
        var values = GeneratedRegex().Match(claim).Groups
            .Cast<Group>()
            .Select(g => int.Parse(g.Value))
            .ToArray();

        return new Claim(values[0], values[1], values[2], values[3], values[4]);
    }

    [GeneratedRegex(@"^#(?<id>\d+) @ (?<start>\d+),(?<end>\d+): (?<width>\d+)x(?<height>\d+)$")]
    private static partial Regex GeneratedRegex();
}