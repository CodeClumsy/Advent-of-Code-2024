using AOC_2024.DataModels;

namespace AOC_2024.Models.Interfaces
{
    public interface IPuzzleSolver
    {
        string SolvePuzzleOne(Puzzle input);
        string SolvePuzzleTwo(Puzzle input);
    }
}
