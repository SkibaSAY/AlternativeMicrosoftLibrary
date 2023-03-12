using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryTree;

namespace BinTreeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CountIncreaseAfterAdding()
        {
            var tree = new BinaryTree<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                tree.Add(i, i);
            }
            Assert.AreEqual(n, tree.Count);
        }
        [TestMethod]
        public void ItemsExistAfterAdding()
        {
            var tree = new BinaryTree<int, int>();
            var a = new[] { 22, 30, 15, 5, 17, 24, 33, 10, 16, 26 };
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                tree.Add(a[i], i);
            }
            Assert.AreEqual(n, tree.Count);
            Array.Sort(a);
            int j = 0;
            foreach (var pair in tree)
            {
                Assert.AreEqual(a[j], pair.Key);
                j++;
            }

        }

        [TestMethod]
        public void ContainsExistingElement()
        {
            var tree = new BinaryTree<int, int>();
            var a = new[] { 8, 3, 10, 1, 6, 14, 4, 7, 13 };
            for (int i = 0; i < a.Length; i++)
            {
                tree.Add(a[i], 0);
            }
            Assert.AreEqual(true, tree.Contains(6, 0));
        }

        [TestMethod]
        public void ContainsNotExistingElement()
        {
            var tree = new BinaryTree<int, int>();
            var a = new[] { 8, 3, 10, 1, 6, 14, 4, 7, 13 };
            for (int i = 0; i < a.Length; i++)
            {
                tree.Add(a[i], 0);
            }
            Assert.AreEqual(false, tree.Contains(37, 0));
        }

        
    }
}
