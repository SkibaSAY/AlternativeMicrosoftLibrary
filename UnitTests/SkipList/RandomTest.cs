using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkipList;
using System;
using System.Collections.Generic;

namespace UnitTests.SkipList
{
    [TestClass]
    public class RandomTest
    {
        [TestMethod]
        public void Random()
        {
            var skipList = new SkipList<int, int>();
            var random = new Random();
            var nums = new List<int>();

            var min = 100;
            var max = 1500;
            int n = random.Next(min,max);
            for(var i = 0; i < n; i++)
            {
                nums.Add(random.Next(-max, max));
            }

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
                    skipList.Remove(nums[i]);
                    var result = skipList.Contains(nums[i]);
                    Assert.AreEqual(false, result);
                }
            }
        }
    }
}
