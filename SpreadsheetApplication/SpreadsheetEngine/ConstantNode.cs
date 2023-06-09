﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Represents a node with a constant value (e.g. 4).
    /// </summary>
    internal class ConstantNode : Node
    {
        /// <summary>
        /// A constant value.
        /// </summary>
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value">A constant decimal value.</param>
        public ConstantNode(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets or sets the constant value of the node.
        /// </summary>
        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Returns the constant value.
        /// </summary>
        /// <returns>A double value.</returns>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
