using AlternativeMicrosoftGenericLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class EnlargeSize
    {

        //[TestMethod]
        //public void EnlargeSizeWhenCapacityLessNewSize()
        //{
        //    var queue = new _Queue<int>();
        //    var newSize = 10;
        //    queue.EnlargeSize(newSize);
        //    Assert.AreEqual(queue._capacity, newSize);
        //}

        //    [TestMethod]
        //    public void EnlargeSizeWhenCapacityEqualNewSize()
        //    {
        //        var queue = new _Queue<int>();
        //        var newSize = 4;
        //        queue.EnlargeSize(newSize);
        //        Assert.AreEqual(queue._capacity, newSize);
        //    }

        //    [TestMethod]
        //    public void EnlargeSizeWhenCapacityMoveNewSize()
        //    {
        //        var queue = new _Queue<int>();
        //        var newSize = 2;
        //        queue.EnlargeSize(newSize);
        //        Assert.AreEqual(queue._capacity, newSize);
        //    }

        //    [TestMethod]
        //    public void EnlargeSizeWhenCapacityLessNewSizeWhenQueueIsNotEmpty()
        //    {
        //        var queue = new _Queue<int>();
        //        queue.Enqueue(7);
        //        queue.Enqueue(5);
        //        var newSize = 2;
        //        queue.EnlargeSize(newSize);
        //        Assert.AreEqual(queue._capacity, newSize);
        //        Assert.AreEqual(queue.Dequeue(), 7);
        //        Assert.AreEqual(queue.Dequeue(), 5);
        //    }

        //    [TestMethod]
        //    public void EnlargeSizeWhenCapacityLessNewSizeWhenQueueIsNotEmptyAndHeaderNotEqueal_one()
        //    {
        //        var queue = new _Queue<int>();
        //        queue.Enqueue(7);
        //        queue.Enqueue(5);
        //        Assert.AreEqual(queue.Dequeue(), 7);
        //        var newSize = 3;
        //        queue.EnlargeSize(newSize);
        //        Assert.AreEqual(queue._capacity, newSize);
        //        Assert.AreEqual(queue.Dequeue(), 5);
        //    }
    }
}
