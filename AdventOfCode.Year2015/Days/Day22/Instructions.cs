using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2015.Days.Day22;

public class Instructions() : Abstractions.Instructions(year: 2015, day: 22)
{
    private const int BossDamage = 9;

    private static PriorityQueue<GameState, int> _queue = new();

    public override void LoadInput()
    {
        _queue = new PriorityQueue<GameState, int>();
        _queue.Enqueue(
            new GameState(500, 50, 51, 0, 0, 0, 0, true),
            0
        );
    }

    public override object PerformPartOne() =>
        Play(false);

    public override object PerformPartTwo() =>
        Play(true);

    private static int Play(bool hardMode)
    {
        while (_queue.TryDequeue(out var state, out var manaSpent))
        {
            if (hardMode && state.PlayerTurn)
            {
                state.PlayerHitpoints--;
                if (state.PlayerHitpoints <= 0)
                    continue;
            }

            state = ApplyEffects(state);

            if (state.BossHitpoints <= 0)
                return manaSpent;

            if (state.PlayerTurn)
            {
                CastSpells(state)
                    .Where(nextState => nextState != null)
                    .ToList()
                    .ForEach(nextState => _queue.Enqueue(nextState!, nextState!.ManaSpent));
                continue;
            }

            var damage = DetermineBossDamage(state);

            state.PlayerHitpoints -= damage;
            if (state.PlayerHitpoints > 0)
            {
                state.PlayerTurn = true;
                _queue.Enqueue(state, state.ManaSpent);
            }
        }

        return 0;
    }

    private static GameState ApplyEffects(GameState state)
    {
        if (state.Shield > 0)
            state.Shield--;

        if (state.Poison > 0)
        {
            state.BossHitpoints = Math.Max(0, state.BossHitpoints - 3);
            state.Poison--;
        }

        if (state.Recharge > 0)
        {
            state.Mana += 101;
            state.Recharge--;
        }

        return state;
    }

    private static IEnumerable<GameState?> CastSpells(GameState state)
    {
        yield return TryCast(53, s => s.BossHitpoints -= 4);

        yield return TryCast(73, s =>
        {
            s.BossHitpoints -= 2;
            s.PlayerHitpoints += 2;
        });

        if (state.Shield == 0)
            yield return TryCast(113, s => s.Shield = 6);

        if (state.Poison == 0)
            yield return TryCast(173, s => s.Poison = 6);

        if (state.Recharge == 0)
            yield return TryCast(229, s => s.Recharge = 5);

        GameState? TryCast(int cost, Action<GameState> apply)
        {
            if (state.Mana < cost)
                return null;

            var newState = state.Clone();
            newState.Mana -= cost;
            newState.ManaSpent += cost;
            newState.PlayerTurn = false;

            apply(newState);

            return newState;
        }
    }

    private static int DetermineBossDamage(GameState state) =>
        Math.Max(
            BossDamage - (state.Shield > 0 ? 7 : 0),
            1);
}