using AlternativeMicrosoftGenericLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.AvlTree
{
    [TestClass]
    public class Find_Contains
    {
        [TestMethod]
        public void ContainsIsFalseWhenTreeIsEmpty()
        {
            var avl = new AvlTree<int, string>();
            var result = avl.Contains(1);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainsIsFalseWhenKeyHasDeleted()
        {
            var avl = new AvlTree<int, string>();
            avl.Add(1, "1");
            avl.Remove(1);
            var result = avl.Contains(1);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainsIsTrue()
        {
            var avl = new AvlTree<int, string>();
            
            for(var i =0; i < 100; i++)
            {
                avl.Add(i, i.ToString());
            }

            var result = avl.Contains(54);
            Assert.IsTrue(result);
        }
    }
}
