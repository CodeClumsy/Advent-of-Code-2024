using AOC_2024.DataModels;
using AOC_2024.Models.Interfaces;

namespace AOC_2024.Controllers
{
    /// <summary>
    /// Manages the orchestration of solving Advent of Code puzzles by dynamically registering solvers and routing puzzle-solving requests.
    /// </summary>
    public class AocManager
    {
        /// <summary>
        /// A dictionary containing registered solvers for each day, keyed by the day number.
        /// </summary>
        private static readonly Dictionary<int, IPuzzleSolver> Solvers = new();

        /// <summary>
        /// Initializes the <see cref="AocManager"/> class by dynamically discovering and registering all solvers in the current assembly.
        /// </summary>
        static AocManager()
        {
            // Get the type of the IPuzzleSolver interface
            var solverType = typeof(IPuzzleSolver);

            // Dynamically find and instantiate all types implementing IPuzzleSolver
            var solverInstances = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => solverType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(type => (IPuzzleSolver)Activator.CreateInstance(type))
                .ToList();

            // Register solvers in the dictionary, using the day number extracted from the class name
            foreach (var solver in solverInstances)
            {
                var dayProperty = solver.GetType().Name.Replace("Day", "").Replace("Solver", "");
                if (int.TryParse(dayProperty, out int day))
                {
                    Solvers[day] = solver;
                }
            }
        }

        /// <summary>
        /// Solves a specific puzzle by day and puzzle number.
        /// </summary>
        /// <param name="day">The day of the puzzle (1-25).</param>
        /// <param name="puzzleNumber">The puzzle number (1 or 2).</param>
        /// <returns>The solution to the specified puzzle as a string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the day or puzzle number is out of range.</exception>
        /// <exception cref="InvalidOperationException">Thrown if no solver is registered for the specified day.</exception>
        public static string Solve(int day, int puzzleNumber)
        {
            string solution = new Puzzle(day).GetAnswer(puzzleNumber);

            return $"Day {day}, Puzzle {puzzleNumber}, Answer: {solution}";
        }

        /// <summary>
        /// Routes the request to the appropriate solver for the given puzzle.
        /// </summary>
        /// <param name="puzzle">The puzzle object containing the day and input data.</param>
        /// <param name="puzzleNumber">The puzzle number (1 or 2).</param>
        /// <returns>The solution to the specified puzzle as a string.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no solver is registered for the specified day.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the puzzle number is not 1 or 2.</exception>
        public static string GetAnswer(Puzzle puzzle, int puzzleNumber)
        {
            if (!Solvers.ContainsKey(puzzle.Day))
            {
                throw new InvalidOperationException($"No solver registered for Day {puzzle.Day}.");
            }

            var solver = Solvers[puzzle.Day];
            return puzzleNumber switch
            {
                1 => solver.SolvePuzzleOne(puzzle),
                2 => solver.SolvePuzzleTwo(puzzle),
                _ => throw new ArgumentOutOfRangeException(nameof(puzzleNumber), "Puzzle number must be 1 or 2.")
            };
        }
    }
}
