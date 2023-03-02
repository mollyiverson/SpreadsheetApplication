// <copyright file="Form1.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // HAVING ISSUE HERE: Can't instantiate spreadsheet without an error
            this.spreadsheet = new Spreadsheet(50, 50);
            //this.spreadsheet.CellPropertyChanged += this.Spreadsheet_PropertyChanged;
            this.InitializeDataGrid();
        }

        private void Spreadsheet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                this.dataGridView1[3, 3].Value = "HELLO";
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

            for (int i = 0; i < 26; i++)
            {
                this.dataGridView1.Columns.Add("column" + (i + 26), "A" + char.ConvertFromUtf32(65 + i));
            }

            this.dataGridView1.Rows.Add(51);

            // Adds the row indices to the spreadsheet
            for (int i = 1; i < this.dataGridView1.Rows.Count; i++)
            {
                this.dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }
        }
    }
}
