// <copyright file="AdditionOperatorNode.cs" company="Molly Iverson:11775649">
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
    /// Represents a addition operator node.
    /// </summary>
    internal class AdditionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionOperatorNode"/> class.
        /// </summary>
        /// <param name="c">The operator symbol.</param>
        /// <param name="left">The node left of the operator.</param>
        /// <param name="right">The node right of the operator.</param>
        public AdditionOperatorNode(char c, Node left, Node right)
            : base(c, left, right)
        {
            this.precedence = 3;
        }

        /// <summary>
        /// Adds two nodes together.
        /// </summary>
        /// <returns>The result.</returns>
        public override double Evaluate()
        {
            return this.left.Evaluate() + this.right.Evaluate();
        }
    }
}
