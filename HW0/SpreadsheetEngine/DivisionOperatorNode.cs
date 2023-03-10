// <copyright file="DivisionOperatorNode.cs" company="Molly Iverson:11775649">
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
    /// Represents a division operator node.
    /// </summary>
    internal class DivisionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionOperatorNode"/> class.
        /// </summary>
        /// <param name="c">The operator symbol.</param>
        public DivisionOperatorNode(char c)
            : base(c)
        {
            this.precedence = 3;
            this.association = "left";
        }

        /// <summary>
        /// Divides a node from another.
        /// </summary>
        /// <returns>The result.</returns>
        public override double Evaluate()
        {
            if (this.left != null && this.right != null)
            {
                return this.left.Evaluate() / this.right.Evaluate();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
