// <copyright file="OperatorNodeFactory.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine;

namespace SpreadsheetEngine
{
    /// <summary>
    /// This class decouples the implemented operators from the ExpressionTree class so
    /// that it's easier to add new operators.
    /// </summary>
    internal class OperatorNodeFactory
    {
        /// <summary>
        /// The implemented operators in the ExpressionTreeCalculator.
        /// </summary>
        public static char[] Operators = { '+', '-', '/', '*' };

        /// <summary>
        /// Returns the correct type of OperatorNode.
        /// </summary>
        /// <param name="op">The character operator (e.g. +, -, *, /).</param>
        /// <returns>A concrete subclass of OperatorNode abstract class.</returns>
        public static OperatorNode CreateOperatorNode(char op)
        {
            switch (op)
            {
                case '+':
                    return new AdditionOperatorNode('+');

                case '-':
                    return new SubtractionOperatorNode('-');

                case '/':
                    return new DivisionOperatorNode('/');

                case '*':
                    return new MultiplicationOperatorNode('*');

                default:
                    throw new Exception("This operator has not been implemented.");
            }
        }
    }
}
