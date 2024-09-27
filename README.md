# Advent Of Code

The annual advent calendar of small puzzles, which can be solved using your computer, mind and some code-fu. The fun part is that there are almost no rules. Programming language, method of work, code design choices, it all doesn't matter. Only the focus on getting the correct answer.

Each day has two parts, which should have the correct answer given before you can procceed to the next day. For each day (or even each part) there is

- An input to be used. 

- Instructions that describe the problem and the desired solution.

- A descriptive example to help explain the problem a bit better.

## Goal of this repository

The difference between science and just fiddling around is writing your process down. Not that I'm doing science overe here but why not writing it down, so my personal progress is not lost.

Also am I not having that much of spare time on a daily basis, so it's nice to have a bit of history when I do have the time.

## Check daily challenges

Within the repository we have a C#.NET solution which we need to build.

```bash
dotnet build .\AdventOfCode.sln
```

After it is build, we have the main project `.\AdventOfCode\AdventOfCode.csproj`. This project can be triggered with two commandline options; `year` for the year the advent calendar is started and `day` for the day within that year.

```bash
cd .\AdventOfCode
dotnet run AdventOfCode.csproj --year 2015 --day 1
```

Both options are required to be numeric or the specific day is not performed. If the input is valid, the requested daily challenge is performed and the solution is printed out in the same console.

```bash
dotnet run AdventOfCode.csproj --year 2015 --day 1
280
1797
```

If both parts of the requested day is implemeted, the result for the first part is on the first line and for the second part is on the second line.

## Repository structure

As stated in the previous section, the repository contains a C#.NET solution with the main project to be found at `.\AdventOfCode\AdventOfCode.csproj`.

The structure of this solution is a project per annualy advent calendar. With each annualy project containing a `Days` directory where the logic for each daily challenge is stored in their own directory.

And then we have a separate project containing the shared abstraction logic.

```bash
- AdventOfCode.sln
- AdventOfCode
  - AdventOfCode.csproj
  - Program.cs
- AdventOfCode.Abstractions
  - AdventOfCode.Abstractions.csproj
  - Instructions.cs
- AdventOfCode.Year2015
  - AdventOfCode.Year2015.csproj
  - Days
    - Day01
      - Instructions.cs
```

### Instructions setup

The instruction per daily challenge is the same. You can load the input and you can perform the first and second part of the daily challenge. These abstract methods are there because the input can be different per daily challenge. And the same goes for the first and second part.