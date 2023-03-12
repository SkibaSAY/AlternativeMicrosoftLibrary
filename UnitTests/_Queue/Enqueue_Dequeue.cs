using AlternativeMicrosoftGenericLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class Enqueue_Dequeue
    {
        #region UsersTest
        [TestMethod]
        public void Enqueue100Items_Dequeue100()
        {
            var q = new _Queue<int>();
            for (var i = 0; i < 100; i++) q.Enqueue(i);
            for (var i = 0; i < 100; i++)
            {
                var item = q.Dequeue();
                Assert.AreEqual(i,item);
            }
        }

        [TestMethod]
        public void Enqueue4_Dequeue2_Enqueue4()
        {
            var q = new _Queue<int>();
            var sys_q = new Queue<int>();
            for(var i=0; i < 4; i++)
            {
                q.Enqueue(i);
                sys_q.Enqueue(i);
            }
            for (var i = 0; i < 2; i++)
            {
                var itemFrom_q = q.Dequeue();
                var itemFrom_sys_q = sys_q.Dequeue();
                Assert.AreEqual(itemFrom_q, itemFrom_sys_q);
            }
            for (var i = 0; i < 4; i++)
            {
                q.Enqueue(i);
                sys_q.Enqueue(i);
            }
            for(var i = 0; i < q.Count; i++)
            {
                var itemFrom_q = q.Dequeue();
                var itemFrom_sys_q = sys_q.Dequeue();
                Assert.AreEqual(itemFrom_q, itemFrom_sys_q);
            }
        }

        [TestMethod]
        public void Enqueue4_Dequeue2_Enqueue2()
        {
            var q = new _Queue<int>();
            var sys_q = new Queue<int>();
            for (var i = 0; i < 4; i++)
            {
                q.Enqueue(i);
                sys_q.Enqueue(i);
            }
            for (var i = 0; i < 2; i++)
            {
                var itemFrom_q = q.Dequeue();
                var itemFrom_sys_q = sys_q.Dequeue();
                Assert.AreEqual(itemFrom_q, itemFrom_sys_q);
            }

            var rnd = new Random();
            for (var i = 0; i < 2; i++)
            {
                var newItem = rnd.Next(-100,100);
                q.Enqueue(newItem);
                sys_q.Enqueue(newItem);
            }


            for (var i = 0; i < q.Count; i++)
            {
                var itemFrom_q = q.Dequeue();
                var itemFrom_sys_q = sys_q.Dequeue();
                Assert.AreEqual(itemFrom_q, itemFrom_sys_q);
            }
        }

        [TestMethod]
        public void Enqueue4_Dequeue2_Enqueue2_Dequeue2()
        {
            var q = new _Queue<int>();
            var sys_q = new Queue<int>();
            for (var i = 0; i < 4; i++)
            {
                q.Enqueue(i);
                sys_q.Enqueue(i);
            }
            for (var i = 0; i < 2; i++)
            {
                var itemFrom_q = q.Dequeue();
                var itemFrom_sys_q = sys_q.Dequeue();
                Assert.AreEqual(itemFrom_q, itemFrom_sys_q);
            }

            var rnd = new Random();
            for (var i = 0; i < 2; i++)
            {
                var newItem = rnd.Next(-100, 100);
                q.Enqueue(newItem);
                sys_q.Enqueue(newItem);
            }

            for (var i = 0; i < 2; i++)
            {
                var itemFrom_q = q.Dequeue();
                var itemFrom_sys_q = sys_q.Dequeue();
                Assert.AreEqual(itemFrom_q, itemFrom_sys_q);
            }


            for (var i = 0; i < q.Count; i++)
            {
                var itemFrom_q = q.Dequeue();
                var itemFrom_sys_q = sys_q.Dequeue();
                Assert.AreEqual(itemFrom_q, itemFrom_sys_q);
            }
        }

        [TestMethod]
        public void Enqueue4_Dequeue2_Enqueue5_6_7()
        {
            var q = new _Queue<int>();
            var sys_q = new Queue<int>();
            for (var i = 1; i <= 4; i++)
            {
                q.Enqueue(i);
                sys_q.Enqueue(i);
            }
            for (var i = 0; i < 2; i++)
            {
                var itemFrom_q = q.Dequeue();
                var itemFrom_sys_q = sys_q.Dequeue();
                Assert.AreEqual(itemFrom_q, itemFrom_sys_q);
            }
            for (var i = 5; i <= 7; i++)
            {
                q.Enqueue(i);
                sys_q.Enqueue(i);
            }
            for (var i = 0; i < q.Count; i++)
            {
                var itemFrom_q = q.Dequeue();
                var itemFrom_sys_q = sys_q.Dequeue();
                Assert.AreEqual(itemFrom_q, itemFrom_sys_q);
            }
        }
        #endregion

        #region ExceptionsTests
        [TestMethod]
        [ExpectedException(typeof(_InvalidOperationException))]
        public void GetException()
        {
            var q = new _Queue<string>();
            q.Dequeue();
        }
        #endregion
    }
}
