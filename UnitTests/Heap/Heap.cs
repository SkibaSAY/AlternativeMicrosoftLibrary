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

        [TestMethod]
        public void TestHeapify()
        {
            var heap = new Heap<int>(Comparer<int>.Default);
            var items = new int[] { 1, 5, 2, 17, 18, 7, 6, 2 };
            foreach(var item in items)
            {
                heap.Add(item);
            }

            var heapItems = heap.GetHeapItems();

        }
    }
}
