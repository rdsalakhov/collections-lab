using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueue
{
    public class TooSmallArrayException : Exception
    {
        public int Value { get; }

        public TooSmallArrayException(string message, int value) : base()
        {
            this.Value = value;
        }
    }
}
