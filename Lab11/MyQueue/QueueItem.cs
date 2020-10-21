using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueue
{
    public class QueueItem<TValue>
    {
        public TValue value;

        public QueueItem<TValue> next;

        public QueueItem(TValue value)
        {
            this.value = value;
            this.next = null;
        }

        public QueueItem(TValue value, QueueItem<TValue> next)
        {
            this.value = value;
            this.next = next;
        }            
    }
}
