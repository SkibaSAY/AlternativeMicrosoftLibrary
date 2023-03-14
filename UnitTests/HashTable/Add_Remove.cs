using HashTableForStudents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTests.HashTable
{
    [TestClass]
    public class Add_Remove
    {
        [TestMethod]
        public void IncreaseTableWhenAddNewItemAndWithoutExeptions()
        {

            try
            {
                var hashTable = new OpenAddressHashTable<string, int>();
                var count = 100;
                for (var i = 0; i < count; i++)
                {
                    hashTable.Add(i.ToString(), i);
                }
            }
            catch(Exception ex)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void Add100ItemsAndContainsAndRemoveThey()
        {
            var hashTable = new OpenAddressHashTable<string, int>();

            var count = 100;
            for (var i = 0; i < count; i++)
            {
                hashTable.Add(i.ToString(), i);
            }
            Assert.AreEqual(hashTable.Count, count);
            for (var i = 0; i < count; i++)
            {
                var isContains = hashTable.ContainsKey(i.ToString());
                Assert.AreEqual(isContains, true);
            }
            for (var i = 0; i < count; i++)
            {
                hashTable.Remove(i.ToString());
            }
            for (var i = 0; i < count; i++)
            {
                var isContains = hashTable.ContainsKey(i.ToString());
                Assert.AreEqual(isContains, false);
            }
            Assert.AreEqual(hashTable.Count, 0);
        }

        [TestMethod]
        public void Test()
        {
            var hashTable = new OpenAddressHashTable<string, int>();
            var items = "17608,-99110,-13754,59765,81869,-6376,-31307,39710,-8183,-6393".Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var item in items)
            {
                try
                {
                    hashTable.Add(item, 1);
                }
                catch(Exception ex)
                {
                    Assert.Fail();
                }
            }
        }
        [TestMethod]
        public void Test1()
        {
            var hashTable = new OpenAddressHashTable<string, int>();
            var items = "67945,-53542,62958,77960,45407,26837,25681,87882,-87349".Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in items)
            {
                try
                {
                    hashTable.Add(item, 1);
                }
                catch (Exception ex)
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void Random1000Items()
        {
            var hashTable = new OpenAddressHashTable<string, int>();
            var randomList = new List<string>();
            var random = new Random();

            var count = 1000;
            for (var i = 0; i < count; i++)
            {
                var num = random.Next(-100000, 100000);
                if (randomList.Contains(num.ToString()))
                {
                    i--;
                    continue;
                }
                randomList.Add(num.ToString());
                try
                {
                    hashTable.Add(num.ToString(), num);
                }
                catch(Exception ex)
                {
                    var failTest = String.Join(",", randomList);
                    Console.WriteLine(failTest);
                    Assert.Fail();
                }
            }

            Assert.AreEqual(hashTable.Count, count);
            foreach(var number in randomList)
            {
                var isContains = hashTable.ContainsKey(number);
                Assert.AreEqual(isContains, true);
            }
            foreach (var number in randomList)
            {
                hashTable.Remove(number);
            }
            foreach (var number in randomList)
            {
                var isContains = hashTable.ContainsKey(number);
                Assert.AreEqual(isContains, false);
            }
            Assert.AreEqual(hashTable.Count, 0);
        }
    }
}
