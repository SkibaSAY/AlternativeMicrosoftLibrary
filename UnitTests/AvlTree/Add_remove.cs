﻿using AlternativeMicrosoftGenericLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.AvlTree
{

    [TestClass]
    public class Add_remove
    {
        [TestMethod]
        public void Add1ItemRemove1Item()
        {
            var addItems = new (int key, string value)[] { (7, "7") };
            var removeitems = new (int key, string value)[] { (7, "7") };
            var avl = new AvlTree<int, string>();

            foreach (var addItem in addItems)
            {
                avl.Add(addItem.key, addItem.value);
            }

            var root = avl._root;
            Assert.AreEqual(root.value, "7");
            Assert.AreEqual(root.leftChild, null);
            Assert.AreEqual(root.rightChild, null);

            foreach (var removeItem in removeitems)
            {
                avl.Remove(removeItem.key);
            }
            root = avl._root;
            Assert.AreEqual(root, null);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AddItemThatIsAlreadyContainsInTree()
        {
            var addItems = new (int key, string value)[] { (7, "7"), (7, "7") };
            var removeitems = new (int key, string value)[] { (7, "7") };
            var avl = new AvlTree<int, string>();

            foreach (var addItem in addItems)
            {
                avl.Add(addItem.key, addItem.value);
            }

            foreach (var removeItem in removeitems)
            {
                avl.Remove(removeItem.key);
            }
        }

        /// <summary>
        /// 
        ///          7                  5                    5          5
        ///      4       9    =>      4   9      =>        4  10  =>     10
        ///       5       10                10
        /// 
        /// 
        /// 
        /// 
        /// </summary>
        [TestMethod]
        public void Add5Items_removeRootAnd2items()
        {
            var addItems = new (int key, string value)[] {(7,"7"), (9, "9"), (4, "4"), (5, "5"), (10, "10") };
            var removeitems = new (int key, string value)[] { (7, "7"), (9, "9"), (4, "4") };
            var avl = new AvlTree<int, string>();

            foreach (var addItem in addItems)
            {
                avl.Add(addItem.key, addItem.value);
            }

            var root = avl._root;
            Assert.AreEqual(root.value, "7");
            Assert.AreEqual(root.leftChild.value,"4");
            Assert.AreEqual(root.leftChild.leftChild, null);
            Assert.AreEqual(root.leftChild.rightChild.value, "5");
            Assert.AreEqual(root.rightChild.value, "9");
            Assert.AreEqual(root.rightChild.leftChild, null);
            Assert.AreEqual(root.rightChild.rightChild.value, "10");

            foreach (var removeItem in removeitems)
            {
                avl.Remove(removeItem.key);
            }
            root = avl._root;
            Assert.AreEqual(root.value, "5");
            Assert.AreEqual(root.leftChild, null);
            Assert.AreEqual(root.rightChild.value, "10");
        }

    }
}
