﻿using System;
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
        public AdditionOperatorNode()
        {
            this.left = null;
            this.right = null;
            this.operatorSymbol = '+';
            this.precedence = 2;
            this.association = "left";
        }

        /// <summary>
        /// Adds two nodes together.
        /// </summary>
        /// <returns>The result.</returns>
        public override double Evaluate()
        {
            if (this.left != null && this.right != null)
            {
                return this.left.Evaluate() + this.right.Evaluate();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
