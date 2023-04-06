using System;
using System.Collections.Generic;
using System.Drawing;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    internal class ColorChangeCommand : ICommand
    {
        private const string RedoMessage = "Redo background color change";
        private const string UndoMessage = "Undo background color change";

        private List<Cell> cells;

        private uint previousColor;

        private uint newColor;

        public ColorChangeCommand(List<Cell> cells, uint previousColor, uint newColor)
        {
            this.cells = cells;
            this.previousColor = previousColor;
            this.newColor = newColor;
        }

        public void Execute()
        {
            foreach (Cell cell in cells)
            {
                cell.Color = this.newColor;
            }
        }

        public void UnExecute()
        {
            foreach (Cell cell in cells)
            {
                cell.Color = this.previousColor;
            }
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
