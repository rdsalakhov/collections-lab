using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueue
{
    public class MyQueue<TValue> : ICloneable, IEnumerable<TValue> where TValue : ICloneable
    {
        QueueItem<TValue> startItem;
        int capacity;

        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                if (value <= 0) throw new TooLowCapacityException("Вместимость очереди не может быть меньше или равна нулю", value);
                else capacity = value;
            }
        }

        
        public MyQueue()
        {
            startItem = null;
            Capacity = 1;
        }

        public MyQueue(int capacity)
        {
            this.startItem = null;
            this.Capacity = capacity;
        }

        public MyQueue(params TValue[] values)
        {
            this.Capacity = values.Count();
            startItem = new QueueItem<TValue>(values[0]);
            var prevItem = startItem;
            for (int i = 1; i < Capacity; i++)
            {
                var curItem = new QueueItem<TValue>(values[i]);
                prevItem.next = curItem;
                prevItem = curItem;
            }
        }

        public int Count()
        {
            var curItem = startItem;
            int count = 0;
            while (curItem != null)
            {
                curItem = curItem.next;
                count++;
            }
            return count;
        }

        public void Clear()
        {
            startItem = null;
        }

        public void Enqueue(TValue newValue)
        {
            Capacity++;
            if (startItem == null) startItem = new QueueItem<TValue>(newValue);
            else
            {
                var curItem = startItem;
                while (curItem.next != null)
                {
                    curItem = curItem.next;
                }
                curItem.next = new QueueItem<TValue>(newValue);
            }
        }

        public TValue Dequeue()
        {
            TValue output = startItem.value;
            startItem = startItem.next;
            return output;
        }

        public TValue Peek()
        {
            if (startItem == null) return default(TValue);
            return startItem.value;
        }

        public bool Contains(TValue value)
        {
            var curItem = startItem;
            while (curItem != null)
            {
                if (curItem.value.Equals(value)) return true;
                curItem = curItem.next;
            }
            return false;
        }

        public TValue[] ToArray()
        {
            var curItem = startItem;
            var array = new TValue[this.Count()];
            for(int i = 0; curItem != null; i++)
            {
                array[i] = (TValue)curItem.value.Clone();
                curItem = curItem.next;
            }
            return array;
        }

        public void CopyTo(Array array, int startInd)
        {
            if (!(array is TValue[])) throw new InvalidArrayTypeException($"Массив не может включать объекты такого типа {typeof(TValue)}", typeof(TValue));
            var valueArray = array as TValue[];
            if (valueArray.Count() - startInd + 1 < this.Count()) throw new TooSmallArrayException("Массив не может вместить все элементы очереди", valueArray.Count());
            var curItem = startItem;
            for (int i = startInd; curItem != null; i++)
            {
                valueArray[i] = curItem.value;
                curItem = curItem.next;
            }
        }

        public object Clone()
        {
            var array = this.ToArray();
            var newQueue = new MyQueue<TValue>(array);
            return newQueue;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return new QueueEnumerator<TValue>(startItem);
        }

        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals(MyQueue<TValue> queue)
        {
            bool ok;
            var curItem = startItem;

            foreach (var i in queue)
            {
                ok = curItem.value.Equals(i);
                curItem = curItem.next;
                if (!ok) return false;
            }
            return true;
        }
    }
}
