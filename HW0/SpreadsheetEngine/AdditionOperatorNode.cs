﻿// <copyright file="AdditionOperatorNode.cs" company="Molly Iverson:11775649">
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
        public AdditionOperatorNode(char c)
            : base(c)
        {
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
