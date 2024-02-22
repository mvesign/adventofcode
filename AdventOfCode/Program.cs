using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;
using CommandLine;

namespace AdventOfCode;

internal static class Program
{
    private static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(PerformRun)
            .WithNotParsed(HandleErrors);
    }

    static void PerformRun(Options options)
    {
        if (!options.Year.HasValue)
        {
            Console.WriteLine("ERROR: Argument 'year' not given");
            return;
        }

        if (!options.Day.HasValue)
        {
            Console.WriteLine("ERROR: Argument 'day' not given");
            return;
        }

        var instructions = GetInstructions(options.Year.Value, options.Day.Value);
        if (instructions == null)
            return;

        instructions.LoadInput();

        Console.WriteLine(
            instructions.PerformPartOne()
        );
        Console.WriteLine(
            instructions.PerformPartTwo()
        );
    }

    static void HandleErrors(IEnumerable<Error> errors)
        => errors.ToList().ForEach(error => Console.WriteLine($"ERROR: {error}"));
    
    static Instructions? GetInstructions(int year, int day)
    {
        var type = Type.GetType($"AdventOfCode.Year{year}.Days.Day{day:00}.Instructions, AdventOfCode.Year{year}");
        if (type == null)
        {
            Console.WriteLine($"ERROR: No instructions found for year '{year}' and day '{day:00}'");
            return null;
        }

        var instructions = (Instructions?)Activator.CreateInstance(type, new object[] { year, day });
        if (instructions == null)
        {
            Console.WriteLine($"ERROR: Could not create instructions for year '{year}' and day '{day:00}'");
            return null;
        }

        return instructions;
    }
}