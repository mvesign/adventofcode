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

        if (!TryGetInstructions(options.Year.Value, options.Day.Value, out var instructions))
            return;

        instructions!.LoadInput();

        Console.WriteLine(
            instructions.PerformPartOne()
        );

        instructions.Reset();

        Console.WriteLine(
            instructions.PerformPartTwo()
        );
    }

    private static void HandleErrors(IEnumerable<Error> errors)
        => errors.ToList().ForEach(error => Console.WriteLine($"ERROR: {error}"));
    
    private static bool TryGetInstructions(int year, int day, out Instructions instructions)
    {
        instructions = null;

        var type = Type.GetType($"AdventOfCode.Year{year}.Days.Day{day:00}.Instructions, AdventOfCode.Year{year}");
        if (type == null)
        {
            Console.WriteLine($"ERROR: No instructions found for year '{year}' and day '{day:00}'");
            return false;
        }

        instructions = (Instructions)Activator.CreateInstance(type);
        if (instructions == null)
        {
            Console.WriteLine($"ERROR: Could not create instructions for year '{year}' and day '{day:00}'");
            return false;
        }

        return true;
    }
}