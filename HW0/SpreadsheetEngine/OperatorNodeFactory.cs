using SpreadsheetEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    internal class OperatorNodeFactory
    {
        public static OperatorNode CreateOperatorNode(char op)
        {
            switch (op)
            {
                case '+':
                    return new AdditionOperatorNode('+');

                case '-':
                    return new SubtractionOperatorNode('-');

                case '/':
                    return new DivisionOperatorNode('/');

                case '*':
                    return new MultiplicationOperatorNode('*');

                default:
                    throw new KeyNotFoundException();
            }
        }
    }
}
