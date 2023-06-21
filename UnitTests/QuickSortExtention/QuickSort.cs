using AlternativeMicrosoftGenericLibrary.QuickSort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QuickSortExtention
{
    [TestClass]
    public class QuickSort
    {
        [TestMethod]
        public void QuickSortRandom()
        {
            var random = new Random();
            var n = 10;
            var arr = new int[n];

            for(var i = 0; i < n; i++)
            {
                arr[i] = random.Next();
            }

            var arrByQuickSort = new int[n];
            arr.CopyTo(arrByQuickSort,0);

            Array.Sort(arr);
            QuickSortExtention<int>.QuickSort(arrByQuickSort);

            for(var i = 0; i < n; i++)
            {
                Assert.AreEqual(arr[i], arrByQuickSort[i]);
            }
        }

        [TestMethod]
        public void QuickSortSuperRandom()
        {
            var m = 100;
            while(m > 0)
            {
                QuickSortRandom();
                m--;
            }
        }
    }
}
