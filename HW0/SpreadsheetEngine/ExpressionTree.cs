// <copyright file="ExpressionTree.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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
        /// A mathematical expression containing constants, variables and/or operators.
        /// </summary>
        private string expression;

        /// <summary>
        /// Contains all the variables and their values.
        /// </summary>
        private Dictionary<string, double> variableTable;

        /// <summary>
        /// The main operator node containing all the other nodes of the expression.
        /// </summary>
        private Node? root;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">The math expression with variables, operators, and constants.</param>
        public ExpressionTree(string expression)
        {
            this.expression = expression;
            this.variableTable = new Dictionary<string, double>();
        }

        /// <summary>
        /// Gets or sets the mathematic expression.
        /// </summary>
        public string Expression
        {
            get { return this.expression; }
            set { this.expression = value; }
        }

        /// <summary>
        /// Sets the specified variable within the ExpressionTree variables dictionary.
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="variableValue">The value of the variable.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variableTable[variableName] = variableValue;
        }

        /// <summary>
        /// Evaluates the arithmetic expression.
        /// </summary>
        /// <returns>The answer.</returns>
        public double Evaluate()
        {
            this.root = this.Compile(this.expression);
            if (this.root == null)
            {
                throw new ArgumentNullException(nameof(this.root));
            }

            return this.root.Evaluate();
        }

        /// <summary>
        /// Converts an expression into Node objects.
        /// </summary>
        /// <param name="s">The string expression.</param>
        /// <returns>A node representing the expression.</returns>
        private Node? Compile(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            // Check for extra parentheses and get rid of them, e.g. (((((2+3)-(4+5)))))
            if (s[0] == '(')
            {
                int parenthesisCounter = 1;
                for (int characterIndex = 1; characterIndex < s.Length; characterIndex++)
                {
                    // if open parenthesis increment the counter
                    if (s[characterIndex] == '(')
                    {
                        parenthesisCounter++;
                    }

                    // if closed parenthesis decrement the counter
                    else if (s[characterIndex] == ')')
                    {
                        parenthesisCounter--;

                        // if the counter is 0 check where we are
                        if (parenthesisCounter == 0)
                        {
                            if (characterIndex != s.Length - 1)
                            {
                                // if we are not at the end, then get out (there are no extra parentheses)
                                break;
                            }
                            else
                            {
                                // else get rid of the outer most parentheses and start over
                                return this.Compile(s.Substring(1, s.Length - 2));
                            }
                        }
                    }
                }
            }

            // define the operators we want to look for in that order
            char[] operators = OperatorNodeFactory.Operators;
            foreach (char op in operators)
            {
                Node? n = this.Compile(s, op);
                if (n != null)
                {
                    return n;
                }
            }

            double number;
            if (double.TryParse(s, out number))
            {
                // We need a ConstantNode
                ConstantNode constantNode = new ConstantNode(number);
                return constantNode;
            }
            else
            {
                // We need a VariableNode
                if (this.variableTable.ContainsKey(s))
                {
                    VariableNode variableNode = new VariableNode(s, this.variableTable[s]);
                    return variableNode;
                }
                else
                {
                    throw new Exception("The variable " + s + "has not been defined.");
                }
            }
        }

        /// <summary>
        /// Converts an expression into an OperatorNode.
        /// </summary>
        /// <param name="expression">The string expression.</param>
        /// <param name="op">The character operation.</param>
        /// <returns>A node representing the expression.</returns>
        private Node? Compile(string expression, char op)
        {
            // track the parentheses
            int parenthesisCounter = 0;

            // iterate from back to front
            for (int expressionIndex = expression.Length - 1; expressionIndex >= 0; expressionIndex--)
            {
                // if closed parenthisis INcrement the counter
                if (expression[expressionIndex] == ')')
                {
                    parenthesisCounter++;
                }

                // if open parenthisis DEcrement the counter
                else if (expression[expressionIndex] == '(')
                {
                    parenthesisCounter--;
                }

                // if the counter is at 0 and we have the operator that we are looking for
                if (parenthesisCounter == 0 && op == expression[expressionIndex])
                {
                    // build an operator node
                    OperatorNode operatorNode = OperatorNodeFactory.CreateOperatorNode(expression[expressionIndex]);

                    // and start over with the left and right sub-expressions
                    operatorNode.Left = this.Compile(expression.Substring(0, expressionIndex));
                    operatorNode.Right = this.Compile(expression.Substring(expressionIndex + 1));
                    return operatorNode;
                }
            }

            // if the counter is not at 0 something was off
            if (parenthesisCounter != 0)
            {
                // throw a general exception
                throw new Exception("There was an issue counting parentheses.");
            }

            // we did not find the operator
            return null;
        }
    }
}
