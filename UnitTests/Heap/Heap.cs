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
            var maxComparer = Comparer<int>.Create((x, y) => (-1) * x.CompareTo(y));
            var heap = new Heap<int>(maxComparer);
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

        #region sort
        [TestMethod]
        public void TestSort()
        {
            var heap = new Heap<int>();
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
        public void TestSort_AlterBeingError()
        {
            var random = new Random();
            var selection = new List<int>();
            selection.AddRange("1075117264,102255216,438589739,205034887,1795841094,2013453474,1997926023,395842402,2056122438,1679089123"
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)));

            var heapChildsCount = random.Next(2, 2);
            var heap = new Heap<int>(childsCount: heapChildsCount);

            for (var i = 0; i < selection.Count; i++)
            {
                heap.Add(selection[i]);
            }

            var result = heap.Sort();
            selection.Sort();

            Assert.AreEqual(selection.Count, result.Count());

            for (var i = 0; i < selection.Count; i++)
            {
                var expected = selection[i];
                var resulting = result[i];
                Assert.AreEqual(expected, resulting);
            }
        }

        public void TestSortRandom(IComparer<int> heapComparer)
        {
            var random = new Random();
            var selectionSize = 1000;
            var selection = new List<int>();

            var heapChildsCount = random.Next(2, 20);
            var heap = new Heap<int>(comparer:heapComparer,childsCount: heapChildsCount);

            for (var i = 0; i < selectionSize; i++)
            {
                var next = random.Next();
                selection.Add(next);
                heap.Add(next);
            }

            var temp = new int[selectionSize];
            selection.CopyTo(temp);

            var result = heap.Sort();
            selection.Sort(heapComparer);

            Assert.AreEqual(selection.Count, result.Count());

            for (var i = 0; i < selection.Count; i++)
            {
                var expected = selection[i];
                var resulting = result[i];
                Assert.AreEqual(expected, resulting);
            }
        }

        [TestMethod]
        public void TestSortMaxHeap_Random()
        {
            var count = 100;
            for(var i = 0; i < count; i++)
            {
                TestSortRandom(Comparer<int>.Create((x, y) => (-1) * x.CompareTo(y)));
            }
        }

        [TestMethod]
        public void TestSortMinHeap_Random()
        {
            var count = 100;
            for (var i = 0; i < count; i++)
            {
                TestSortRandom(Comparer<int>.Default);
            }
        }

        #endregion


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

        [TestMethod]
        public void TestSortSeveralSortedLists_Random()
        {
            var severalSortedLists = new List<int[]>();
            var random = new Random();
            var arrayCount = random.Next(1,100);

            for (var i = 0; i < arrayCount; i++)
            {
                var newArrSize = random.Next(0,300);
                var newArr = new int[newArrSize];
                for (var j = 0; j < newArrSize; j++)
                {
                    newArr[j] = random.Next();
                }
                Array.Sort(newArr);
                severalSortedLists.Add(newArr);
            }

            var result = Heap<int>.SortSeveralSortedList(
                severalSortedLists
                );

            var sortedItems = severalSortedLists.SelectMany(arr => arr).OrderBy(x => x).ToList();
            var flag = true;
            for (var i = 0; i < sortedItems.Count; i++)
            {
                try
                {
                    Assert.AreEqual(sortedItems[i], result[i]);
                }
                catch
                {
                    flag = false;
                }
            }

            if (!flag)
            {
                throw new Exception();
            }
        }
        #endregion
    }
}
