using AOC_2024.DataModels;
using AOC_2024.Models.Interfaces;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC_2024.Solutions
{
    public class Day4Solver: IPuzzleSolver
    {
        public string SolvePuzzleOne(Puzzle input)
        {
            string word = "xmas";
            string reversedWord = new string(word.Reverse().ToArray());

            // match condition for forward or reversed word to be searched
            // ?= lookahead assertion to allow for overlapping chars in match
            string matchCondition = $"(?={word}|{reversedWord})";

            var horizontal = input.SplitNewline(1, true, true).Select(line => line.ToLower()).ToList();
            int rowCount = horizontal.Count;
            int columnCount = horizontal.First().Length;

            // extract vertical and diagonal strings
            List<string> verticalStrings = new List<string>(columnCount);
            List<string> diagonalTlToBr = new List<string>();
            List<string> diagonalTrToBl = new List<string>();

            for (int i = 0; i < columnCount; i++)
                verticalStrings.Add(new string(horizontal.Select(row => row.Length > i ? row[i] : '\0').Where(ch => ch != '\0').ToArray()));

            // traverse diagonals from top-left to bottom-right and top-right to bottom-left
            for (int start = 0; start < rowCount + columnCount - 1; start++)
            {
                string diagonalTLToBR = string.Empty;
                string diagonalTRToBL = string.Empty;

                for (int i = 0; i <= start; i++)
                {
                    int row = i;
                    int colTLBR = start - i;
                    int colTRBL = columnCount - 1 - (start - i);

                    if (row < rowCount && colTLBR < columnCount)
                        diagonalTLToBR += horizontal[row][colTLBR];
                    if (row < rowCount && colTRBL >= 0)
                        diagonalTRToBL += horizontal[row][colTRBL];
                }

                if (!string.IsNullOrEmpty(diagonalTLToBR)) diagonalTlToBr.Add(diagonalTLToBR.ToLower());
                if (!string.IsNullOrEmpty(diagonalTRToBL)) diagonalTrToBl.Add(diagonalTRToBL.ToLower());
            }

            int CountMatches(List<string> lines) => lines.Sum(line => Regex.Matches(line, matchCondition, RegexOptions.IgnoreCase).Count);

            int total = CountMatches(horizontal) + CountMatches(verticalStrings) + CountMatches(diagonalTlToBr) + CountMatches(diagonalTrToBl);

            return total.ToString();
        }

        public string SolvePuzzleTwo(Puzzle input)
        {
            // split input into a list of lowercase strings
            List<string> grid = input.SplitNewline(2, true, true).Select(line => line.ToLower()).ToList();
            string matchCondition = $"(?=mas|sam)";
            int total = 0;

            // loop through rows, ignoring the first and last
            for (int row = 1; row < grid.Count - 1; row++)
            {
                string currentRow = grid[row];

                // loop through columns, ignoring the first and last
                for (int col = 1; col < currentRow.Length - 1; col++)
                {
                    char currentChar = currentRow[col];

                    // only interested in 'a' as the center of the 'X'
                    if (currentChar == 'a')
                    {
                        // get characters diagonally around 'a'
                        char tl = grid[row + 1][col - 1]; // bottom-left
                        char tr = grid[row + 1][col + 1]; // bottom-right
                        char bl = grid[row - 1][col - 1]; // top-left
                        char br = grid[row - 1][col + 1]; // top-right

                        // form the two diagonals as strings
                        string tlToBr = $"{tl}{currentChar}{br}";
                        string trToBl = $"{tr}{currentChar}{bl}";

                        // check if either diagonal matches 'mas' or 'sam'
                        var tlToBrCount = Regex.Matches(tlToBr, matchCondition, RegexOptions.IgnoreCase);
                        var trToBlCount = Regex.Matches(trToBl, matchCondition, RegexOptions.IgnoreCase);

                        // increment total if both diagonals have valid matches
                        if (tlToBrCount.Any() && trToBlCount.Any())
                        {
                            total++;
                        }
                    }
                }
            }

            return total.ToString();
        }
    }
}
