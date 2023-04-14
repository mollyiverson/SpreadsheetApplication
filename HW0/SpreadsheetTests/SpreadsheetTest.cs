// <copyright file="SpreadsheetTest.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>
using System.Drawing;
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
                Assert.Multiple(() =>
                {
                    Assert.That(cell2.Value, Is.EqualTo(string.Empty));
                    Assert.That(cell2.Text, Is.EqualTo("=B2")); // Text should not change
                });
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
                Assert.Multiple(() =>
                {
                    Assert.That(cell.Value, Is.EqualTo(string.Empty)); // Value should be set to empty string
                    Assert.That(cell.Text, Is.EqualTo("=B20")); // Text should not change
                });
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
                Assert.Multiple(() =>
                {
                    Assert.That(cell3.Value, Is.EqualTo("6"));
                    Assert.That(cell3.Text, Is.EqualTo("=C3 + 1")); // Text should not change
                });
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Exception Case: Tests referencing a cell with a string(words) and attempts to perform an arithmetic operation on it.
        /// This normally would cause an exception, but it has been handled.
        /// </summary>
        [Test]
        public void TestAddStringCell()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(1, 1);
            Cell? cell2 = spreadsheet.GetCell(2, 2);
            if (cell != null && cell2 != null)
            {
                cell.Text = "Hello";
                cell2.Text = "=B2 + 1";
                Assert.Multiple(() =>
                {
                    Assert.That(cell2.Value, Is.EqualTo(string.Empty));
                    Assert.That(cell2.Text, Is.EqualTo("=B2 + 1")); // Text should not change
                });
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests the undo function.
        /// </summary>
        [Test]
        public void TestUndo()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(1, 1);
            if (cell != null)
            {
                cell.Text = "Hello";
                spreadsheet.AddUndo(cell, string.Empty, "Hello");
                spreadsheet.Undo();
                Assert.Multiple(() =>
                {
                    Assert.That(cell.Text, Is.EqualTo(string.Empty));
                    Assert.That(spreadsheet.GetRedoStackSize(), Is.EqualTo(1));
                });
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests the redo function.
        /// </summary>
        [Test]
        public void TestRedo()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(1, 1);
            if (cell != null)
            {
                cell.Text = "Hello";

                // Undo
                spreadsheet.AddUndo(cell, string.Empty, "Hello");
                spreadsheet.Undo();

                // Redo
                spreadsheet.Redo();
                Assert.That(cell.Text, Is.EqualTo("Hello"));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests undo and redo with the Color property.
        /// </summary>
        [Test]
        public void TestColorUndoRedo()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(1, 1);
            if (cell != null)
            {
                cell.Color = 4294934783; // pink

                List<Cell> cellList = new List<Cell> { cell };
                List<uint> colorList = new List<uint> { 0xFFFFFFFF };

                // Undo
                spreadsheet.AddUndo(cellList, colorList, 4294934783);
                spreadsheet.Undo();
                Assert.That(cell.Color, Is.EqualTo(0xFFFFFFFF));

                // Redo
                spreadsheet.Redo();
                Assert.That(cell.Color, Is.EqualTo(4294934783));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests undo, the redo, then undo again.
        /// </summary>
        [Test]
        public void TestUndoRedoUndo()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(1, 1);
            if (cell != null)
            {
                cell.Text = "Hello";

                // Undo
                spreadsheet.AddUndo(cell, string.Empty, "Hello");
                spreadsheet.Undo();

                // Redo
                spreadsheet.Redo();

                // Undo again
                spreadsheet.Undo();
                Assert.That(cell.Text, Is.EqualTo(string.Empty));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests the Save and Load functionality.
        /// </summary>
        [Test]
        public void TestSaveAndLoad()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);
            Cell? cell = spreadsheet.GetCell(1, 1);
            if (cell != null)
            {
                cell.Text = "Hello";

                // SAVE SPREADSHEET
                cell.Text = "Goodbye";

                // LOAD SPREADSHEET
                Assert.That(cell.Text, Is.EqualTo("Hello"));
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests loading a spreadsheet from an XML file with extra tags.
        /// </summary>
        [Test]
        public void TestLoadExtraTagsXML()
        {
            Spreadsheet spreadsheet = new Spreadsheet(5, 5);

            string filename = "ExtraTags.xml";
            string path = Path.Combine(AppContext.BaseDirectory, filename);

            FileStream f = File.OpenRead(path);

            spreadsheet.LoadFromXML(f);

            Cell? cell1 = spreadsheet.GetCell(0, 1); // B1
            Cell? cell2 = spreadsheet.GetCell(0, 0); // A1
            if (cell1 != null && cell2 != null)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(cell1.Text, Is.EqualTo("hello"));
                    Assert.That(cell1.Color, Is.EqualTo(Convert.ToUInt32("FFFF80C0", 16)));
                    Assert.That(cell2.Text, Is.EqualTo("=8"));
                    Assert.That(cell2.Color, Is.EqualTo(Convert.ToUInt32("FF8000FF", 16)));
                });
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}