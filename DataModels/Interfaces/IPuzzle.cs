using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2024.Models.Interfaces
{
    /// <summary>
    /// Defines the structure for an Advent of Code puzzle implementation.
    /// </summary>
    public interface IPuzzle
    {
        /// <summary>
        /// Gets or sets the day of the puzzle (1-25).
        /// </summary>
        int Day { get; set; }

        /// <summary>
        /// Gets or sets the file path for the input data of the first puzzle.
        /// </summary>
        string PuzzleOneInput { get; set; }

        /// <summary>
        /// Gets or sets the file path for the input data of the second puzzle.
        /// </summary>
        string PuzzleTwoInput { get; set; }

        /// <summary>
        /// Calculates and returns the solution for the puzzle.
        /// </summary>
        /// <returns>The solution to the second puzzle as a string.</returns>
        string GetAnswer(int puzzleNumber);
    }
}
