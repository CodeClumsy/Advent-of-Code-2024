using AOC_2024.DataModels;
using AOC_2024.Models.Interfaces;
using System.Text.RegularExpressions;

namespace AOC_2024.Solutions
{
    public class Day3Solver : IPuzzleSolver
    {
        public string SolvePuzzleOne(Puzzle input)
        {
            string memory = input.PuzzleOneInput.Replace("\n", "").Replace(" ", "").ToLower();
            var matches = Regex.Matches(memory, @"mul\((\d{1,3}),(\d{1,3})\)");
            int result = 0;

            // Loop through the matches
            foreach (Match match in matches)
            {
                // Extract the two numbers from the capturing groups
                int firstNumber = int.Parse(match.Groups[1].Value);
                int secondNumber = int.Parse(match.Groups[2].Value);

                // Multiply and add to result
                result += firstNumber * secondNumber;
            }

            // return result;
            // could return here. regex is obviously the way to do this, but I felt like solving it again manually.
            // Hence:

            result = 0;
            int totalChars = memory.Length;
            // Loop through each character in the string
            for (int i = 0; i < totalChars; i++)
            {
                // Check if the current position starts the pattern "mul("
                // This ensures I only process when I encounter a valid starting point
                if (memory[i] == 'm' &&
                    memory[i + 1] == 'u' &&
                    memory[i + 2] == 'l' &&
                    memory[i + 3] == '(')
                {
                    // Initialize placeholders for the two numbers to be extracted
                    string firstNumber = "";
                    string secondNumber = "";

                    // Flag to indicate whether I successfully parsed the first number
                    bool nextNumber = false;

                    // This variable tracks the last index of the first number parsed
                    int lastIndexOfFirstNumber = 0;

                    // Begin parsing the first number, starting right after "mul("
                    if (int.TryParse(memory[i + 4].ToString(), out _))
                    {
                        // If the first character after "mul(" is a number, append it to the first number
                        firstNumber += memory[i + 4];
                        lastIndexOfFirstNumber = 4;

                        // Check if the next character is also a number
                        if (int.TryParse(memory[i + 5].ToString(), out _))
                        {
                            firstNumber += memory[i + 5];
                            lastIndexOfFirstNumber = 5;

                            // Check for a third digit in the first number
                            if (int.TryParse(memory[i + 6].ToString(), out _))
                            {
                                firstNumber += memory[i + 6];
                                lastIndexOfFirstNumber = 6;

                                // Check if the first number is correctly terminated by a comma
                                if (memory[i + 7] == ',')
                                {
                                    nextNumber = true; // Indicate that I'm ready to parse the second number
                                }
                            }
                            else if (memory[i + 6] == ',') // If only two digits, check for comma
                            {
                                nextNumber = true; // First number is valid
                            }
                            else
                            {
                                continue; // Skip to the next iteration if invalid
                            }
                        }
                        else if (memory[i + 5] == ',') // Handle single-digit first numbers
                        {
                            nextNumber = true; // First number is valid
                        }
                        else
                        {
                            continue; // Skip if the first number is invalid
                        }

                        // Parse the second number if the first number was successfully parsed
                        if (nextNumber)
                        {
                            // Check if the first character of the second number is a digit
                            if (int.TryParse(memory[i + lastIndexOfFirstNumber + 2].ToString(), out _))
                            {
                                secondNumber += memory[i + lastIndexOfFirstNumber + 2];

                                // Check for a second digit in the second number
                                if (int.TryParse(memory[i + lastIndexOfFirstNumber + 3].ToString(), out _))
                                {
                                    secondNumber += memory[i + lastIndexOfFirstNumber + 3];

                                    // Check for a third digit in the second number
                                    if (int.TryParse(memory[i + lastIndexOfFirstNumber + 4].ToString(), out _))
                                    {
                                        secondNumber += memory[i + lastIndexOfFirstNumber + 4];

                                        // Ensure the second number is terminated by a closing parenthesis
                                        if (memory[i + lastIndexOfFirstNumber + 5] == ')')
                                        {
                                            // Multiply the two numbers and add the result to the accumulator
                                            result += (int.Parse(firstNumber) * int.Parse(secondNumber));
                                            continue; // Continue to the next iteration
                                        }
                                        else
                                        {
                                            continue; // Skip if the closing parenthesis is missing
                                        }
                                    }
                                    else if (memory[i + lastIndexOfFirstNumber + 4] == ')') // Handle two-digit second numbers
                                    {
                                        result += (int.Parse(firstNumber) * int.Parse(secondNumber));
                                        continue; // Continue to the next iteration
                                    }
                                    else
                                    {
                                        continue; // Skip if the second number is invalid
                                    }
                                }
                                else if (memory[i + lastIndexOfFirstNumber + 3] == ')') // Handle single-digit second numbers
                                {
                                    result += (int.Parse(firstNumber) * int.Parse(secondNumber));
                                    continue; // Continue to the next iteration
                                }
                            }
                            else
                            {
                                continue; // Skip if the second number is invalid
                            }
                        }
                        else
                        {
                            continue; // Skip if the first number is invalid
                        }
                    }
                }
            }

            return result.ToString();
        }

        public string SolvePuzzleTwo(Puzzle input)
        {
            string memory = input.PuzzleTwoInput.Replace("\n", "").Replace(" ", "").ToLower();

            // Regex to find mul, do, or don't instructions
            var matches = Regex.Matches(memory, @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)");

            int result = 0;
            bool mulEnabled = true;  // Initially, mul instructions are enabled

            // Loop through the matches
            foreach (Match match in matches)
            {
                if (match.Value == "do()")
                {
                    // Enable mul instructions
                    mulEnabled = true;
                }
                else if (match.Value == "don't()")
                {
                    // Disable mul instructions
                    mulEnabled = false;
                }
                else if (match.Groups[1].Success && match.Groups[2].Success) // It's a mul() instruction
                {
                    if (mulEnabled)
                    {
                        // Extract the two numbers from the capturing groups
                        int firstNumber = int.Parse(match.Groups[1].Value);
                        int secondNumber = int.Parse(match.Groups[2].Value);

                        // Multiply and add to result
                        result += firstNumber * secondNumber;
                    }
                }
            }

            return result.ToString();
        }
    }
}
