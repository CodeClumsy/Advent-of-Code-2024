using AOC_2024.DataModels;
using AOC_2024.Models.Interfaces;

namespace AOC_2024.Solutions
{
    public class Day2Solver: IPuzzleSolver
    {
        public string SolvePuzzleOne(Puzzle input)
        {
            // Parse the input into a list of lists of integers
            // Each line is split into integers based on a single space delimiter
            List<List<int>> lines = input.SplitLinesToIntLists(1, " ");

            // Convert each line of integers into a Report object
            // Reports are used to determine if the reactor levels are safe
            List<Report> reports = lines.Select(line => new Report(line)).ToList();

            // Count the number of reports that are considered safe and return as a string
            return $"{reports.Count(r => r.IsSafe())}";
        }

        public string SolvePuzzleTwo(Puzzle input)
        {
            // Parse the input into a list of lists of integers
            // Each line is split into integers based on a single space delimiter
            List<List<int>> lines = input.SplitLinesToIntLists(2, " ");

            // Convert each line of integers into a Report object
            // The dampener is enabled for these reports, allowing a single bad reactor level
            List<Report> reports = lines.Select(line => new Report(line, true)).ToList();

            // Count the number of reports that are considered safe (with dampener applied) and return as a string
            return $"{reports.Count(r => r.IsSafe())}";
        }

        private class Report
        {
            List<int> ReactorLevels { get; set; } = new List<int>();
            public bool Dampener { get; set; } = false;

            public Report(List<int> reactorLevels, bool dampener = false)
            {
                ReactorLevels = reactorLevels;
                Dampener = dampener;
            }

            public bool IsSafe()
            {
                // If dampener is false, check strict conditions
                if (!Dampener)
                {
                    return IsStrictlySortedAndSafe(ReactorLevels);
                }

                // If dampener is true, allow removing one level
                for (int i = 0; i < ReactorLevels.Count; i++)
                {
                    // Create a new list without the current level
                    var reducedList = ReactorLevels.Where((_, index) => index != i).ToList();

                    // Check if the reduced list is strictly sorted and safe
                    if (IsStrictlySortedAndSafe(reducedList))
                    {
                        return true;
                    }
                }

                return false; // Not safe if no valid list found
            }

            private bool IsStrictlySortedAndSafe(List<int> levels)
            {
                // Check if the list is sorted in ascending or descending order
                var ascending = levels.OrderBy(x => x).ToList();
                var descending = levels.OrderByDescending(x => x).ToList();

                if (levels.SequenceEqual(ascending) || levels.SequenceEqual(descending))
                {
                    // Check the difference between consecutive elements
                    for (int i = 0; i < levels.Count - 1; i++)
                    {
                        var diff = Math.Abs(levels[i] - levels[i + 1]);
                        if (diff == 0 || diff > 3)
                        {
                            return false; // Not safe if the difference exceeds 3 or is less than 1
                        }
                    }
                    return true; // Safe if sorted and differences are within range
                }

                return false; // Not sorted
            }
        }
    }
}
