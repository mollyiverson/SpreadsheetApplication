// <copyright file="Spreadsheet.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Serves as the container for the 2D array of Cells.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// The 2D array of Cells that represents the spreadsheet.
        /// </summary>
        private SCell[,] cellArray;

        /// <summary>
        /// The number of rows in the spreadsheet.
        /// </summary>
        private int rowCount;

        /// <summary>
        /// The number of columns in the spreadsheet.
        /// </summary>
        private int columnCount;

        /// <summary>
        /// Used to evaluate the formulas in the cells.
        /// </summary>
        private ExpressionTree expressionTree;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// Creates the spreadsheet with specfied dimensions.
        /// </summary>
        /// <param name="rows">The number of rows in the spreadsheet.</param>
        /// <param name="columns">The number of columns in the spreadsheet.</param>
        public Spreadsheet(int rows, int columns)
        {
            this.expressionTree = new ExpressionTree(string.Empty, this.GetCellValue);
            this.rowCount = rows;
            this.columnCount = columns;
            this.cellArray = new SCell[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.cellArray[i, j] = new SCell(i, j);
                    this.cellArray[i, j].PropertyChanged += this.SCell_PropertyChanged;
                    this.cellArray[i, j].DependentCellPropertyChanged += this.SCell_Maybe_PropertyChanged;
                }
            }
        }

        /// <summary>
        /// This event handler notifies the Form when a cell has changed.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged = delegate { };

        /// <summary>
        /// Gets the number of rows in the spreadsheet.
        /// </summary>
        public int RowCount
        {
            get { return this.rowCount; }
        }

        /// <summary>
        /// Gets the number of columns in the spreadsheet.
        /// </summary>
        public int ColumnCount
        {
            get { return this.columnCount; }
        }

        /// <summary>
        /// This function will be referenced in the ExpressionTree class when it needs to
        /// access a variable.
        /// </summary>
        /// <param name="cellName">The Cell name (i.e. B3).</param>
        /// <returns>The evaluated value of the expression/cell.</returns>
        /// <exception cref="Exception">Throws an exception if GetCell is called with out of range data.</exception>
        public double GetCellValue(string cellName)
        {
            // Get the letter column
            char columnLetter = cellName[0];

            // Get number of rows as a substring
            string rows = cellName.Substring(1);

            // Convert
            int columnIndex = columnLetter - 65;
            int rowIndex = int.Parse(rows) - 1;

            Cell? currentCell = this.GetCell(rowIndex, columnIndex);
            if (currentCell == null)
            {
                throw new Exception("GetCell: requesting a cell out of range.");
            }
            else
            {
                string cellValue = currentCell.Value;
                try
                {
                    double answer = Convert.ToDouble(cellValue);
                    return answer;
                }
                catch (System.FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new Exception("Cannot reference an empty cell.");
                }
            }
        }

        /// <summary>
        /// Takes a row and column index and returns the cell at that location
        /// or null if there is no such cell.
        /// </summary>
        /// <param name="row">The row in the spreadsheet where the cell is.</param>
        /// <param name="column">The column in the spreadsheet where the cell is.</param>
        /// <returns>A cell in the spreadsheet.</returns>
        public Cell? GetCell(int row, int column)
        {
            if (row >= this.RowCount || column >= this.ColumnCount)
            {
                return null;
            }

            return this.cellArray[row, column];
        }

        /// <summary>
        /// Takes a list of variables in the expression and converts it into a list of Cells.
        /// </summary>
        /// <returns>A list of Cells.</returns>
        public List<Cell> GetDependentCells()
        {
            List<string> cells = this.expressionTree.GetVariables();
            List<Cell> dependentCells = new List<Cell>();
            foreach (string cellName in cells)
            {
                char columnLetter = cellName[0];

                // Get number of rows as a substring
                string rows = cellName.Substring(1);

                // Convert
                int columnIndex = columnLetter - 65;
                int rowIndex = int.Parse(rows) - 1;

                dependentCells.Add(this.cellArray[rowIndex, columnIndex]);
            }

            return dependentCells;
        }

        /// <summary>
        /// The event where a cell in the spreadsheet is modified.
        /// </summary>
        /// <param name="sender">Cell changed.</param>
        /// <param name="e">Event where cell is changed.</param>
        private void SCell_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            SCell? currentCell = sender as SCell;

            if (currentCell != null)
            {
                if (e.PropertyName == "Text")
                {
                    if (currentCell.Text[0] != '=')
                    {
                        currentCell.Value = currentCell.Text;
                    }
                    else
                    {
                        string expression = currentCell.Text.Substring(1);
                        this.expressionTree.Expression = expression;
                        double value = this.expressionTree.Evaluate();
                        currentCell.ClearList();
                        currentCell.DependentCells = this.GetDependentCells();
                        currentCell.Subscribe();
                        currentCell.Value = value + string.Empty;
                    }
                }
                else if (e.PropertyName == "Value")
                {
                    this.CellPropertyChanged(sender, e);
                }
            }
        }

        /// <summary>
        /// The event where a dependent cell in the spreadsheet is modifed, and other cells that reference it call this event to check
        /// if they need to change.
        /// </summary>
        /// <param name="sender">Cell referencing a changed cell.</param>
        /// <param name="e">Event where a refenced cell has changed.</param>
        private void SCell_Maybe_PropertyChanged(object? sender, EventArgs e)
        {
            SCell? currentCell = sender as SCell;

            if (currentCell != null)
            {
                if (currentCell.Text[0] != '=')
                {
                    currentCell.Value = currentCell.Text;
                }
                else
                {
                    string expression = currentCell.Text.Substring(1);
                    this.expressionTree.Expression = expression;
                    double value = this.expressionTree.Evaluate();
                    currentCell.Value = value + string.Empty;
                }
            }
        }

        /// <summary>
        /// Represents a cell in the spreadsheet. This is a concrete derived class of the abstract
        /// Cell class.
        /// </summary>
        private class SCell : Cell
        {
            /// <summary>
            /// All cells referenced in the Cell's text expression.
            /// </summary>
            private List<Cell> dependentCells;

            /// <summary>
            /// Initializes a new instance of the <see cref="SCell"/> class.
            /// </summary>
            /// <param name="rowIndex">The row index of the cell.</param>
            /// <param name="columnIndex">The column index of the cell.</param>
            public SCell(int rowIndex, int columnIndex)
                : base(rowIndex, columnIndex)
            {
                this.dependentCells = new List<Cell>();
            }

            /// <summary>
            /// This event handler notifies the Spreadsheet when a dependent cell has changed and this cell value may change too.
            /// </summary>
            public event EventHandler DependentCellPropertyChanged = delegate { };

            /// <summary>
            /// Gets or sets the dependent cells list.
            /// </summary>
            public List<Cell> DependentCells
            {
                get { return this.dependentCells; }
                set { this.dependentCells = value; }
            }

            /// <summary>
            /// Gets or sets the evaluated value of the Cell.
            /// </summary>
            public new string Value
            {
                get
                {
                    return this.value;
                }

                set
                {
                    if (this.value == value)
                    {
                        return;
                    }

                    this.value = value;
                    base.OnCellChanged(new PropertyChangedEventArgs("Value"));
                }
            }

            /// <summary>
            /// Gets or sets the text inside the Cell.
            /// </summary>
            public override string Text
            {
                get
                {
                    return this.text;
                }

                set
                {
                    if (this.text == value)
                    {
                        return;
                    }

                    this.text = value;
                    base.OnCellChanged(new PropertyChangedEventArgs("Text"));
                }
            }

            /// <summary>
            /// Subscribes the current Cell to any property changes of its dependent cells.
            /// </summary>
            public void Subscribe()
            {
                foreach (var cell in this.dependentCells)
                {
                    cell.PropertyChanged += this.OtherCell_PropertyChanged;
                }
            }

            /// <summary>
            /// Unsubscribes the current Cell from any property changes of its dependent cells.
            /// </summary>
            public void ClearList()
            {
                foreach (var cell in this.dependentCells)
                {
                    cell.PropertyChanged -= this.OtherCell_PropertyChanged;
                }

                this.dependentCells.Clear();
            }

            /// <summary>
            /// Calls the base class event, notifying that either the Value or Text of a Cell has changed.
            /// </summary>
            /// <param name="e">The event of either the Text of Value of a cell changing.</param>
            protected override void OnCellChanged(PropertyChangedEventArgs e)
            {
                base.OnCellChanged(e);
            }

            /// <summary>
            /// Handles if a referenced cell changes.
            /// </summary>
            /// <param name="sender">The referenced cell that changed.</param>
            /// <param name="e">A cell's changed property.</param>
            private void OtherCell_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Value")
                {
                    this.DependentCellPropertyChanged.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
