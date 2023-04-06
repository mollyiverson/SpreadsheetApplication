// <copyright file="ICommand.cs" company="Molly Iverson:11775649">
// Copyright (c) Molly Iverson:11775649. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Represents a generic property change in a cell.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the change in the cell.
        /// </summary>
        public void Execute();

        /// <summary>
        /// Undoes the change in the cell.
        /// </summary>
        public void UnExecute();

        /// <summary>
        /// Returns the specific redo message for the type of command.
        /// </summary>
        /// <returns>Redo message.</returns>
        public string GetRedoMessage();

        /// <summary>
        /// Returns the specific undo message for the type of command.
        /// </summary>
        /// <returns>Undo message.</returns>
        public string GetUndoMessage();
    }
}
