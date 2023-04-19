// <copyright file="Form1.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using SpreadsheetEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Spreadsheet_Molly_Iverson
{
    /// <summary>
    /// Contains all interaction with the UI.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Represents the logic behind the UI spreadsheet.
        /// </summary>
        private Spreadsheet spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.spreadsheet = new Spreadsheet(50, 26);
            this.spreadsheet.CellPropertyChanged += this.Spreadsheet_PropertyChanged;
            this.InitializeDataGrid();

            this.undoMenuItem.Enabled = false;
            this.redoMenuItem.Enabled = false;
        }

        /// <summary>
        /// The event if any cell in the spreadsheet is changed.
        /// </summary>
        /// <param name="sender">Cell changed.</param>
        /// <param name="e">Cell property changed event.</param>
        private void Spreadsheet_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is Cell currentCell)
            {
                int row = currentCell.RowIndex;
                int column = currentCell.ColumnIndex;

                if (e.PropertyName == "Text")
                {
                    this.dataGridView1.Rows[row].Cells[column].Value = currentCell.Value;
                }
                else if (e.PropertyName == "Value")
                {
                    this.dataGridView1.Rows[row].Cells[column].Value = currentCell.Value;
                }
                else if (e.PropertyName == "Color")
                {
                    this.dataGridView1.Rows[row].Cells[column].Style.BackColor = Color.FromArgb((int)currentCell.Color);
                }
            }
        }

        /// <summary>
        /// Sets up the DataGrid by adding the columns and rows.
        /// </summary>
        private void InitializeDataGrid()
        {
            this.dataGridView1.Columns.Clear();

            for (int i = 0; i < 26; i++)
            {
                this.dataGridView1.Columns.Add("column" + i, char.ConvertFromUtf32(65 + i));
            }

            this.dataGridView1.Rows.Add(51);

            // Adds the row indices to the spreadsheet
            for (int i = 1; i < this.dataGridView1.Rows.Count; i++)
            {
                this.dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }

            foreach (DataGridViewColumn col in this.dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// The demo runs when the user presses this button.
        /// </summary>
        /// <param name="sender">The button.</param>
        /// <param name="e">Button is clicked event.</param>
        private void DemoButton_Click(object sender, EventArgs e)
        {
            Random random = new ();
            for (int i = 0; i < 50; i++)
            {
                Cell? currentCell = this.spreadsheet.GetCell(random.Next(0, 50), random.Next(0, 26));
                if (currentCell != null)
                {
                    currentCell.Text = "Hello World!";
                }
            }

            for (int i = 0; i < 50; i++)
            {
                Cell? currentCell = this.spreadsheet.GetCell(i, 1);
                if (currentCell != null)
                {
                    currentCell.Text = "This is cell B" + (i + 1);
                }
            }

            for (int i = 0; i < 50; i++)
            {
                Cell? currentCell = this.spreadsheet.GetCell(i, 0);
                if (currentCell != null)
                {
                    currentCell.Text = "=B" + (i + 1);
                }
            }
        }

        /// <summary>
        /// The event if any cell in the spreadsheet has started being edited by the user.
        /// </summary>
        /// <param name="sender">The selected DataGridViewCell.</param>
        /// <param name="e">Event that reflects that the current DataGridViewCell is being edited.</param>
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Cell? currentCell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);
                if (currentCell != null)
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = currentCell.Text;
                }

                string msg = string.Format("Editing Cell at {0}{1}", Convert.ToChar(e.ColumnIndex + 65), e.RowIndex + 1);
                this.Text = msg;
            }
        }

        /// <summary>
        /// The event if any cell in the spreadsheet is finished being edited by the user.
        /// </summary>
        /// <param name="sender">The selected DataGridViewCell.</param>
        /// <param name="e">Occurs when edit mode stops for the currently selected cell.</param>
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Cell? currentCell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);
                if (currentCell != null)
                {
                    // Add Undo Command
                    string oldText = currentCell.Text;
                    string newNext = (string)this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                    if (oldText != newNext)
                    {
                        this.spreadsheet.AddUndo(currentCell, oldText, newNext);

                        // Change text of undo button
                        this.undoMenuItem.Text = this.spreadsheet.PeekUndoStackName();
                        this.undoMenuItem.Enabled = true;
                    }

                    // Change the cell text
                    currentCell.Text = (string)this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = currentCell.Value;
                }

                string msg = string.Format("Finished Editing Cell at {0}{1}", Convert.ToChar(e.ColumnIndex + 65), e.RowIndex + 1);
                this.Text = msg;
            }
        }

        /// <summary>
        /// Changes the background color of selected cells with a color of the user's choosing.
        /// </summary>
        /// <param name="sender">The MenuStrip option.</param>
        /// <param name="e">The change color of cells button is pressed.</param>
        private void ChangeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedCells.Count >= 1)
            {
                if (this.colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    Color colorChoice = this.colorDialog1.Color;
                    uint colorToUINT = (uint)colorChoice.ToArgb();

                    // Arguments to make a new undo command
                    List<Cell> cellList = new List<Cell>();
                    List<uint> previousColors = new List<uint>();

                    bool colorsChanged = false;

                    for (int counter = 0; counter < this.dataGridView1.SelectedCells.Count; counter++)
                    {
                        Cell? currentCell = this.spreadsheet.GetCell(this.dataGridView1.SelectedCells[counter].RowIndex, this.dataGridView1.SelectedCells[counter].ColumnIndex);

                        if (currentCell != null)
                        {
                            cellList.Add(currentCell);
                            previousColors.Add(currentCell.Color);

                            if (currentCell.Color != colorChoice.ToArgb())
                            {
                                colorsChanged = true;
                            }

                            currentCell.Color = colorToUINT;
                        }
                    }

                    // It won't make an undo command if none of the colors actually changed. At least one cell has to have a color change.
                    if (colorsChanged)
                    {
                        // Add new Undo command
                        this.spreadsheet.AddUndo(cellList, previousColors, colorToUINT);

                        // Change text of undo button
                        this.undoMenuItem.Text = this.spreadsheet.PeekUndoStackName();
                        this.undoMenuItem.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Undoes the most recent command.
        /// </summary>
        /// <param name="sender">The MenuStrip undo option.</param>
        /// <param name="e">The undo button is pushed.</param>
        private void UndoTextChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.Undo();
            if (this.spreadsheet.GetUndoStackSize() == 0)
            {
                this.undoMenuItem.Enabled = false;
                this.undoMenuItem.Text = "Undo";
            }
            else
            {
                this.undoMenuItem.Enabled = true;
                this.undoMenuItem.Text = this.spreadsheet.PeekUndoStackName();
            }

            this.redoMenuItem.Enabled = true;
            this.redoMenuItem.Text = this.spreadsheet.PeekRedoStackName();
        }

        /// <summary>
        /// Redoes the most recent undone command.
        /// </summary>
        /// <param name="sender">The MenuStrip redo option.</param>
        /// <param name="e">The redo button is pushed.</param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.Redo();
            if (this.spreadsheet.GetRedoStackSize() == 0)
            {
                this.redoMenuItem.Enabled = false;
                this.redoMenuItem.Text = "Redo";
            }
            else
            {
                this.redoMenuItem.Enabled = true;
                this.redoMenuItem.Text = this.spreadsheet.PeekRedoStackName();
            }

            this.undoMenuItem.Enabled = true;
            this.undoMenuItem.Text = this.spreadsheet.PeekUndoStackName();
        }

        /// <summary>
        /// Saves the spreasheet data to an XML file.
        /// </summary>
        /// <param name="sender">The MenuStrip save option.</param>
        /// <param name="e">The save button is pressed.</param>
        private void SaveSpreadsheet_Click(object sender, EventArgs e)
        {
            Stream fileStream;
            this.saveFileDialog1.InitialDirectory = AppContext.BaseDirectory;

            // this.saveFileDialog1.InitialDirectory = AppContext.BaseDirectory;
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((fileStream = this.saveFileDialog1.OpenFile()) != null)
                {
                    this.spreadsheet.SaveToXML(fileStream);
                    fileStream.Close();
                }
            }
        }

        /// <summary>
        /// Loads spreadsheet data from an XML file.
        /// </summary>
        /// <param name="sender">The MenuStrip load option.</param>
        /// <param name="e">The load button is pressed.</param>
        private void LoadSpreadsheet_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.InitialDirectory = AppContext.BaseDirectory;

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream fileStream = this.openFileDialog1.OpenFile();
                this.spreadsheet.LoadFromXML(fileStream);
                fileStream.Close();
            }

            // Redo and Undo stacks are cleared when a new spreadsheet is loaded
            if (this.spreadsheet.GetRedoStackSize() == 0 && this.spreadsheet.GetUndoStackSize() == 0)
            {
                this.redoMenuItem.Enabled = false;
                this.redoMenuItem.Text = "Redo";
                this.undoMenuItem.Enabled = false;
                this.undoMenuItem.Text = "Undo";
            }
            else
            {
                throw new Exception("Undo and redo stack should have been cleared.");
            }
        }

        /// <summary>
        /// Clears the spreadsheet data.
        /// </summary>
        /// <param name="sender">The Menustrip clear spreadsheet item.</param>
        /// <param name="e">Menustrip clear spreadsheet option is pressed.</param>
        private void ClearSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.ClearSpreadsheet();

            if (this.spreadsheet.GetRedoStackSize() == 0 && this.spreadsheet.GetUndoStackSize() == 0)
            {
                this.redoMenuItem.Enabled = false;
                this.redoMenuItem.Text = "Redo";
                this.undoMenuItem.Enabled = false;
                this.undoMenuItem.Text = "Undo";
            }
            else
            {
                throw new Exception("Undo and redo stack should have been cleared.");
            }
        }
    }
}