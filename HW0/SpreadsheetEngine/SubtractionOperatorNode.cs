// <copyright file="SubtractionOperatorNode.cs" company="Molly Iverson:11775649">
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
    /// Represents a subtraction operator node.
    /// </summary>
    internal class SubtractionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionOperatorNode"/> class.
        /// </summary>
        public SubtractionOperatorNode()
        {
            this.left = null;
            this.right = null;
            this.operatorSymbol = '-';
            this.precedence = 2;
            this.association = "left";
        }

        /// <summary>
        /// Subtracts two nodes from each other.
        /// </summary>
        /// <returns>The result.</returns>
        public override double Evaluate()
        {
            if (this.left != null && this.right != null)
            {
                return this.left.Evaluate() - this.right.Evaluate();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
