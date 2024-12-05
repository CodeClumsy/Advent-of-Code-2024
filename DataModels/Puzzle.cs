using AOC_2024.DataModels.BaseClasses;
using AOC_2024.Utils;
using AOC_2024.Controllers;


namespace AOC_2024.DataModels
{
    /// <summary>
    /// Represents an Advent of Code puzzle, containing logic to retrieve input and calculate answers.
    /// </summary>
    public class Puzzle
    {
        /// <summary>
        /// Gets or sets the day of the puzzle (1-25).
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets the input data for the first puzzle of the day.
        /// </summary>
        public string PuzzleOneInput { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the input data for the second puzzle of the day.
        /// </summary>
        public string PuzzleTwoInput { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Puzzle"/> class for the specified day.
        /// </summary>
        /// <param name="day">The day of the puzzle (must be between 1 and 25).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the day is not between 1 and 25.</exception>
        public Puzzle(int day)
        {
            if (day < 1 || day > 25)
            {
                throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 25.");
            }

            this.Day = day;
        }

        /// <summary>
        /// Calculates and retrieves the answer for the specified puzzle number.
        /// </summary>
        /// <param name="puzzleNumber">The puzzle number (1 for the first puzzle, 2 for the second).</param>
        /// <returns>The answer to the specified puzzle as a string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the puzzle number is not 1 or 2.</exception>
        /// <exception cref="Exception">Thrown if any error occurs while retrieving the answer.</exception>
        public string GetAnswer(int puzzleNumber)
        {
            try
            {
                // Ensure puzzle number is valid
                if (puzzleNumber < 1 || puzzleNumber > 2)
                {
                    throw new ArgumentOutOfRangeException(nameof(puzzleNumber), "Puzzle number must be either 1 or 2.");
                }

                // Ensure input file exists and set the appropriate puzzle input property
                string pathToInput = FileHandler.GetFilePathForDay(Day, puzzleNumber);
                if (puzzleNumber == 1)
                {
                    this.PuzzleOneInput = FileHandler.ReadFileContents(pathToInput);
                }
                else if (puzzleNumber == 2)
                {
                    this.PuzzleTwoInput = FileHandler.ReadFileContents(pathToInput);
                }

                // Return the calculated puzzle answer
                return AocManager.GetAnswer(this, puzzleNumber);
            }
            catch
            {
                throw;
            }
        }

        public List<string> SplitNewline(int puzzleNumber, bool removeBlankLines = false, bool trimLines = false)
        {
            if (puzzleNumber < 1 || puzzleNumber > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(SplitNewline), "Puzzle must be between 1 and 2.");
            }

            List<string> outData = puzzleNumber == 1 ? PuzzleOneInput.Split("\n").ToList() : PuzzleTwoInput.Split("\n").ToList();

            if (removeBlankLines)
            {
                outData = outData.Where(line => !string.IsNullOrEmpty(line)).ToList();
            }

            if (trimLines)
            {
                for (int i = 0; i < outData.Count; i++)
                {
                    outData[i] = outData[i].Trim();
                }
            }

            return outData;
        }

        public List<List<string>> SplitLinesBy(int puzzleNumber, string delimiter)
        {
            return SplitNewline(puzzleNumber, true)
                .Select(i => i
                    .Split(delimiter).ToList()
                    ).ToList();
        }

        public List<List<int>> SplitLinesToIntLists(int puzzleNumber, string delimiter)
        {
            return SplitLinesBy(puzzleNumber, delimiter)
                .Select(line => line
                    .Select(num => int.Parse(num)).ToList()
                    ).ToList();
        }
    }
}

