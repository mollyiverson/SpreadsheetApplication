// <copyright file="ExpressionTreeTest.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void TestAddConstants()
        {
            ExpressionTree expressionTree = new ExpressionTree("1 + 2");
            Assert.AreEqual(3, expressionTree.Evaluate());
        }

        [Test]
        public void TestSubstractVariable()
        {
            ExpressionTree expressionTree = new ExpressionTree("10 - b");
            expressionTree.SetVariable("b", 2);
            Assert.AreEqual(8, expressionTree.Evaluate());
        }

        [Test]
        public void TestExtraParentheses()
        {
            ExpressionTree expressionTree = new ExpressionTree("(((3 + 3)))");
            Assert.AreEqual(6, expressionTree.Evaluate());
        }
    }
}
