// <copyright file="VariableNode.cs" company="Molly Iverson:11775649">
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
    /// Represents a reference to a variable.
    /// </summary>
    internal class VariableNode : Node
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        private string name;

        /// <summary>
        /// The value of the variable.
        /// </summary>
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// Constructs a variable node.
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        public VariableNode(string variableName, double value)
        {
            this.name = variableName;
            this.value = value;
        }

        /// <summary>
        /// Gets or sets the name of the variable.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the value of the variable.
        /// </summary>
        public double Value
        {
            get { return this.value; }
            set { this.value = value; }

        }

        /// <summary>
        /// Returns the value of the variable.
        /// </summary>
        /// <returns>Variable value.</returns>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
