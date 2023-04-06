using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    public interface ICommand
    {
        public void Execute();

        public void UnExecute();

        public string GetRedoMessage();

        public string GetUndoMessage();

    }
}
