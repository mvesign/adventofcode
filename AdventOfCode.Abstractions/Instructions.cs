using System.IO;

namespace AdventOfCode.Abstractions;

public abstract class Instructions(int year, int day)
{
    protected int Year = year;
    protected int Day = day;

    public virtual void LoadInput()
    {
        // By default we do nothing.
    }

    public virtual void Reset()
    {
        // By default we do nothing.
    }

    public abstract object PerformPartOne();

    public abstract object PerformPartTwo();
    
    protected string ReadAllText()
    {
        var inputFilePath = GetInputFilePath();
        return File.Exists(inputFilePath)
            ? File.ReadAllText(inputFilePath)
            : string.Empty;
    }

    protected string[] ReadAllLines()
    {
        var inputFilePath = GetInputFilePath();
        return File.Exists(inputFilePath)
            ? File.ReadAllLines(inputFilePath)
            : [];
    }

    protected string GetInputFilePath() =>
        Path.Combine(
            Directory.GetCurrentDirectory(), "..\\", $"AdventOfCode.Year{Year}", "Days", $"Day{Day:00}", "input.txt"
        );
}