// <copyright file="Node.cs" company="Molly Iverson:11775649">
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
