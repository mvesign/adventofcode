using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2015.Days.Day21;

public class Instructions() : Abstractions.Instructions(year: 2015, day: 21)
{
    private readonly Equipment[] weapons =
    [
        new(8, 4, 0),
        new(10, 5, 0),
        new(25, 6, 0),
        new(40, 7, 0),
        new(74, 8, 0)
    ];
    private readonly Equipment[] armors =
    [
        new(13, 0, 1),
        new(31, 0, 2),
        new(53, 0, 3),
        new(75, 0, 4),
        new(102, 0, 5),
        new(0, 0, 0)
    ];
    private readonly Equipment[] rings =
    [
        new(25, 1, 0),
        new(50, 2, 0),
        new(100, 3, 0),
        new(20, 0, 1),
        new(40, 0, 2),
        new(80, 0, 3),
        new(0, 0, 0)
    ];

    public override object PerformPartOne() =>
        PermutePlayers()
            .Where(_ => Fight(_, GetBoss()))
            .Min(_ => _.Cost);

    public override object PerformPartTwo() =>
        PermutePlayers()
            .Where(_ => !Fight(_, GetBoss()))
            .Max(_ => _.Cost);

    private static Person GetBoss() =>
        new()
        {
            HitPoints = 104,
            Damage = 8,
            Armor = 1
        };

    private IEnumerable<Person> PermutePlayers()
    {
        foreach (var weapon in weapons)
            foreach (var armor in armors)
                foreach (var ringCombination in PermuteRings().ToArray())
                    yield return new Person
                    {
                        HitPoints = 100,
                        Damage = weapon.Damage + armor.Damage + ringCombination.Sum(_ => _.Damage),
                        Armor = weapon.Armor + armor.Armor + ringCombination.Sum(_ => _.Armor),
                        Cost = weapon.Cost + armor.Cost + ringCombination.Sum(_ => _.Cost)
                    };
    }

    IEnumerable<Equipment[]> PermuteRings()
    {
        foreach (var ring1 in rings)
            foreach (var ring2 in rings)
                if (ring1 != ring2)
                    yield return new [] {ring1, ring2};

        foreach (var ring in rings)
            yield return new[] {ring};
    }

    private bool Fight(Person attacker, Person defender)
    {
        var hitPoints = attacker.Damage - defender.Armor;

        defender.HitPoints -= hitPoints > 0 ? hitPoints : 1;
        if (defender.HitPoints <= 0)
            return true;
        
        return !Fight(defender, attacker);
    }
}