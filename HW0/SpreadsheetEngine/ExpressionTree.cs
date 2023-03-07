// <copyright file="ExpressionTree.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Implement an arithmetic expression parser that builds a tree for the expression.
    /// This tree can then be used for evaluation of the expressiom.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">The math expression with variables, operators, and constants.</param>
        public ExpressionTree(string expression)
        {
        }

        /// <summary>
        /// Sets the specified variable within the ExpressionTree variables dictionary.
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="variableValue">The value of the variable.</param>
        public void SetVariable(string variableName, double variableValue)
        {
        }

        /// <summary>
        /// Evaluates the arithmetic expression.
        /// </summary>
        /// <returns>The answer.</returns>
        public double Evaluate()
        {
            return 0;
        }
    }
}
