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

        /// <summary>
        /// Normal Case: Tests setting a cell in a spreadsheet to an expression with only constants.
        /// </summary>
        [Test]
        public void TestArithmeticAssignment()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(1, 1);
            if (cell != null)
            {
                cell.Text = "=4*3";
                Assert.That(cell.Value, Is.EqualTo("12"));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Normal Case: Tests setting a cell in a spreadsheet to another cell.
        /// </summary>
        [Test]
        public void TestReferenceToOtherCell()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(1, 1);
            Cell? cell2 = spreadsheet.GetCell(2, 2);
            if (cell != null && cell2 != null)
            {
                cell.Text = "15";
                cell2.Text = "=B2";
                Assert.That(cell2.Value, Is.EqualTo("15"));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Normal Case: Tests setting a cell in a spreadsheet to an expression that references another cell.
        /// </summary>
        [Test]
        public void TestFormulaWithReference()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(1, 1);
            Cell? cell2 = spreadsheet.GetCell(2, 2);
            if (cell != null && cell2 != null)
            {
                cell.Text = "15";
                cell2.Text = "=B2 + 20";
                Assert.That(cell2.Value, Is.EqualTo("35"));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Edge Case: Tests making a cell reference an empty cell.
        /// </summary>
        [Test]
        public void TestReferenceEmptyCell()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(1, 1);
            Cell? cell2 = spreadsheet.GetCell(2, 2);
            if (cell != null && cell2 != null)
            {
                cell2.Text = "=B2";
                Assert.That(cell2.Value, Is.EqualTo(string.Empty));
                Assert.That(cell2.Text, Is.EqualTo("=B2")); // Text should not change
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Exception Case: Tests referencing an invalid cell. This normally would cause an exception, but it has been handled.
        /// </summary>
        [Test]
        public void TestReferenceInvalidCell()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(2, 2);
            if (cell != null)
            {
                cell.Text = "=B20"; // out of range. Spreadsheet expects 1-based input
                Assert.That(cell.Value, Is.EqualTo(string.Empty)); // Value should be set to empty string
                Assert.That(cell.Text, Is.EqualTo("=B20")); // Text should not change
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Edge Case: Tests a cell references a cell that references a cell.
        /// </summary>
        [Test]
        public void TestReferenceMultipleCells()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(1, 1);
            Cell? cell2 = spreadsheet.GetCell(2, 2);
            Cell? cell3 = spreadsheet.GetCell(3, 3);
            if (cell != null && cell2 != null && cell3 != null)
            {
                cell.Text = "5";
                cell2.Text = "=B2";
                cell3.Text = "=C3 + 1";
                Assert.That(cell3.Value, Is.EqualTo("6"));
                Assert.That(cell3.Text, Is.EqualTo("=C3 + 1")); // Text should not change
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}