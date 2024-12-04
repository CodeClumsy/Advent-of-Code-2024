using System;
using System.Collections.Generic;
using System.IO;

namespace AOC_2024.Utils
{
    internal static class FileHandler
    {
        /// <summary>
        /// Gets the file path for a specific day based on the day number.
        /// </summary>
        /// <param name="day">The day number (between 1 and 25).</param>
        /// <param name="puzzle">The puzzle number (always 1 or 2)</param>
        /// <returns>The full file path for the day's input file.</returns>
        public static string GetFilePathForDay(int day, int puzzle)
        {
            try
            {
                if (day < 1 || day > 25)
                {
                    throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 25.");
                }
                if (puzzle < 1 || puzzle > 2)
                {
                    throw new ArgumentOutOfRangeException(nameof(puzzle), "Puzzle must be between 1 and 2.");
                }

                // Get the root project directory
                string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Navigate up to the project directory if needed
                rootDirectory = Path.GetFullPath(Path.Combine(rootDirectory, "..", "..", ".."));

                // Construct the relative path to the file
                string relativePath = Path.Combine("Days", $"Day{day}-{puzzle}.txt");

                // Resolve the full path
                string fullPath = Path.Combine(rootDirectory, relativePath);

                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException($"The file for Day {day}, Puzzle {puzzle} was not found.", fullPath);
                }

                return fullPath;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads the contents of a file at the specified path and returns it as a string.
        /// </summary>
        /// <param name="filePath">The path of the file to read.</param>
        /// <returns>The file's contents as a string.</returns>
        public static string ReadFileContents(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
                }

                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File not found.", filePath);
                }

                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error reading file: {ex.Message}");
                throw; // Re-throwing to allow the caller to handle the exception if needed
            }
        }
    }
}