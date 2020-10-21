using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueue
{
    public class TooLowCapacityException : Exception
    {
        public int Value { get; }
        public TooLowCapacityException(string message, int value) : base()
        {
            this.Value = value;
        }
    }
}
