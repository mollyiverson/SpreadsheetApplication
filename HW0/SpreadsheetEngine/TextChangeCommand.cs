using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    internal class TextChangeCommand : ICommand
    {
        private const string RedoMessage = "Redo cell text change";
        private const string UndoMessage = "Undo cell text change";

        private Cell cell;

        private string oldText;

        private string newText;

        public TextChangeCommand(Cell cell, string oldText, string newText)
        {
            this.cell = cell;
            this.oldText = oldText;
            this.newText = newText;
        }

        public void Execute()
        {
            this.cell.Text = newText;
        }

        public void UnExecute()
        {
            this.cell.Text = oldText;
        }

        public string GetRedoMessage()
        {
            return RedoMessage;
        }

        public string GetUndoMessage()
        {
            return UndoMessage;
        }
    }
}
