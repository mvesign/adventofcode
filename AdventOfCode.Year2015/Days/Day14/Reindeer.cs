namespace AdventOfCode.Year2015.Days.Day14;

internal class Reindeer
{
    public Reindeer(int speed, int fly, int rest)
    {
        Speed = speed;
        Fly = Ticks = fly;
        Rest = rest;
    }

    public int Speed { get; set; }
    public int Fly { get; set; }
    public int Rest { get; set; }
    public int Ticks { get; set; }
    public bool IsFlying { get; set; } = true;
    public int Distance { get; set; }
    public int Score { get; set; }
}