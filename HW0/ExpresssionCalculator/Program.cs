// <copyright file="Program.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System.Collections;
using SpreadsheetEngine;

/// <summary>
/// The main entry point to the application.
/// </summary>
internal class Program
{
    /// <summary>
    /// Runs the main console application.
    /// </summary>
    /// <param name="args">Any command line arguments.</param>
    public static void Main(string[] args)
    {
        bool keepRunning = true;

        ExpressionTree expressionTree = new ExpressionTree("A1-12-C1");
        expressionTree.SetVariable("A1", 0);
        expressionTree.SetVariable("C1", 0);

        while (keepRunning)
        {
            PrintMenu(expressionTree.Expression);
            int menuChoice = Convert.ToInt32(Console.ReadLine()); // Will fail if input is not an int

            while (menuChoice < 1 || menuChoice > 4)
            {
                Console.WriteLine("Invalid input. Please try again.");
                menuChoice = Convert.ToInt32(Console.ReadLine());
            }

            if (menuChoice == 1)
            {
                Console.Write("Enter new expression: ");
                string? newExpression = Console.ReadLine();

                if (newExpression == null)
                {
                    Console.WriteLine("Invalid input.");
                }
                else
                {
                    expressionTree.Expression = newExpression;
                }
            }
            else if (menuChoice == 2)
            {
                Console.Write("Enter variable name: ");
                string? variableName = Console.ReadLine(); // Assumes valid input (single word, no whitespace)
                if (variableName == null || variableName == "\n")
                {
                    Console.WriteLine("Invalid input");
                    return;
                }

                Console.Write("Enter variable value: ");
                string? variableValue = Console.ReadLine();
                if (variableValue == null || variableValue == "\n")
                {
                    Console.WriteLine("Invalid input");
                    return;
                }

                double value = Convert.ToDouble(variableValue);
                expressionTree.SetVariable(variableName, value);
            }
            else if (menuChoice == 3)
            {
                double result = expressionTree.Evaluate();
                Console.WriteLine("Answer: " + result);
            }
            else
            {
                keepRunning = false;
                Console.WriteLine("Goodbye!");
            }
        }
    }

    /// <summary>
    /// Prints the main menu to the screen.
    /// </summary>
    /// <param name="currentExpression">The current mathematical expression in the expression tree.</param>
    public static void PrintMenu(string currentExpression)
    {
        Console.WriteLine("Menu (current expression = \"" + currentExpression + "\")");
        Console.WriteLine("\t1 = Enter a new expression");
        Console.WriteLine("\t2 = Set a variable value");
        Console.WriteLine("\t3 = Evaluate tree");
        Console.WriteLine("\t4 = Quit");
    }
}