using AOC_2024.Models.Interfaces;

namespace AOC_2024.DataModels.BaseClasses
{
    public abstract class PuzzleBase: IPuzzle
    {
        public abstract int Day { get; set; }
        public abstract string PuzzleOneInput { get; set; }
        public abstract string PuzzleTwoInput { get; set; }

        public abstract string GetAnswer(int puzzleNumber);

        protected List<string> SplitNewline(int puzzleNumber)
        {
            if (puzzleNumber < 1 || puzzleNumber > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(SplitNewline), "Puzzle must be between 1 and 2.");
            }

            return puzzleNumber == 1 ? PuzzleOneInput.Split("\n").ToList() : PuzzleTwoInput.Split("\n").ToList();
        }
    }
}
