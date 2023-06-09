﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        /// Used to keep track of actions taken.
        /// </summary>
        private Stack<ICommand> undoStack;

        /// <summary>
        /// Used to keep track of actions undone.
        /// </summary>
        private Stack<ICommand> redoStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// Creates the spreadsheet with specfied dimensions.
        /// </summary>
        /// <param name="rows">The number of rows in the spreadsheet.</param>
        /// <param name="columns">The number of columns in the spreadsheet.</param>
        public Spreadsheet(int rows, int columns)
        {
            this.expressionTree = new ExpressionTree(string.Empty, this.GetCellValueFromName);
            this.undoStack = new Stack<ICommand>();
            this.redoStack = new Stack<ICommand>();
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
        /// Clears all data from the spreadsheet.
        /// </summary>
        public void ClearSpreadsheet()
        {
            for (int i = 0; i < this.rowCount; i++)
            {
                for (int j = 0; j < this.columnCount; j++)
                {
                    this.cellArray[i, j].Text = string.Empty;
                    this.cellArray[i, j].Color = 0xFFFFFFFF;
                    this.cellArray[i, j].ClearList();
                }
            }

            this.ClearRedoStack();
            this.ClearUndoStack();
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
            if (row >= this.RowCount || column >= this.ColumnCount || row < 0 || column < 0)
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
                Cell? cell = this.GetCell(cellName);

                if (cell != null)
                {
                    dependentCells.Add(cell);
                }
            }

            return dependentCells;
        }

        /// <summary>
        /// Adds a text change command to the UndoStack.
        /// </summary>
        /// <param name="cell">The cell whose text changed.</param>
        /// <param name="oldText">The old text of the cell.</param>
        /// <param name="newText">The new text of the cell.</param>
        public void AddUndo(Cell cell, string oldText, string newText)
        {
            TextChangeCommand textChange = new TextChangeCommand(cell, oldText, newText);
            this.undoStack.Push(textChange);
        }

        /// <summary>
        /// Adds a color change command to the UndoStack.
        /// </summary>
        /// <param name="newColorCells">The cells whose color changed.</param>
        /// <param name="previousColors">The old colors of the cells.</param>
        /// <param name="color">The new color of the cells.</param>
        public void AddUndo(List<Cell> newColorCells, List<uint> previousColors, uint color)
        {
            ColorChangeCommand colorChange = new ColorChangeCommand(newColorCells, previousColors, color);
            this.undoStack.Push(colorChange);
        }

        /// <summary>
        /// Executes the last command undone and adds it to the undo stack.
        /// </summary>
        public void Redo()
        {
            if (this.redoStack.Count > 0)
            {
                ICommand lastCommand = this.redoStack.Pop();
                this.undoStack.Push(lastCommand);
                lastCommand.Execute();
            }
        }

        /// <summary>
        /// Executes the last command and adds it to the redo stack.
        /// </summary>
        public void Undo()
        {
            if (this.undoStack.Count > 0)
            {
                ICommand lastCommand = this.undoStack.Pop();
                this.redoStack.Push(lastCommand);
                lastCommand.UnExecute();
            }
        }

        /// <summary>
        /// Returns the size of the redo stack.
        /// </summary>
        /// <returns>Size of redo stack.</returns>
        public int GetRedoStackSize()
        {
            return this.redoStack.Count;
        }

        /// <summary>
        /// Returns the size of the undo stack.
        /// </summary>
        /// <returns>Size of undo stack.</returns>
        public int GetUndoStackSize()
        {
            return this.undoStack.Count;
        }

        /// <summary>
        /// Gets the undo message of the top element of the undo stack.
        /// </summary>
        /// <returns>The undo message.</returns>
        public string PeekUndoStackName()
        {
            if (this.undoStack.Count > 0)
            {
                return this.undoStack.Peek().GetUndoMessage();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the undo message of the top element of the redo stack.
        /// </summary>
        /// <returns>The redo message.</returns>
        public string PeekRedoStackName()
        {
            if (this.redoStack.Count > 0)
            {
                return this.redoStack.Peek().GetRedoMessage();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Clears the redo stack.
        /// </summary>
        public void ClearRedoStack()
        {
            this.redoStack.Clear();
        }

        /// <summary>
        /// Clears the undo stack.
        /// </summary>
        public void ClearUndoStack()
        {
            this.undoStack.Clear();
        }

        /// <summary>
        /// Loads an XML file and converts the data to a new spreasheet.
        /// </summary>
        /// <param name="stream">The XML file.</param>
        public void LoadFromXML(Stream stream)
        {
            this.ClearRedoStack();
            this.ClearUndoStack();
            this.ClearSpreadsheet();

            XmlReaderSettings settings = new XmlReaderSettings()
            {
                IgnoreWhitespace = true,
                IgnoreComments = true,
            };

            XmlReader reader = XmlReader.Create(stream, settings);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    // if it is a cell, read its data
                    if (reader.Name == "cell")
                    {
                        string? cellName = reader.GetAttribute("name");
                        if (cellName != null)
                        {
                            Cell? newCell = this.GetCell(cellName);

                            if (newCell != null)
                            {
                                reader.Read();
                                while (reader.ReadState != ReadState.EndOfFile && reader.Name != "cell")
                                {
                                    // Read the text element
                                    if (reader.Name == "text")
                                    {
                                        reader.Read();
                                        string text = reader.Value;
                                        newCell.Text = text;
                                        if (text != string.Empty)
                                        {
                                            reader.Read();
                                        }

                                        reader.Read();
                                    }

                                    // Read the color element
                                    else if (reader.Name == "bgcolor")
                                    {
                                        string color = reader.ReadElementContentAsString();
                                        uint result = Convert.ToUInt32(color, 16);
                                        newCell.Color = result;
                                    }
                                    else
                                    {
                                        reader.Read();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves the spreadsheet data into an XML file.
        /// </summary>
        /// <param name="stream">The file to be saved to.</param>
        public void SaveToXML(Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings();

            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                writer.WriteStartElement("spreadsheet");
                foreach (var cell in this.cellArray)
                {
                    if (cell.HasCellBeenChanged())
                    {
                        // if cell isn't default
                        writer.WriteStartElement("cell");

                        int row = cell.RowIndex;
                        int col = cell.ColumnIndex + 'A';
                        string cellName = ((char)col).ToString() + (row + 1).ToString();

                        writer.WriteAttributeString("name", cellName);

                        writer.WriteStartElement("bgcolor");
                        writer.WriteString(cell.Color.ToString("X8"));
                        writer.WriteEndElement();

                        writer.WriteStartElement("text");
                        writer.WriteString(cell.Text);
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Retrieves the cell using the name of the cell (i.e. A3).
        /// </summary>
        /// <param name="cellName">Name of cell.</param>
        /// <returns>The cell.</returns>
        private Cell? GetCell(string cellName)
        {
            try
            {
                char columnLetter = cellName[0];

                // Get number of rows as a substring
                string rows = cellName.Substring(1);

                // Convert
                int columnIndex = columnLetter - 65;
                int rowIndex = int.Parse(rows) - 1;

                if ((columnIndex >= 0 && columnIndex < this.columnCount) && (rowIndex >= 0 && rowIndex < this.rowCount))
                {
                    return this.cellArray[rowIndex, columnIndex];
                }
                else
                {
                    throw new Exception("Variable not compatible with Spreadsheet (Columns A-Z, rows 1-50)");
                }
            }
            catch
            {
                // bad format of variable. Not compatible with spreadsheet
                throw new Exception("Variable not compatible with Spreadsheet (Columns A-Z, rows 1-50)");
            }
        }

        /// <summary>
        /// This function will be referenced in the ExpressionTree class when it needs to
        /// access a variable.
        /// </summary>
        /// <param name="cellName">The Cell name (i.e. B3).</param>
        /// <returns>The evaluated value of the expression/cell.</returns>
        /// <exception cref="Exception">Throws an exception if GetCell is called with out of range data.</exception>
        private double GetCellValueFromName(string cellName)
        {
            try
            {
                Cell? currentCell = this.GetCell(cellName);

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
                    catch (System.FormatException)
                    {
                        // referencing an empty cell returns 0
                        return 0;
                    }
                }
            }
            catch
            {
                // invalid cell -> bad reference error
                return 0;
            }
        }

        /// <summary>
        /// The event where a cell in the spreadsheet is modified.
        /// </summary>
        /// <param name="sender">Cell changed.</param>
        /// <param name="e">Event where cell is changed.</param>
        private void SCell_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is SCell currentCell)
            {
                if (e.PropertyName == "Text")
                {
                    string cellText = currentCell.Text;
                    currentCell.ClearList();
                    if (cellText == null || cellText == string.Empty)
                    {
                        currentCell.Value = string.Empty;
                    }
                    else if (cellText[0] != '=')
                    {
                        // simple assignment to a constant or a string
                        currentCell.Value = cellText;
                    }
                    else
                    {
                        // longer expressions with or without cells
                        string expression = cellText.Substring(1);
                        this.expressionTree.Expression = expression;
                        try
                        {
                            double value = this.expressionTree.Evaluate(); // throws ArgumentNullException if an invalid cell is referenced
                            currentCell.DependentCells = this.GetDependentCells();

                            // one of the cells in the expression is itself
                            if (currentCell.DependentCells.Contains(currentCell))
                            {
                                currentCell.ClearList();
                                currentCell.SetSelfReference(cellText);
                                this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));
                            }
                            else
                            {
                                currentCell.Subscribe();
                                currentCell.Value = value + string.Empty;
                            }
                        }
                        catch
                        {
                            // a nonempty/nonvalid cell is referenced
                            currentCell.SetBadReference(cellText);
                            this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));
                        }
                    }
                }
                else if (e.PropertyName == "Value")
                {
                    if (currentCell.Value == "!(circular reference)")
                    {
                        // When checking for circular references, the variables in the chain changed values.
                        // This resets the variables to their previous values.
                        if (this.undoStack.Count > 0)
                        {
                            ICommand lastCommand = this.undoStack.Peek();
                            string oldText = currentCell.Text;
                            List<Cell> dependentCells = currentCell.DependentCells;
                            List<Cell> copiedList = dependentCells.Select(x => x).ToList();
                            lastCommand.UnExecute();
                            currentCell.SetCircularReference(oldText);
                            currentCell.DependentCells = copiedList;
                            currentCell.Subscribe();
                        }
                    }

                    this.CellPropertyChanged(sender, e);
                }
                else if (e.PropertyName == "Color")
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
            if (sender is SCell currentCell)
            {
                string cellText = currentCell.Text;
                currentCell.ClearList();
                if (cellText[0] != '=')
                {
                    // simple constant or string assignment
                    currentCell.Value = cellText;
                }
                else
                {
                    // expressions
                    string expression = cellText.Substring(1);
                    this.expressionTree.Expression = expression;
                    try
                    {
                        double value = this.expressionTree.Evaluate(); // Can throw an ArgumentNullException if an invalid cell is referenced
                        currentCell.DependentCells = this.GetDependentCells();
                        currentCell.Subscribe();
                        currentCell.Value = value + string.Empty;
                    }
                    catch
                    {
                        // This error should never be reached because invalid cell checks are done when the cell is initially edited
                        throw new Exception("Error: The referenced cells shouldn't have issues with invalid cells.");
                    }
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
            /// Indicates whether the cell value has just been changed.
            /// </summary>
            private bool valueJustChanged;

            /// <summary>
            /// Initializes a new instance of the <see cref="SCell"/> class.
            /// </summary>
            /// <param name="rowIndex">The row index of the cell.</param>
            /// <param name="columnIndex">The column index of the cell.</param>
            public SCell(int rowIndex, int columnIndex)
                : base(rowIndex, columnIndex)
            {
                this.dependentCells = new List<Cell>();
                this.valueJustChanged = false;
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
                    if (this.valueJustChanged)
                    {
                        this.value = "!(circular reference)";
                        this.valueJustChanged = false;
                    }
                    else
                    {
                        if (this.value == value)
                        {
                            this.valueJustChanged = true;
                            this.OnCellChangedReferencedCells(new PropertyChangedEventArgs("Value"));
                            this.valueJustChanged = false;
                            return;
                        }

                        this.value = value;
                        this.valueJustChanged = true;
                        this.OnCellChangedReferencedCells(new PropertyChangedEventArgs("Value"));
                        base.OnCellChanged(new PropertyChangedEventArgs("Value"));
                        this.valueJustChanged = false;
                    }
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
            /// Gets or sets the color of the cell.
            /// </summary>
            public override uint Color
            {
                get
                {
                    return this.color;
                }

                set
                {
                    if (this.color == value)
                    {
                        return;
                    }

                    this.color = value;
                    base.OnCellChanged(new PropertyChangedEventArgs("Color"));
                }
            }

            /// <summary>
            /// Subscribes the current Cell to any property changes of its dependent cells.
            /// </summary>
            public void Subscribe()
            {
                foreach (var cell in this.dependentCells)
                {
                    cell.PropertyChangedForDependents += this.OtherCell_PropertyChanged;
                }
            }

            /// <summary>
            /// Unsubscribes the current Cell from any property changes of its dependent cells.
            /// </summary>
            public void ClearList()
            {
                foreach (var cell in this.dependentCells)
                {
                    cell.PropertyChangedForDependents -= this.OtherCell_PropertyChanged;
                }

                this.dependentCells.Clear();
            }

            /// <summary>
            /// Sets the value of the cell to the circular reference error message without calling
            /// other cells that reference this cell.
            /// </summary>
            /// <param name="oldText">The text of the cell that causes this error.</param>
            public void SetCircularReference(string oldText)
            {
                this.value = "!(circular reference)";
                this.text = oldText;
            }

            /// <summary>
            /// Sets the value of the cell to the self reference error message without calling
            /// other cells that reference this cell.
            /// </summary>
            /// <param name="oldText">The text of the cell that causes this error.</param>
            public void SetSelfReference(string oldText)
            {
                this.value = "!(self reference)";
                this.text = oldText;
            }

            /// <summary>
            /// Sets the value of the cell to the bad reference error message without calling
            /// other cells that reference this cell.
            /// </summary>
            /// <param name="oldText">The text of the cell that causes this error.</param>
            public void SetBadReference(string oldText)
            {
                this.value = "!(bad reference)";
                this.text = oldText;
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
                    if (sender is Cell currentCell)
                    {
                        this.DependentCellPropertyChanged.Invoke(this, new EventArgs());
                    }
                }
            }
        }
    }
}
