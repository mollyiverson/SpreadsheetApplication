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
        /// The Cell's row index in the spreadsheet.
        /// </summary>
        protected readonly int rowIndex;

        /// <summary>
        /// The Cell's column index in the spreadsheet.
        /// </summary>
        protected readonly int columnIndex;

        /// <summary>
        /// The text inside the spreadsheet cell.
        /// </summary>
        protected string text;

        /// <summary>
        /// Represents the “evaluated” value of the cell. It will just be the Text property if the
        /// text doesn’t start with the ‘=’ character.
        /// </summary>
        protected string value;

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex">The cell's row in the spreadsheet.</param>
        /// <param name="columnIndex">The cell's column in the spreadsheet.</param>
        /// <param name="text">The data inside the cell.</param>
        public Cell(int rowIndex, int columnIndex, string text)
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.text = text;
        }

        /// <summary>
        /// Gets the row index.
        /// </summary>
        public int RowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets the column index.
        /// </summary>
        public int ColumnIndex
        {
            get { return this.columnIndex; }
        }

        /// <summary>
        /// Gets or sets the text inside the Cell.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (value == this.text)
                {
                    return;
                }

                this.text = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }

        /// <summary>
        /// Gets the evaluated value of the Cell.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }
        }
    }
}