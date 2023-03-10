// <copyright file="ExpressionTreeTest.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine;

namespace SpreadsheetApplicationTests
{
    /// <summary>
    /// Tests the ExpressionTree class.
    /// </summary>
    public class ExpressionTreeTest
    {
        /// <summary>
        /// Sets up the tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Tests different types of expressions (different operators, precedence, parentheses, dividing by zero).
        /// </summary>
        /// <param name="expression">A string mathematical expression.</param>
        /// <returns>The result of the expression.</returns>
        [Test]
        [TestCase("3+5", ExpectedResult = 8.0)] // expression with single operator
        [TestCase("100/10*10", ExpectedResult = 100.0)] // mixing operators with same precedence
        [TestCase("100/(10*10)", ExpectedResult = 1.0)] // mixing operators with same precedence and parentheses
        [TestCase("7-4+2", ExpectedResult = 5.0)] // mixing operators +/- with same precedence
        [TestCase("10/(7-2)", ExpectedResult = 2.0)] // operators with different precedence and parentheses - higher precedence first
        [TestCase("(12-2)/2", ExpectedResult = 5.0)] // operators with different precedence and parentheses - lower precedence first
        [TestCase("(((((2+3)-(4+5)))))", ExpectedResult = -4.0)] // extra parentheses and negative result
        [TestCase("2*3+5", ExpectedResult = 11.0)] // operators with different precedence - higher precedence first
        [TestCase("2+3*5", ExpectedResult = 17.0)] // operators with different precedence - lower precedence first
        [TestCase("2 + 3 * 5", ExpectedResult = 17.0)] // spaces and mixing operators (+ and *) with different precedence
        [TestCase("5/0", ExpectedResult = double.PositiveInfinity)] // dividing a floating point value by zero doesn't throw error. Results in postive infinity
        public double TestEvaluateNormalCases(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        /// <summary>
        /// Tests if adding the maximum values results in infinity.
        /// </summary>
        [Test]
        public void TestInfinity()
        {
            string maxValue = double.MaxValue.ToString("F", System.Globalization.CultureInfo.InvariantCulture);
            double result = new ExpressionTree($"{maxValue}+{maxValue}").Evaluate();
            Assert.True(double.IsInfinity(result));
        }

        /// <summary>
        /// Tests using variables instead of just constants.
        /// </summary>
        [Test]
        public void TestExpressionsWithVariableValues()
        {
            ExpressionTree exp = new ExpressionTree("A5 + 5");
            exp.SetVariable("A5", 23);
            Assert.AreEqual(23, exp.Evaluate());
        }

        /// <summary>
        /// Tests an expression with an extra parentheses.
        /// </summary>
        /// <param name="expression">A string mathematical expression.</param>
        [TestCase("((2+5))-2(2+3))")]
        public void TestConstructInvalidExpression(string expression)
        {
            Assert.That(() => new ExpressionTree(expression), Throws.TypeOf<System.Exception>());
        }

        /// <summary>
        /// Tests if the ExpressionTree fails with an invalid operation character.
        /// </summary>
        /// <param name="expression">A string mathematical expression.</param>
        [TestCase("4%2")]
        public void TestEvaluateUnsupportedOperator(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            Assert.That(() => exp.Evaluate(), Throws.TypeOf<System.Collections.Generic.KeyNotFoundException>());
        }
    }
}
