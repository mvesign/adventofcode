using CommandLine;

namespace AdventOfCode;

public class Options
{
    [Option('y', "year", Required = true, HelpText = "Year of the AdventOfCode project.")]
    public int? Year { get; set; }

    [Option('d', "day", Required = true, HelpText = "Day of the AdventOfCode project.")]
    public int? Day { get; set; }
}