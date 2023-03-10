﻿// <copyright file="ExpressionTree.cs" company="Molly Iverson:11775649">
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
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            this.root = null;
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
            this.root = this.ConvertPostfixToTree(this.expression);

            if (this.root == null)
            {
                throw new ArgumentNullException(nameof(this.root));
            }

            return this.root.Evaluate();
        }

        /// <summary>
        /// Converts the regular mathematical expression to postfix format.
        /// </summary>
        /// <param name="expression">A mathematical expression.</param>
        /// <returns>The expression in postfix format.</returns>
        private string ConvertToPostfix(string expression)
        {
            Stack<char> operatorStack = new Stack<char>();
            StringBuilder postfixString = new StringBuilder();

            int index = 0;

            while (index < expression.Length)
            {
                // If character is an operator
                if (this.IsOperator(expression[index]))
                {
                    postfixString.Append(" ");
                    OperatorNode currentOperator = OperatorNodeFactory.CreateOperatorNode(expression[index]);

                    if (operatorStack.Count > 0)
                    {
                        char nextOperator = operatorStack.Peek();
                        OperatorNode nextOperatorNode = OperatorNodeFactory.CreateOperatorNode(nextOperator);

                        // This is the method for left association. Will add others in next assignments.
                        while (operatorStack.Count > 0 && nextOperatorNode.Precedence >= currentOperator.Precedence)
                        {
                            nextOperator = operatorStack.Pop();
                            postfixString.Append(nextOperator.ToString());
                            postfixString.Append(" ");

                            if (operatorStack.Count != 0)
                            {
                                nextOperator = operatorStack.Peek();
                                nextOperatorNode = OperatorNodeFactory.CreateOperatorNode(nextOperator);
                            }
                        }
                    }

                    operatorStack.Push(expression[index]);
                }

                // Character is alphanumeric. Assuming valid input.
                else
                {
                    postfixString.Append(expression[index]);
                }

                index++;
            }

            while (operatorStack.Count > 0)
            {
                postfixString.Append(" " + operatorStack.Pop());
            }

            return postfixString.ToString();
        }

        /// <summary>
        /// Converts a Postfix expression into an ExpressionTree.
        /// </summary>
        /// <param name="postfixString">A mathematical expression in postfix form.</param>
        /// <returns>ExpressionTree.</returns>
        private Node? ConvertPostfixToTree(string postfixString)
        {
            string[] elements = postfixString.Split(' ');
            if (string.IsNullOrEmpty(postfixString))
            {
                return null;
            }

            Stack<Node> nodeStack = new Stack<Node>();

            foreach (var element in elements)
            {
                if (this.IsOperator(element[0]))
                {
                    // We need an Operator Node
                    Node rightOperand = nodeStack.Pop();
                    Node leftOperand = nodeStack.Pop();
                    OperatorNode operatorNode = OperatorNodeFactory.CreateOperatorNode(element[0]);
                    operatorNode.Left = leftOperand;
                    operatorNode.Right = rightOperand;
                    nodeStack.Push(operatorNode);
                }
                else
                {
                    double number;
                    if (double.TryParse(element, out number))
                    {
                        // We need a ConstantNode
                        ConstantNode constantNode = new ConstantNode(number);
                        nodeStack.Push(constantNode);
                    }
                    else
                    {
                        // We need a VariableNode
                        if (this.variableTable.ContainsKey(element))
                        {
                            VariableNode variableNode = new VariableNode(element, this.variableTable[element]);
                            nodeStack.Push(variableNode);
                        }
                        else
                        {
                            throw new Exception("The variable " + element + " has not been defined.");
                        }
                    }
                }
            }

            return nodeStack.Pop();
        }

        /// <summary>
        /// Determines if a character is a valid operator.
        /// </summary>
        /// <param name="op">Operator.</param>
        /// <returns>Whether the operator is valid.</returns>
        private bool IsOperator(char op)
        {
            return OperatorNodeFactory.Operators.Contains(op);
        }
    }
}
