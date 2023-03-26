using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkipList;
using System;
using System.Collections.Generic;

namespace SkipList
{
    [TestClass]
    public class Add_Remove_Contains
    {
        [TestMethod]
        public void CountDecreaseAfterRemove()
        {
            int n = 10;
            var lib = new SkipList<int, int>();

            for (int i = 0; i < n; i++)
            {
                lib.Add(i, i);
            }
            for (int i = 0; i < n; i++)
            {
                lib.Remove(i);
                Assert.AreEqual(n-i-1, lib.Count);
            }
        }

        [TestMethod]
        public void EmptyListNotContainsZeroKey()
        {
            var skipList = new SkipList<int, int>();
            var nums = new List<int>();
            int n = nums.Count;
            for (int i = 0; i < n; i++)
            {
                skipList.Add(nums[i], i);
            }

            var result = skipList.Contains(0);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void AddNegativeItems()
        {
            var skipList = new SkipList<int, int>();
            var nums = new List<int>(new[] { 44, -22, -1, 56, -3, 90, 31, -15, -26 });
            int n = nums.Count;
            for (int i = 0; i < n; i++)
            {
                skipList.Add(nums[i], i);
            }
            foreach(var num  in nums)
            {
                var result = skipList.Contains(num);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddNegativeItems_Remove()
        {
            var skipList = new SkipList<int, int>();
            var nums = new List<int>(new[] { 4, -2, -15, 63, -23, -901, 531, -1215, 526 });
            int n = nums.Count;
            
            //в нескольких иттерациях проверяется, что удаление не портит логику добаления следующих элементов
            for(var iteration = 0; iteration < 3; iteration++)
            {
                for (int i = 0; i < n; i++)
                {
                    skipList.Add(nums[i], i);
                }
                foreach (var num in nums)
                {
                    var result = skipList.Contains(num);
                    Assert.AreEqual(true, result);
                }

                for (int i = 0; i < n; i++)
                {
                    var currentNum = nums[i];
                    skipList.Remove(currentNum);
                    var result = skipList.Contains(currentNum);
                    Assert.AreEqual(false, result);
                }
            }
        }

        [TestMethod]
        public void AddContainsRemoveEqualsKyes()
        {
            var skipList = new SkipList<int, int>();
            var a = 7;
            var nums = new List<int>(new[] { -4, -15, 64, a, -23, -901, 531, -1215, 526,a });
            int n = nums.Count;

            //предварительно добавлю, чтобы не зависеть от nums при проверке результата
            skipList.Add(a, a);

            //в нескольких иттерациях проверяется, что удаление не портит логику добаления следующих элементов
            for (var iteration = 0; iteration < 3; iteration++)
            {
                for (int i = 0; i < n; i++)
                {
                    skipList.Add(nums[i], i);
                }
                foreach (var num in nums)
                {
                    var result = skipList.Contains(num);
                    Assert.AreEqual(true, result);
                }

                for (int i = 0; i < n; i++)
                {
                    var currentNum = nums[i];
                    skipList.Remove(currentNum);
                    var result = skipList.Contains(currentNum);
                    var expected = currentNum == a ? true : false;
                    Assert.AreEqual(expected, result);
                }
            }
        }
    }
}
