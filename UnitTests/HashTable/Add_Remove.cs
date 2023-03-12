using HashTableForStudents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        public void Add100ItemsAndRemoveThey()
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
    }
}
