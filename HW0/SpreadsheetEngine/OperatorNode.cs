﻿// <copyright file="OperatorNode.cs" company="Molly Iverson:11775649">
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
    /// Represents an operation to be done on two nodes (e.g. *, +).
    /// </summary>
    internal abstract class OperatorNode : Node
    {
        /// <summary>
        /// A character that represents a mathematical operation (+,-,/,*).
        /// </summary>
        protected char operatorSymbol;

        /// <summary>
        /// The node to the left of the operator.
        /// </summary>
        protected Node? left;

        /// <summary>
        /// The node to the right of the operator.
        /// </summary>
        protected Node? right;

        /// <summary>
        /// The rank of the operator in terms of precedence. Used to determine what operator to perform first.
        /// </summary>
        protected int precedence;

        /// <summary>
        /// Represents whether the operator is right, left, or nonassociated.
        /// </summary>
        protected string association;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="c">Operation character.</param>
        public OperatorNode(char c)
        {
            this.operatorSymbol = c;
            this.left = null;
            this.right = null;
            this.precedence = 0;
            this.association = "TBD";
        }

        /// <summary>
        /// Gets or sets the character representing a mathematical operation.
        /// </summary>
        public char OperatorSymbol
        {
            get { return this.operatorSymbol; }
            set { this.operatorSymbol = value; }
        }

        /// <summary>
        /// Gets or sets the node left of the operator.
        /// </summary>
        public Node? Left
        {
            get { return this.left; }
            set { this.left = value; }
        }

        /// <summary>
        /// Gets or sets the node right of the operator.
        /// </summary>
        public Node? Right
        {
            get { return this.right; }
            set { this.right = value; }
        }

        /// <summary>
        /// Gets or sets the precedence of the operation.
        /// </summary>
        public int Precedence
        {
            get { return this.precedence; }
            set { this.precedence = value; }
        }

        /// <summary>
        /// Gets or sets the associativity of the operation.
        /// </summary>
        public string Association
        {
            get { return this.association; }
            set { this.association = value; }
        }
    }
}