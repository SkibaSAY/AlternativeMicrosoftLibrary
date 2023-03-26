using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AlternativeMicrosoftGenericLibrary;

namespace Queue
{
    [TestClass]
    public class Count
    {
        [TestMethod]
        public void Count_Test()
        {
            var queue = new _Queue<int>();
            var count = queue.Count;

            Assert.AreEqual(count, 0);
        }

        [TestMethod]
        public void IncreasedCountAfterEnqueue()
        {
            var queue = new _Queue<int>();
            var newItem = 10;
            queue.Enqueue(newItem);

            Assert.AreEqual(queue.Count, 1);
        }

        [TestMethod]
        public void DecreaseCountAfterDequeue()
        {
            var queue = new _Queue<int>();
            var newItem = 10;
            queue.Enqueue(newItem);
            queue.Dequeue();

            Assert.AreEqual(queue.Count, 0);
        }
    }
}
