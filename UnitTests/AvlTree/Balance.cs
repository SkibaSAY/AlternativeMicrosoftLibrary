using AlternativeMicrosoftGenericLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.AvlTree
{
    [TestClass]
    public class Balance
    {
        /// <summary>
        /// 
        ///     7            9
        ///      9    =>   7  10
        ///       10
        /// 
        /// </summary>
        [TestMethod]
        public void TestSimpleRightDisbalance()
        {
            var avl = new AvlTree<int, string>();
            var addItems = new (int key, string value)[] { (7, "7"), (9, "9"), (11, "11") };

            foreach (var item in addItems)
            {
                avl.Add(item.key, item.value);
            }

            var root = avl._root;
            Assert.AreEqual(root.key, 9);
            Assert.AreEqual(root.leftChild.key, 7);
            Assert.AreEqual(root.rightChild.key, 11);
        }

        /// <summary>
        /// 
        ///     7            8
        ///      9    =>   7   9
        ///     8 
        /// 
        /// </summary>
        
        [TestMethod]
        public void TestSimpleRightDisbalance_2()
        {
            var avl = new AvlTree<int, string>();
            var addItems = new (int key, string value)[] { (7, "7"), (9, "9"), (8, "8") };

            foreach (var item in addItems)
            {
                avl.Add(item.key, item.value);
            }

            var root = avl._root;
            Assert.AreEqual(root.key, 8);
            Assert.AreEqual(root.leftChild.key, 7);
            Assert.AreEqual(root.rightChild.key, 9);
        }

        /// <summary>
        /// 
        ///     7           6
        ///   5    =>     5   7
        ///     6 
        /// 
        /// </summary>
        [TestMethod]
        public void TestSimpleLeftDisbalance()
        {
            var avl = new AvlTree<int, string>();
            var addItems = new (int key, string value)[] { (7, "7"), (5, "5"), (6, "6") };

            foreach (var item in addItems)
            {
                avl.Add(item.key, item.value);
            }

            var root = avl._root;
            Assert.AreEqual(root.key, 6);
            Assert.AreEqual(root.leftChild.key, 5);
            Assert.AreEqual(root.rightChild.key, 7);
        }

        /// <summary>
        ///  AfterRottate                 First Rotate(right)            Second Rotate(left) 
        ///     7                           7                                9
        ///  4     10             =>     4      9             =>         7        10
        ///      9    14                     8    10                  4     8         14
        ///    8                                     14
        /// </summary>
        [TestMethod]
        public void TestRightDisbalance()
        {
            var avl = new AvlTree<int, string>();
            var addItems = new (int key, string value)[] { (7, "7"), (4, "4"), (9, "9"), (8, "8"), (10, "10"), (14, "14") };

            foreach (var item in addItems)
            {
                avl.Add(item.key, item.value);
            }

            #region Second Rotate(left)
            var root = avl._root;
            Assert.AreEqual(root.key, 9);
            Assert.AreEqual(root.leftChild.key, 7);
            Assert.AreEqual(root.rightChild.key, 10);

            Assert.AreEqual(root.leftChild.leftChild.key, 4);
            Assert.AreEqual(root.leftChild.rightChild.key, 8);

            Assert.AreEqual(root.rightChild.leftChild, null);
            Assert.AreEqual(root.rightChild.rightChild.key, 14);
            #endregion
        }
    }
}
