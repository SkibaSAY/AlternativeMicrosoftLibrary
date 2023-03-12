using AlternativeMicrosoftGenericLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.AvlTree
{
    [TestClass]
    public class Remove
    {
        /// <summary>
        /// 
        ///                  6                         6
        ///              4       7        =>       4       7
        ///           2    5       8            1    5        8
        ///         1   3                        3
        /// 
        /// 
        /// </summary>
        [TestMethod]
        public void RemoveWhenNodeHave2Childs()
        {
            var avl = new AvlTree<int, int>();
            var addList = new int[] { 7, 4, 6, 8, 2, 5, 1, 3 };
            var removeItem = 2;

            foreach (var item in addList) avl.Add(item, item);
            avl.Remove(removeItem);

            var root = avl._root;
            Assert.AreEqual(root.key, 6);
            Assert.AreEqual(root.leftChild.key, 4);
            Assert.AreEqual(root.rightChild.key, 7);
            Assert.AreEqual(root.leftChild.leftChild.key, 1);
            Assert.AreEqual(root.leftChild.leftChild.rightChild.key, 3);
            Assert.AreEqual(root.leftChild.rightChild.key, 5);
            Assert.AreEqual(root.rightChild.rightChild.key, 8);
        }
    }
}
