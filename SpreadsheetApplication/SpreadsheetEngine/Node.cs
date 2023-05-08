using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// The abstract node class to be derived from.
    /// </summary>
    internal abstract class Node
    {
        /// <summary>
        /// Returns the value of the node.
        /// </summary>
        /// <returns>Value of node.</returns>
        public abstract double Evaluate();
    }
}
