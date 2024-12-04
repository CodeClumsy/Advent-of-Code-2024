using AOC_2024.DataModels;
using AOC_2024.Models.Interfaces;

namespace AOC_2024.Solutions
{
    public class Day1Solver: IPuzzleSolver
    {
        public string SolvePuzzleOne(Puzzle input)
        {
            // Get input for puzzle one, split by newline, remove blank lines
            List<string> lines = input.SplitNewline(1, true);

            // Convert rows into columns as a tuple of List<int> for each column
            var columns = GetColumns(lines);

            // Zip results with linq, perform comparison, subtract smaller from larger, return diff
            List<int> results = columns.columnOne.Zip(columns.columnTwo, (first, second) =>
            {
                int smaller = Math.Min(first, second);
                int larger = Math.Max(first, second);

                return larger-smaller;
            }).ToList();

            return $"{results.Sum()}";
        }

        public string SolvePuzzleTwo(Puzzle input)
        {
            // Get input for puzzle one, split by newline, remove blank lines
            List<string> lines = input.SplitNewline(2, true);

            // Convert rows into columns as a tuple of List<int> for each column
            var columns = GetColumns(lines);

            int similarityScore = 0;
            foreach (int rowOneNum in columns.columnOne)
            {
                similarityScore += (rowOneNum * columns.columnTwo.Count(rowTwoNum => rowTwoNum == rowOneNum));
            }

            return $"{similarityScore}";
        }

        // Split rows into columns, parse all column values as ints, order descending
        private (List<int> columnOne, List<int> columnTwo) GetColumns(List<string> lines)
        {
            List<int> columnOne = lines
                .Select(line => line.Split("   "))
                .Select(num => int.Parse(num.First()))
                .OrderByDescending(num => num)
                .ToList();
            List<int> columnTwo = lines
                .Select(line => line.Split("   "))
                .Select(num => int.Parse(num.Last()))
                .OrderByDescending(num => num)
                .ToList();

            return (columnOne, columnTwo);
        }
    }
}
