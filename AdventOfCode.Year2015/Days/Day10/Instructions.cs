using System.Linq;
using System.Text;

namespace AdventOfCode.Year2015.Days.Day10;

public class Instructions() : Abstractions.Instructions(year: 2015, day: 10)
{
    private string _input = string.Empty;

    public  override void LoadInput() =>
        _input = ReadAllText();

    public override object PerformPartOne() =>
        Perform(40).Length;

    public override object PerformPartTwo() =>
        Perform(50).Length;

    private string Perform(int count)
        => Enumerable.Range(0, count).Aggregate(_input, (current, _) => LookAndSay(current));

    private static string LookAndSay(string number)
    {
        var result = new StringBuilder();
        var current = number[0];
        var count = 0;
        
        foreach(var character in number)
            if (current != character)
            {
                result.Append(count).Append(current);
                current = character;
                count = 1;
            }
            else
                count++;

        return result.Append(count).Append(current).ToString();
    }
}