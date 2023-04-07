// <copyright file="ColorChangeCommand.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Represents the user's command to change the color of one or more cells.
    /// </summary>
    internal class ColorChangeCommand : ICommand
    {
        /// <summary>
        /// The message for the UI when this command is on top of the redo stack.
        /// </summary>
        private const string RedoMessage = "Redo background color change";

        /// <summary>
        /// The message for the UI when this command is on top of the undo stack.
        /// </summary>
        private const string UndoMessage = "Undo background color change";

        /// <summary>
        /// The list of cells whose color changed.
        /// </summary>
        private List<Cell> cells;

        /// <summary>
        /// The list of previous colors of the cells whose color changed.
        /// </summary>
        private List<uint> previousColors;

        /// <summary>
        /// The new color of the cells.
        /// </summary>
        private uint newColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChangeCommand"/> class.
        /// </summary>
        /// <param name="cells">The list of cells whose color changed.</param>
        /// <param name="previousColors">The previous colors of the cells.</param>
        /// <param name="newColor">The new color of the cells.</param>
        public ColorChangeCommand(List<Cell> cells, List<uint> previousColors, uint newColor)
        {
            this.cells = cells;
            this.previousColors = previousColors;
            this.newColor = newColor;
        }

        /// <summary>
        /// Changes the color of the cells to the new color.
        /// </summary>
        public void Execute()
        {
            foreach (Cell cell in this.cells)
            {
                cell.Color = this.newColor;
            }
        }

        /// <summary>
        /// Changes the cell's colors back to their original colors.
        /// </summary>
        public void UnExecute()
        {
            for (int i = 0; i < this.cells.Count; i++)
            {
                this.cells[i].Color = this.previousColors[i];
            }
        }

        /// <summary>
        /// Gets the Redo message.
        /// </summary>
        /// <returns>Redo message.</returns>
        public string GetRedoMessage()
        {
            return RedoMessage;
        }

        /// <summary>
        /// Gets the Undo message.
        /// </summary>
        /// <returns>The undo message.</returns>
        public string GetUndoMessage()
        {
            return UndoMessage;
        }
    }
}
