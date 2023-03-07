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

        /// <summary>
        /// Normal Case: Tests if a Spreadsheet has the correct row count.
        /// </summary>
        [Test]
        public void TestRows()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 7);
            Assert.That(spreadsheet.RowCount, Is.EqualTo(5));
        }

        /// <summary>
        /// Normal Case: Tests if a Spreadsheet has the correct column count.
        /// </summary>
        [Test]
        public void TestColumns()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 7);
            Assert.That(spreadsheet.ColumnCount, Is.EqualTo(7));
        }
    }
}