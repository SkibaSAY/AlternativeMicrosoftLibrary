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

        [TestMethod]
        public void TestSortSeveralRandom()
        {
            var count = 100;
            for(var i = 0; i < count; i++)
            {
                TestSortRandom();
            }
        }

        [TestMethod]
        public void TestSortRandom()
        {
            var random = new Random();
            var selectionSize = 1000;
            var selection = new List<int>();

            var heapChildsCount = random.Next(2, 2);
            var heap = new Heap<int>(childsCount: heapChildsCount);

            for(var i = 0; i< selectionSize; i++)
            {
                var next = random.Next();
                selection.Add(next);
                heap.Add(next);
            }

            var result = heap.Sort();
            selection.Sort();

            Assert.AreEqual(selection.Count, result.Count());

            for(var i = 0; i < selection.Count; i++)
            {
                var expected = selection[i];
                var resulting = result[i];
                Assert.AreEqual(expected, resulting);
            }
        }



        #region SortSeveralSortedLists
        [TestMethod]
        public void TestSortSeveralSortedLists()
        {
            var severalSortedLists = new List<int[]>{
                new int[] { 1,21,31 },
                new int[] { 23,25,37 },
                new int[] { 1,2,3 },
            };

            var result = Heap<int>.SortSeveralSortedList(severalSortedLists);

            var sortedItems = severalSortedLists.SelectMany(arr => arr).OrderBy(x => x).ToList();
            for (var i = 0; i < sortedItems.Count; i++)
            {
                Assert.AreEqual(sortedItems[i], result[i]);
            }
        }

        [TestMethod]
        public void TestSortSeveralSortedLists2()
        {
            var severalSortedLists = new List<int[]>{
                new int[] { 2,51 },
                new int[] { 1,4,74,79 },
                new int[] { 1,20,32 },
            };

            var result = Heap<int>.SortSeveralSortedList(severalSortedLists);

            var sortedItems = severalSortedLists.SelectMany(arr => arr).OrderBy(x => x).ToList();
            for (var i = 0; i < sortedItems.Count; i++)
            {
                Assert.AreEqual(sortedItems[i], result[i]);
            }
        }

        [TestMethod]
        public void TestSortSeveralSortedLists_MaxComporator()
        {
            var severalSortedLists = new List<int[]>{
                new int[] { 200,51 },
                new int[] { 100,40,17,15 },
                new int[] { 165,20,5,4,3,2,1 },
            };

            var result = Heap<int>.SortSeveralSortedList(
                severalSortedLists,
                Comparer<int>.Create((x,y)=> (-1)*x.CompareTo(y))
                );

            var sortedItems = severalSortedLists.SelectMany(arr => arr).OrderByDescending(x => x).ToList();
            for (var i = 0; i < sortedItems.Count; i++)
            {
                Assert.AreEqual(sortedItems[i], result[i]);
            }
        }
        #endregion
    }
}
