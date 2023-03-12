using AlternativeMicrosoftGenericLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class Contains
    {
        [TestMethod]
        public void ContainsTrue_False()
        {
            var q = new _Queue<int>();
            q.Enqueue(5);
            q.Enqueue(7);
            q.Enqueue(25);

            var resultTrue = q.Contains(5);
            Assert.AreEqual(resultTrue, true);

            var resultFalse = q.Contains(3);
            Assert.AreEqual(resultFalse, false);
        }

        [TestMethod]
        public void ContainsTrue_False_ByString()
        {
            var q = new _Queue<string>();
            q.Enqueue("51");
            q.Enqueue("7");
            q.Enqueue("25");

            var resultTrue = q.Contains("51");
            Assert.AreEqual(resultTrue, true);

            var resultFalse = q.Contains("57");
            Assert.AreEqual(resultFalse, false);
        }

        [TestMethod]
        public void ContainsTrue_False_ByString_WhenSearchedItemIsDelete()
        {
            var q = new _Queue<string>();
            q.Enqueue("51");
            q.Enqueue("7");
            q.Enqueue("25");

            var resultTrue = q.Contains("51");
            Assert.AreEqual(resultTrue, true);

            q.Dequeue();

            var resultFalse = q.Contains("51");
            Assert.AreEqual(resultFalse, false);
        }
    }
}
