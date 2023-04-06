using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Represents the user's command to change the text of a cell.
    /// </summary>
    internal class TextChangeCommand : ICommand
    {
        /// <summary>
        /// The message for the UI when this command is on top of the redo stack.
        /// </summary>
        private const string RedoMessage = "Redo cell text change";

        /// <summary>
        /// The message for the UI when this command is on top of the undo stack.
        /// </summary>
        private const string UndoMessage = "Undo cell text change";

        /// <summary>
        /// The cell whose text has changed.
        /// </summary>
        private Cell cell;

        /// <summary>
        /// The old text value of the cell.
        /// </summary>
        private string oldText;

        /// <summary>
        /// The new text of the cell.
        /// </summary>
        private string newText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextChangeCommand"/> class.
        /// </summary>
        /// <param name="cell">The cell changed.</param>
        /// <param name="oldText">The old cell text.</param>
        /// <param name="newText">The new cell text.</param>
        public TextChangeCommand(Cell cell, string oldText, string newText)
        {
            this.cell = cell;
            this.oldText = oldText;
            this.newText = newText;
        }

        /// <summary>
        /// Changes the text of the cell to the new text.
        /// </summary>
        public void Execute()
        {
            this.cell.Text = this.newText;
        }

        /// <summary>
        /// Changes the text of the cell to the old text.
        /// </summary>
        public void UnExecute()
        {
            this.cell.Text = this.oldText;
        }

        /// <summary>
        /// Returns the redo message.
        /// </summary>
        /// <returns>Redo message.</returns>
        public string GetRedoMessage()
        {
            return RedoMessage;
        }

        /// <summary>
        /// Returns the undo message.
        /// </summary>
        /// <returns>Undo message.</returns>
        public string GetUndoMessage()
        {
            return UndoMessage;
        }
    }
}
