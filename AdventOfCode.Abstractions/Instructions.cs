using System.IO;

namespace AdventOfCode.Abstractions;

public abstract class Instructions(int year, int day)
{
    protected int Year = year;
    protected int Day = day;

    public void LoadInput()
        => LoadInput(
            Path.Combine(
                Directory.GetCurrentDirectory(), $"AdventOfCode.Year{Year}", "Days", $"Day{Day:00}", "input.txt"
            )
        );

    public abstract object PerformPartOne();

    public abstract object PerformPartTwo();
    
    protected abstract void LoadInput(string filePath);
}
