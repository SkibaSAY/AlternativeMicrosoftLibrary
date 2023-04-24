using AlternativeMicrosoftGenericLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var expected = new int[] { 18, 17, 7, 2, 5, 2, 6, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
            var a = 0;
            Assert.AreEqual(expected.Length, heapItems.Count());
            foreach(var item in heapItems)
            {
                Assert.AreEqual(expected[a], item);
                a++;
            }
        }

        [TestMethod]
        public void TestSort()
        {
            var heap = new Heap<int>(Comparer<int>.Default);
            var items = new int[] { 1, 5, 2, 17, 18, 7, 6, 2 };
            foreach (var item in items)
            {
                heap.Add(item);
            }
            var sortedItems = heap.Sort();
            Array.Sort(items);
            for(var i = 0;i < items.Length; i++)
            {
                Assert.AreEqual(items[i], sortedItems[i]);
            }
        }
    }
}
