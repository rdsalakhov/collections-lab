using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueue
{
    public class QueueEnumerator<TValue> : IEnumerator<TValue>
    {
        QueueItem<TValue> startItem;
        QueueItem<TValue> curItem;

        public QueueEnumerator(QueueItem<TValue> startItem)
        {
            this.startItem = startItem;
            this.curItem = new QueueItem<TValue>(default, startItem);
        }

        public TValue Current
        {
            get { return curItem.value; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        

        public bool MoveNext()
        {
            if (curItem.next == null) return false;
            else
            {
                curItem = curItem.next;
                return true;
            }
        }

        public void Reset()
        {
            curItem = startItem;
        }

        public void Dispose()
        {
            
        }
    }
}
