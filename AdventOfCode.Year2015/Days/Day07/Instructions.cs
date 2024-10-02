using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2015.Days.Day07;

public class Instructions() : Abstractions.Instructions(year: 2015, day: 7)
{
    private string _filePath = string.Empty;

    public override void LoadInput() =>
        // Not actual loading the input here, but storing the filepath for later reference.
        _filePath = GetInputFilePath();

    public override object PerformPartOne() =>
        GetSignal(GetInstructions(), "a");

    public override object PerformPartTwo()
    {
        var signalA = GetSignal(GetInstructions(), "a");

        var instructions = GetInstructions();
        instructions["b"] = [signalA.ToString(), "->", "b"];

        return GetSignal(instructions, "a");
    }

    private Dictionary<string, string[]> GetInstructions() =>
        File.ReadAllLines(_filePath)
            .Select(x => x.Split(' '))
            .ToDictionary(x => x.Last());

    private static int GetSignal(Dictionary<string, string[]> instructions, string input)
    {
        int Evaluate(string x) =>
            char.IsLetter(x[0]) ? GetSignal(instructions, x) : int.Parse(x);

        var instruction = instructions[input];
        var signal = instruction[1] switch
        {
            "->" => Evaluate(instruction[0]),
            "AND" => Evaluate(instruction[0]) & Evaluate(instruction[2]),
            "OR" => Evaluate(instruction[0]) | Evaluate(instruction[2]),
            "LSHIFT" => Evaluate(instruction[0]) << Evaluate(instruction[2]),
            "RSHIFT" => Evaluate(instruction[0]) >> Evaluate(instruction[2]),
            _ => ~Evaluate(instruction[1]) // Otherwise it is the NOT operator.
        };

        instructions[input] = [signal.ToString(), "->", input];

        return signal;
    }
}