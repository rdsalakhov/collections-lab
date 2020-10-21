using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyQueue;

namespace Lab11Tests
{
    [TestClass]
    public class MyQueueTests
    {
        [TestMethod]
        public void EnqueuePeekTest()
        {
            MyQueue<int> q = new MyQueue<int>();
            q.Enqueue(10);
            int expected = 10;
            Assert.AreEqual(expected, q.Peek());
        }

        [TestMethod]
        public void DequeueTest()
        {
            MyQueue<int> q = new MyQueue<int>();
            q.Enqueue(10);
            var d = q.Dequeue();
            int expected = 0;
            Assert.AreEqual(expected, q.Peek());
        }

        [TestMethod]
        public void CloneTest()
        {
            MyQueue<int> q = new MyQueue<int>();
            q.Enqueue(10);
            
            var q2 = (MyQueue<int>)q.Clone();
            var expected = q;
            Assert.AreEqual(expected, q);
        }

        [TestMethod]
        public void CountTest()
        {
            MyQueue<int> q = new MyQueue<int>();
            q.Enqueue(10);

            var expected = 1;
            Assert.AreEqual(expected, q.Count());
        }

        [TestMethod]
        public void ContainsTrueTest()
        {
            MyQueue<int> q = new MyQueue<int>();
            q.Enqueue(10);

            var expected = true;
            Assert.AreEqual(expected, q.Contains(10));
        }
        [TestMethod]
        public void ContainsFalseTest()
        {
            MyQueue<int> q = new MyQueue<int>();
            q.Enqueue(10);

            var expected = false;
            Assert.AreEqual(expected, q.Contains(11));
        }

        [TestMethod]
        public void ToArrayTest()
        {
            MyQueue<int> q = new MyQueue<int>();
            q.Enqueue(10);
            q.Enqueue(10);

            int[] expected = { 10 , 10};
            CollectionAssert.AreEqual(expected, q.ToArray());
        }

        [TestMethod]
        public void CopyToTest()
        {
            MyQueue<int> q = new MyQueue<int>();
            q.Enqueue(10);
            var actual = new int[1];
            q.CopyTo(actual, 0);
            int[] expected = { 10 };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EqualsTrueTest()
        {
            MyQueue<int> q = new MyQueue<int>();
            q.Enqueue(10);
            MyQueue<int> q2 = new MyQueue<int>();
            q2.Enqueue(10);
            bool actual = q.Equals(q2);
            bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EqualsFaalseTest()
        {
            MyQueue<int> q = new MyQueue<int>();
            q.Enqueue(10);
            MyQueue<int> q2 = new MyQueue<int>();
            q2.Enqueue(1);
            bool actual = q.Equals(q2);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EnumeratorTest()
        {
            MyQueue<int> q = new MyQueue<int>();
            q.Enqueue(10);
            q.Enqueue(10);
            q.Enqueue(10);
            
            foreach(var i in q)
            {

            }
        }

        [TestMethod]
        public void TooLowCapacityTest()
        {
            try
            {
                MyQueue<int> q = new MyQueue<int>(-1);
                Assert.Fail();
            }
            catch (TooLowCapacityException e)
            {
                var val = e.Value;
            }
        }

        [TestMethod]
        public void CapacityConstructorTest()
        {
            MyQueue<int> q = new MyQueue<int>(10);
            var expected = 10;
            Assert.AreEqual(expected, q.Capacity);
        }

        [TestMethod]
        public void ArrayConstructorTest()
        {
            MyQueue<int> q = new MyQueue<int>(new int[]{ 10, 10, 10 });
            var expected = new MyQueue<int>();
            expected.Enqueue(10);
            expected.Enqueue(10);
            expected.Enqueue(10);
            bool ok = q.Equals(expected);
            Assert.AreEqual(true, ok);
        }

        [TestMethod]
        public void CopyToInvalidArrayTypeExceptionTest()
        {
            MyQueue<int> q = new MyQueue<int>(new int[] { 10, 10, 10 });
            var expected = new MyQueue<int>();
            expected.Enqueue(10);
            expected.Enqueue(10);
            expected.Enqueue(10);
            bool[] arr = new bool[3];
            try
            {
                q.CopyTo(arr, 0);
            }
            catch(InvalidArrayTypeException e)
            {
                var val = e.Value;
            }
            
        }

        [TestMethod]
        public void CopyToTooSmallArrayExceptionTest()
        {
            MyQueue<int> q = new MyQueue<int>(new int[] { 10, 10, 10 });
            var expected = new MyQueue<int>();
            expected.Enqueue(10);
            expected.Enqueue(10);
            expected.Enqueue(10);
            int[] arr = new int[5];
            try
            {
                q.CopyTo(arr, 4);
            }
            catch (TooSmallArrayException e)
            {
                var val = e.Value;

            }
        }

        [TestMethod]
        public void ClearTest()
        {
            MyQueue<int> q = new MyQueue<int>(new int[] { 10, 10, 10 });
            q.Clear();
            Assert.AreEqual(default, q.Peek());
        }
    }
}
