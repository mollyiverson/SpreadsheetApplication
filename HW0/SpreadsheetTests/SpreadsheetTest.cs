// <copyright file="SpreadsheetTest.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>
using System.Reflection;
using SpreadsheetEngine;

namespace SpreadsheetApplicationTests
{
    /// <summary>
    /// Tests the Spreadsheet class in the SpreadsheetEngine library.
    /// </summary>
    public class SpreadsheetTest
    {
        private Spreadsheet objectUnderTest = new Spreadsheet(5, 5);

        /// <summary>
        /// Used to access private methods of another class.
        /// </summary>
        /// <param name="methodName">The name of the private method to access.</param>
        /// <returns>A private method.</returns>
        private MethodInfo GetMethod(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                Assert.Fail("methodName cannot be null or whitespace");
            }

            var method = this.objectUnderTest.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            if (method == null)
            {
                Assert.Fail(string.Format("{0} method not found", methodName));
            }

            return method;
        }

        /// <summary>
        /// Sets up the tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Edge Case: Tests the GetCell() method of the Spreadsheet class.
        /// Tests whether it returns null when it's asked to get a cell out of
        /// range.
        /// </summary>
        [Test]
        public void TestGetCellNull()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Assert.That(spreadsheet.GetCell(6, 6), Is.EqualTo(null));
        }
    }
}