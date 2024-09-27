using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015.Days.Day14;

public partial class Instructions() : Abstractions.Instructions(year: 2015, day: 14)
{
    private List<Reindeer> _input = [];

    private const int seconds = 2503;

    public override object PerformPartOne() =>
        _input.Select(GetDistance).Max();

    public override object PerformPartTwo()
    {
        for (var i = 0; i < seconds; i++)
        {
            _input.ForEach(Tick);

            var topDistance = _input.Max(_ => _.Distance);

            _input.Where(_ => _.Distance == topDistance).ToList().ForEach(_ => _.Score++);
        }

        return _input.Max(_ => _.Score);
    }

    public override void LoadInput() =>
        _input = ReadAllLines()
            .Select(_ => GeneratedRegex().Match(_).Groups)
            .Select(_ => new Reindeer(int.Parse(_[2].Value), int.Parse(_[3].Value), int.Parse(_[4].Value)))
            .ToList();

    private static int GetDistance(Reindeer reindeer)
    {
        var elapsed = reindeer.Fly;
        var isFlying = true;    
        var distance = 0;

        for (var i = 0; i < seconds; i++)
        {
            if (elapsed == 0)
            {
                isFlying = !isFlying;
                elapsed = isFlying ? reindeer.Fly : reindeer.Rest;
            }

            if (isFlying)
                distance += reindeer.Speed;

            elapsed--;
        }

        return distance;
    }

    private static void Tick(Reindeer reindeer)
    {
        if (reindeer.Ticks == 0)
        {
            reindeer.IsFlying = !reindeer.IsFlying;
            reindeer.Ticks = reindeer.IsFlying ? reindeer.Fly : reindeer.Rest;
        }

        if (reindeer.IsFlying)
            reindeer.Distance += reindeer.Speed;

        reindeer.Ticks--;
    }

    [GeneratedRegex(@"^(\w+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds.")]
    private static partial Regex GeneratedRegex();
}