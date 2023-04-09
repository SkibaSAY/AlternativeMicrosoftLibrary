using AlternativeMicrosoftGenericLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Heap
{
    [TestClass]
    public class Heap
    {
        [TestMethod]
        public void IncreaseCountAfterAdding()
        {
            var heap = new Heap<int>(Comparer<int>.Default);
            var count = 10;
            for(var i = 0; i < count; i++)
            {
                heap.Add(i);
            }
            Assert.AreEqual(count, heap.Count);
        }
    }
}
