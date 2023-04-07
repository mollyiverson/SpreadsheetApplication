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
        /// Creates instances of the OperatorNode subclasses.
        /// </summary>
        private OperatorNodeFactory operatorNodeFactory;

        /// <summary>
        /// Allows the outside project to implement their own way of accessing their private data.
        /// </summary>
        private Func<string, double>? getCellValue;

        /// <summary>
        /// Determines if the Variable values should come from the ExpressionTree variableTable
        /// or from the outside project via the getCellValue function.
        /// </summary>
        private bool useDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">The math expression with variables, operators, and constants.</param>
        /// <param name="getCellValue">Function so outside project can send in their own private data.</param>
        public ExpressionTree(string expression, Func<string, double> getCellValue)
        {
            this.expression = expression;
            this.variableTable = new Dictionary<string, double>();
            this.root = null;
            this.operatorNodeFactory = new OperatorNodeFactory();
            this.getCellValue = getCellValue;
            this.useDictionary = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">The math expression with variables, operators, and constants.</param>
        public ExpressionTree(string expression)
        {
            this.expression = expression;
            this.variableTable = new Dictionary<string, double>();
            this.root = null;
            this.operatorNodeFactory = new OperatorNodeFactory();
            this.useDictionary = true;
            this.getCellValue = null;
        }

        /// <summary>
        /// Gets or sets the mathematic expression.
        /// </summary>
        public string Expression
        {
            get
            {
                return this.expression;
            }

            set
            {
                if (this.expression != value)
                {
                    this.expression = value;
                    this.variableTable.Clear();
                }
            }
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
            string expressionNoWhitespace = string.Concat(this.expression.Where(c => !char.IsWhiteSpace(c)));
            string postfixExpression = this.ConvertToPostfix(expressionNoWhitespace);
            this.root = this.ConvertPostfixToTree(postfixExpression);

            if (this.root == null)
            {
                throw new ArgumentNullException(nameof(this.root));
            }

            return this.root.Evaluate();
        }

        /// <summary>
        /// Returns a list of all variables involved in the current expression.
        /// </summary>
        /// <returns>A list of string variable names.</returns>
        public List<string> GetVariables()
        {
            return this.variableTable.Keys.ToList();
        }

        /// <summary>
        /// Determines if a character is alphanumeric.
        /// </summary>
        /// <param name="character">A char.</param>
        /// <returns>Whether the character is alphanumeric.</returns>
        private static bool IsAlphaNumeric(char character)
        {
            // If character is in the letter or number range.
            if ((character >= 65 && character <= 90) || (character >= 97 && character <= 122)
                || (character >= 48 && character <= 57))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Allows outside projects to implement their own function that accesses their own data.
        /// </summary>
        /// <param name="variable">The name of the variable being accessed.</param>
        /// <param name="function">The outside function that gets the data for the variable.</param>
        /// <returns>The variable's data.</returns>
        private double LookUpVariable(string variable, Func<string, double> function)
        {
            return function(variable);
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
                if (this.operatorNodeFactory.IsOperator(expression[index]))
                {
                    postfixString.Append(' ');
                    OperatorNode currentOperator = this.operatorNodeFactory.CreateOperatorNode(expression[index]);

                    if (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                    {
                        char nextOperator = operatorStack.Peek();
                        OperatorNode nextOperatorNode = this.operatorNodeFactory.CreateOperatorNode(nextOperator);

                        // This is the method for left association. Will add others in next assignments.
                        while (operatorStack.Count > 0 && operatorStack.Peek() != '(' && nextOperatorNode.Precedence >= currentOperator.Precedence)
                        {
                            nextOperator = operatorStack.Pop();
                            postfixString.Append(' ');
                            postfixString.Append(nextOperator);
                            postfixString.Append(' ');

                            if (operatorStack.Count != 0 && operatorStack.Peek() != '(')
                            {
                                nextOperator = operatorStack.Peek();
                                nextOperatorNode = this.operatorNodeFactory.CreateOperatorNode(nextOperator);
                            }
                        }
                    }

                    operatorStack.Push(expression[index]);
                }
                else if (expression[index] == ')')
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                    {
                        postfixString.Append(' ');
                        postfixString.Append(operatorStack.Pop());
                    }

                    if (operatorStack.Count == 0)
                    {
                        throw new System.Exception("Invalid expression.");
                    }

                    postfixString.Append(' ');
                    operatorStack.Pop();    // pop the left parenthesis and discard it
                }
                else if (expression[index] == '(')
                {
                    operatorStack.Push(expression[index]);
                }
                else
                {
                    // Character is alphanumeric or a decimal point.
                    if (!(IsAlphaNumeric(expression[index]) || expression[index] == '.'))
                    {
                        throw new System.Collections.Generic.KeyNotFoundException();
                    }

                    postfixString.Append(expression[index]);
                }

                index++;
            }

            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek() == '(')
                {
                    throw new System.Exception("Invalid expression.");
                }

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
            bool invalidVariableRef = false;
            string[] elements = System.Text.RegularExpressions.Regex.Split(postfixString, @"\s+");
            if (string.IsNullOrEmpty(postfixString))
            {
                return null;
            }

            Stack<Node> nodeStack = new Stack<Node>();

            foreach (var element in elements)
            {
                if (element == string.Empty)
                {
                    break;
                }

                if (this.operatorNodeFactory.IsOperator(element[0]))
                {
                    // We need an Operator Node
                    Node rightOperand = nodeStack.Pop();
                    Node leftOperand = nodeStack.Pop();
                    OperatorNode operatorNode = this.operatorNodeFactory.CreateOperatorNode(element[0]);
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
                        if (this.useDictionary)
                        {
                            if (this.variableTable.TryGetValue(element, out double value))
                            {
                                VariableNode variableNode = new VariableNode(element, value);
                                nodeStack.Push(variableNode);
                            }
                            else
                            {
                                this.variableTable[element] = 0;
                                VariableNode variableNode = new VariableNode(element, 0);
                                nodeStack.Push(variableNode);
                            }
                        }
                        else
                        {
                            if (this.getCellValue != null)
                            {
                                VariableNode variableNode;
                                try
                                {
                                    variableNode = new VariableNode(element, this.LookUpVariable(element, this.getCellValue));
                                }
                                catch
                                {
                                    variableNode = new VariableNode(element, 0);
                                    invalidVariableRef = true;
                                }

                                nodeStack.Push(variableNode);
                                this.variableTable[element] = 0;
                            }
                            else
                            {
                                throw new Exception("Conflict between whether to use ExpressionTree dictionary or get variables from outside source.");
                            }
                        }
                    }
                }
            }

            if (invalidVariableRef == true)
            {
                return null;
            }
            else
            {
                return nodeStack.Pop();
            }
        }
    }
}