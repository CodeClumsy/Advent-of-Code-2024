using AOC_2024.DataModels;
using AOC_2024.Models.Interfaces;

namespace AOC_2024.Solutions
{
    public class Day5Solver: IPuzzleSolver
    {
        public string SolvePuzzleOne(Puzzle input)
        {
            // Get input for puzzle one, split by newline, remove blank lines, trim lines
            List<string> lines = input.SplitNewline(1);

            List<(int X, int Y)> orderingRules = input.PuzzleOneInput
                .Split("\n\n").First().Split("\n")
                .Select(line => 
                {
                    var rule = line.Split("|").Select(num => int.Parse(num));
                    return (rule.First(), rule.Last());
                }
                ).ToList();

            foreach (var rule in orderingRules)
            {
                Console.WriteLine($"{rule.X}|{rule.Y}");
            }

            return "";
        }

        public string SolvePuzzleTwo(Puzzle input)
        {
            throw new NotImplementedException();
        }
    }
}
