// <copyright file="Nodes.cs" company="Molly Iverson:11775649">
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
    /// Includes the abstract Node class and all its concrete subclasses.
    /// </summary>
    internal class Nodes
    {
        /// <summary>
        /// Represents an abstract Node in the Expression Tree.
        /// </summary>
        internal abstract class Node
        {
            public abstract double Evaluate();
        }

        /// <summary>
        /// Represents a constant value (e.g. 4).
        /// </summary>
        internal class ConstantNode : Node
        {
            private double value;

            public double Value { get; set; }

            public override double Evaluate()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Represents a reference to a variable.
        /// </summary>
        internal class VariableNode : Node
        {
            private string name;

            public string Name { get; set; }

            public override double Evaluate()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Represents an operation to be done on two nodes (e.g. *, +).
        /// </summary>
        internal class OperatorNode : Node
        {
            private char operatorSymbol;
            private Node left;
            private Node right;

            public OperatorNode(char c)
            {
                OperatorSymbol = c;
                this.Left = null;
                this.Right = null;
            }

            public char OperatorSymbol { get; set; }

            public Node Left { get; set; }

            public Node? Right { get; set; }

            public override double Evaluate()
            {
                throw new NotImplementedException();
            }
        }

    }
}
