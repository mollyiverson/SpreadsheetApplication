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
        /// Normal Case: Tests sinple expressions with varying operators.
        /// </summary>
        /// <param name="expression">A string mathematical expression.</param>
        /// <returns>The result of the expression.</returns>
        [Test]
        [TestCase("3+5", ExpectedResult = 8.0)] // expression with single operator
        [TestCase("7-4+2", ExpectedResult = 5.0)] // mixing operators +/- with same precedence
        [TestCase("2*3+5", ExpectedResult = 11.0)] // operators with different precedence - higher precedence first
        [TestCase("2+3*5", ExpectedResult = 17.0)] // operators with different precedence - lower precedence first
        [TestCase("100/10*10", ExpectedResult = 100.0)] // mixing operators with same precedence
        public double TestEvaluateNormalCases(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        /// <summary>
        /// Normal Case: Tests expressions with parentheses and mixed operators.
        /// </summary>
        /// <param name="expression">A string mathematical expression.</param>
        /// <returns>The result of the expression.</returns>
        [Test]
        [TestCase("(((((2+3)-(4+5)))))", ExpectedResult = -4.0)] // extra parentheses and negative result
        [TestCase("10/(7-2)", ExpectedResult = 2.0)] // operators with different precedence and parentheses - higher precedence first
        [TestCase("(12-2)/2", ExpectedResult = 5.0)] // operators with different precedence and parentheses - lower precedence first
        [TestCase("100/(10*10)", ExpectedResult = 1.0)] // mixing operators with same precedence and parentheses

        public double TestParentheses(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        /// <summary>
        /// Normal Case: Tests an expression with whitespace between operators and operands.
        /// </summary>
        /// <param name="expression">A string mathematical expression.</param>
        /// <returns>The result of the expression.</returns>
        [Test]
        [TestCase("1.4          *      \n3", ExpectedResult = 1.4 * 3)]
        [TestCase("2 + 3 * 5", ExpectedResult = 17.0)]
        public double TestExpressionWithWhitespace(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        /// <summary>
        /// Edge Case: Tests dividing by zero.
        /// </summary>
        [Test]
        public void TestDivideByZero()
        {
            ExpressionTree exp = new ExpressionTree("5/0");
            Assert.That(exp.Evaluate(), Is.EqualTo(double.PositiveInfinity));
        }

        /// <summary>
        /// Edge Case: Tests if adding the maximum values results in infinity.
        /// </summary>
        [Test]
        public void TestInfinity()
        {
            string maxValue = double.MaxValue.ToString("F", System.Globalization.CultureInfo.InvariantCulture);
            double result = new ExpressionTree($"{maxValue}+{maxValue}").Evaluate();
            Assert.That(double.IsInfinity(result), Is.True);
        }

        /// <summary>
        /// Normal Case: Tests using variables instead of just constants.
        /// </summary>
        [Test]
        public void TestExpressionsWithVariableValues()
        {
            ExpressionTree exp = new ExpressionTree("A5+5");
            exp.SetVariable("A5", 23);
            Assert.That(exp.Evaluate(), Is.EqualTo(28));
        }

        /// <summary>
        /// Exception Case: Tests an expression with an invalid number of parentheses(not matching).
        /// </summary>
        /// <param name = "expression">A mathematical expression.</param>
        [TestCase("((2+5))-2(2+3))")]
        public void TestConstructInvalidExpression(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            Assert.That(() => exp.Evaluate(), Throws.TypeOf<System.Exception>());
        }

        /// <summary>
        /// Exception Case: Tests if the ExpressionTree fails with an invalid operation character.
        /// </summary>
        /// <param name="expression">A string mathematical expression.</param>
        [TestCase("4%2")]
        public void TestEvaluateUnsupportedOperator(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            Assert.That(() => exp.Evaluate(), Throws.TypeOf<System.Collections.Generic.KeyNotFoundException>());
        }

        /// <summary>
        /// Normal Case: Tests if variables are cleared when the expression changes.
        /// </summary>
        [Test]
        public void TestNewExpressionClearVariables()
        {
            // I know we should have two assert statements in one test, but I believe it's necessary
            // for this one. I have to know that A6 was set before in order to know if it changed.
            ExpressionTree exp = new ExpressionTree("4+A6");
            exp.SetVariable("A6", 6);
            Assert.That(exp.Evaluate(), Is.EqualTo(10));
            exp.Expression = "A6+2";
            Assert.That(exp.Evaluate(), Is.EqualTo(2));
        }

        /// <summary>
        /// Normal Case: Tests an expression with decimals.
        /// </summary>
        [Test]
        public void TestDecimals()
        {
            ExpressionTree exp = new ExpressionTree("4.2-1.1");
            Assert.That(exp.Evaluate(), Is.EqualTo(3.1));
        }
    }
}