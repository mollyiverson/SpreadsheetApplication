// <copyright file="Form1.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using SpreadsheetEngine;

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
                    currentCell.Text = (string)this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }

                string msg = string.Format("Finished Editing Cell at {0}{1}", Convert.ToChar(e.ColumnIndex + 65), e.RowIndex + 1);
                this.Text = msg;
            }
        }


        /// <summary>
        /// Shows the text cell value to show when hovering over the cell.
        /// </summary>
        /// <param name="sender">The hovered over DataGridViewCell.</param>
        /// <param name="e">Occurs when the mouse is hovering over the cell.</param>
        //private void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
        //    {
        //        Cell? currentCell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);
        //        if (currentCell != null)
        //        {
        //            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = currentCell.Value;
        //        }
        //    }
        //}

        /// <summary>
        /// The Cell text goes back to its value once the mouse stops hovering over it.
        /// </summary>
        /// <param name="sender">The hovered over DataGridViewCell.</param>
        /// <param name="e">Occurs when the mouse tops hovering over the cell.</param>
        //private void DataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
        //    {
        //        Cell? currentCell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);
        //        if (currentCell != null)
        //        {
        //            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = currentCell.Value;
        //        }
        //    }
        //}
    }
}