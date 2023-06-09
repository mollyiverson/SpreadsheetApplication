﻿using System;
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
        public MultiplicationOperatorNode()
        {
            this.left = null;
            this.right = null;
            this.operatorSymbol = '*';
            this.precedence = 3;
            this.association = "left";
        }

        /// <summary>
        /// Multiplies two nodes together.
        /// </summary>
        /// <returns>The result.</returns>
        public override double Evaluate()
        {
            if (this.left != null && this.right != null)
            {
                return this.left.Evaluate() * this.right.Evaluate();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
