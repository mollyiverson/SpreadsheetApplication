// <copyright file="Cell.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Represents one cell in the spreadsheet.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex">The cell's row in the spreadsheet.</param>
        /// <param name="columnIndex">The cell's column in the spreadsheet.</param>
        public Cell(int rowIndex, int columnIndex)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
            this.Text = string.Empty;
            this.Value = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        /// <summary>
        /// Gets the row index.
        /// </summary>
        public int RowIndex
        {
            get;
        }

        /// <summary>
        /// Gets the column index.
        /// </summary>
        public int ColumnIndex
        {
            get;
        }

        /// <summary>
        /// Gets or sets the text inside the Cell.
        /// </summary>
        public string Text
        {
            get
            {
                return this.Text;
            }

            set
            {
                if (value == this.Text)
                {
                    return;
                }

                this.Text = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }

        /// <summary>
        /// Gets the evaluated value of the Cell.
        /// </summary>
        public string Value
        {
            get;
        }
    }
}