namespace AdventOfCode.Year2015.Days.Day22;

public class GameState(
    int mana,
    int playerHitpoints,
    int bossHitpoints,
    int shield,
    int poison,
    int recharge,
    int manaSpent,
    bool playerTurn)
{
    public int Mana = mana;
    public int PlayerHitpoints = playerHitpoints;
    public int BossHitpoints = bossHitpoints;
    public int Shield = shield;
    public int Poison = poison;
    public int Recharge = recharge;
    public int ManaSpent = manaSpent;
    public bool PlayerTurn = playerTurn;

    public GameState Clone() =>
        new(Mana, PlayerHitpoints, BossHitpoints, Shield, Poison, Recharge, ManaSpent, PlayerTurn);
}