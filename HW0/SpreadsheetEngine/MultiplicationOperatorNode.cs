// <copyright file="MultiplicationOperatorNode.cs" company="Molly Iverson:11775649">
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
    /// Represents a multiplication operator node.
    /// </summary>
    internal class MultiplicationOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplicationOperatorNode"/> class.
        /// </summary>
        /// <param name="c">The operator symbol.</param>
        /// <param name="left">The node left of the operator.</param>
        /// <param name="right">The node right of the operator.</param>
        public MultiplicationOperatorNode(char c)
            : base(c)
        {
            this.precedence = 1;
        }

        /// <summary>
        /// Multiplies two nodes together.
        /// </summary>
        /// <returns>The result.</returns>
        public override double Evaluate()
        {
            return this.left.Evaluate() * this.right.Evaluate();
        }
    }
}
