using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueue
{
    public class InvalidArrayTypeException : Exception
    {
        public Type Value { get; }
        public InvalidArrayTypeException(string message, Type value) : base()
        {
            this.Value = value;
        }
    }
}
