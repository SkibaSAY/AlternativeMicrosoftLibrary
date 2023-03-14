using HashTableForStudents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTests.HashTable
{
    [TestClass]
    public class Contains
    {
        [TestMethod]
        public void AddItemRemoveHisAndContains()
        {
            var hashTable = new OpenAddressHashTable<string, int>();
            var count = 1000;
            for(var i = 0; i < count; i++)
            {
                hashTable.Add(i.ToString(), 1);
            }

            var list = new List<int>();
            for (var i = 1; i < count; i*=10)
            {
                var item = i.ToString();
                hashTable.Remove(item);
                list.Add(i);
            }

            foreach(var item in list)
            {
                var flag = hashTable.ContainsKey(item.ToString());
                Assert.AreEqual(flag, false);
            }

            for (var i = 0; i < count; i++)
            {
                if (list.Contains(i)) continue;
                var item = i.ToString();
                var flag = hashTable.ContainsKey(item.ToString());
                Assert.AreEqual(flag, true);
            }
        }
    }
}
