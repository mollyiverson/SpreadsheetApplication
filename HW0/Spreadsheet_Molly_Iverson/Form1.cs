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

namespace Spreadsheet_Molly_Iverson
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
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

            for (int i = 0; i < 50; i++)
            {
                this.dataGridView1.Rows.Add();
            }
        }
    }
}
