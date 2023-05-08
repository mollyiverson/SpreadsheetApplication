using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine;

namespace SpreadsheetEngine
{
    /// <summary>
    /// This class decouples the implemented operators from the ExpressionTree class so
    /// that it's easier to add new operators.
    /// </summary>
    internal class OperatorNodeFactory
    {
        /// <summary>
        /// Represents all subclasses of OperatorNode and their corresponding character symbol.
        /// </summary>
        private Dictionary<char, Type> operators = new Dictionary<char, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// Populates the operators dictionary with operator symbols and the corresponding OperatorNode subclass.
        /// </summary>
        public OperatorNodeFactory()
        {
            this.TraverseAvailableOperators((op, type) => this.operators.Add(op, type));
        }

        /// <summary>
        /// Helper method used to populate the operators dictionary.
        /// </summary>
        /// <param name="op">Operator character.</param>
        /// <param name="type">Subclass of OperatorNode.</param>
        private delegate void OnOperator(char op, Type type); // helper

        /// <summary>
        /// Creates an instance of a subclass of the OperatorNode class.
        /// </summary>
        /// <param name="op">Operator character.</param>
        /// <returns>A subclass of OperatorNode object.</returns>
        /// <exception cref="Exception">Throws an exception if the operator hasn't been implemented.</exception>
        public OperatorNode CreateOperatorNode(char op)
        {
            if (this.operators.ContainsKey(op))
            {
                object? operatorNodeObject = System.Activator.CreateInstance(this.operators[op]);
                if (operatorNodeObject is OperatorNode)
                {
                    return (OperatorNode)operatorNodeObject;
                }
            }

            throw new Exception("Unhandled operator");
        }

        /// <summary>
        /// Determines whether a character represents an accepted operator.
        /// </summary>
        /// <param name="op">Operator character.</param>
        /// <returns>Whether a given char is an operator.</returns>
        public bool IsOperator(char op)
        {
            return this.operators.ContainsKey(op);
        }

        /// <summary>
        /// Uses reflection to find all subclasses of OperatorNode and adds it to the operators dictionary.
        /// </summary>
        /// <param name="onOperator">Delegate used to populate the operators dictionary.</param>
        private void TraverseAvailableOperators(OnOperator onOperator) // populate dictionary, query all classes
        {
            Type operatorNodeType = typeof(OperatorNode);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Get all types that inherit from our OperatorNode class using LINQ
                IEnumerable<Type> operatorTypes =
                assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));

                // Iterate over those subclasses of OperatorNode
                foreach (var type in operatorTypes)
                {
                    // for each subclass, retrieve the Operator property
                    PropertyInfo? operatorField = type.GetProperty("OperatorSymbol");
                    if (operatorField != null)
                    {
                        // Get the character of the Operator
                        object? value = operatorField.GetValue(Activator.CreateInstance(type));
                        if (value is char)
                        {
                            char operatorSymbol = (char)value;

                            // And invoke the function passed as parameter
                            // with the operator symbol and the operator class
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
