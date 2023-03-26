using AlternativeMicrosoftGenericLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Queue
{
    [TestClass]
    public class Peek
    {
        [TestMethod]
        public void PeekBy1Item()
        {
            var q = new _Queue<int>();
            var newItem = 1;
            q.Enqueue(newItem);
            var queueFirst = q.Peek();
            Assert.AreEqual(queueFirst, newItem);
        }
        [TestMethod]
        public void PeekByRandomItem()
        {
            var q = new _Queue<int>();
            var rnd = new Random();
            var newItem = rnd.Next(100,1000);

            q.Enqueue(newItem);

            for (var i = 0; i < 100 ; i++)
            {
                q.Enqueue(i);
            }
            var queueFirst = q.Peek();
            Assert.AreEqual(queueFirst, newItem);
        }

        #region ExceptionsTests
        [TestMethod]
        [ExpectedException(typeof(_InvalidOperationException))]
        public void GetException()
        {
            var q = new _Queue<string>();
            q.Peek();
        }
        #endregion
    }
}
