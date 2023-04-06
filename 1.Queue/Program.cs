﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AlternativeMicrosoftGenericLibrary;
using HashTableForStudents;
using SkipList;

namespace _1.Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            //QueueTestPerformance();
            //AvlTreeTestPerformance();
            //HashTestPerformance();
            SkipListTestPerformance();
        }

        #region _Queue
        static void QueueTestPerformance()
        {
            var countEnqueue = 10000;
            var countDequeue = 7250;
            var countCycle = 1;

            var queue = new Queue<int>();
            var microsoftResult = QueueTestPerformance(
                queue: queue,
                countEnqueue: countEnqueue,
                countDequeue: countDequeue,
                countCycle: countCycle
            );
            Console.WriteLine($"Microsoft: {microsoftResult}");

            var _queue = new _Queue<int>();
            var alternativeResult = QueueTestPerformance(
                queue: _queue,
                countEnqueue: countEnqueue,
                countDequeue: countDequeue,
                countCycle: countCycle
            );
            Console.WriteLine($"Alternative: {alternativeResult}");
            Console.ReadLine();
        }
        static TimeSpan QueueTestPerformance(Queue<int> queue,int countEnqueue,int countDequeue,int countCycle)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < countCycle; i++)
            {
                for (var item = 0; item < countEnqueue; item++)
                {
                    queue.Enqueue(item);
                    queue.Contains(item);
                }
                for(var dequeue = 0; dequeue< countDequeue; dequeue++)
                {
                    queue.Dequeue();
                }
            }
            stopwatch.Stop();
            return new TimeSpan(stopwatch.ElapsedTicks);
        }

        static TimeSpan QueueTestPerformance(_Queue<int> queue, int countEnqueue, int countDequeue, int countCycle)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < countCycle; i++)
            {
                for (var item = 0; item < countEnqueue; item++)
                {
                    queue.Enqueue(item);
                    queue.Contains(item);
                }
                for (var dequeue = 0; dequeue < countDequeue; dequeue++)
                {
                    queue.Dequeue();
                }
            }
            stopwatch.Stop();
            return new TimeSpan(stopwatch.ElapsedTicks);
        }
        #endregion

        #region Avl
        static void AvlTreeTestPerformance()
        {
            var leftRandomBorber = -100000000;
            var rightRandomBorder = 100000000;

            var countAdd = 10000;
            var countDelete = 5000;

            var addList = new List<(string key, int value)>();
            var random = new Random();

            for(var i = 0; i < countAdd; i++)
            {
                var applicant = random.Next(leftRandomBorber, rightRandomBorder);
                if (addList.Contains((applicant.ToString(), applicant)))
                {
                    i--;
                    continue;
                }
                addList.Add((applicant.ToString(), applicant));
                //addList.Add((i, i));
            }

            var removeList = new List<(string key, int value)>();
            for (var i = countDelete; i < countDelete*2; i++)
            {
                removeList.Add(addList[i]);
            }

            var microsoftResult = SortedDict(addList, removeList);
            Console.WriteLine($"Microsoft: {microsoftResult}");

            var alternativeResult = AvlTree(addList, removeList);
            Console.WriteLine($"Alternative: {alternativeResult}");
            Console.ReadLine();
        }
        public static TimeSpan SortedDict(List<(string key,int value)> addList,List<(string key, int value)> removeList)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var dict = new SortedDictionary<string, int>();

            foreach (var item in addList) dict.Add(item.key, item.value);
            foreach (var item in removeList) dict.Remove(item.key);
            foreach (var item in addList) dict.ContainsKey(item.key);

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public static TimeSpan AvlTree(List<(string key, int value)> addList, List<(string key, int value)> removeList)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var avl = new AvlTree<string, int>();

            foreach (var item in addList) avl.Add(item.key, item.value);
            foreach (var item in removeList) avl.Remove(item.key);
            foreach (var item in addList) avl.Contains(item.key);

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }
        #endregion

        #region Hash
        static void HashTestPerformance()
        {
            var leftRandomBorber = -100000000;
            var rightRandomBorder = 100000000;

            var countAdd = 15000;
            var countDelete = 5000;

            var addList = new List<(string key, int value)>();
            var random = new Random();

            for (var i = 0; i < countAdd; i++)
            {
                var applicant = random.Next(leftRandomBorber, rightRandomBorder);
                if (addList.Contains((applicant.ToString(), applicant)))
                {
                    i--;
                    continue;
                }
                addList.Add((applicant.ToString(), applicant));
                //addList.Add((i, i));
            }

            var removeList = new List<(string key, int value)>();
            for (var i = countDelete; i < countDelete * 2; i++)
            {
                removeList.Add(addList[i]);
            }

            var microsoftResult = DictTest(addList, removeList);
            Console.WriteLine($"Microsoft: {microsoftResult}");

            var alternativeResult = HashSetTest(addList, removeList);
            Console.WriteLine($"Alternative: {alternativeResult}");
            Console.ReadLine();
        }

        public static TimeSpan HashSetTest(List<(string key, int value)> addList, List<(string key, int value)> removeList)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var hashTable = new OpenAddressHashTable<string, int>();

            foreach (var item in addList) hashTable.Add(item.key, item.value);
            foreach (var item in removeList) hashTable.Remove(item.key);
            foreach (var item in addList) hashTable.ContainsKey(item.key);

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public static TimeSpan DictTest(List<(string key, int value)> addList, List<(string key, int value)> removeList)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var dict = new Dictionary<string, int>();

            foreach (var item in addList) dict.Add(item.key, item.value);
            foreach (var item in removeList) dict.Remove(item.key);
            foreach (var item in addList) dict.ContainsKey(item.key);

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }
        #endregion

        #region SkipList
        static void SkipListTestPerformance()
        {
            var leftRandomBorber = -100000;
            var rightRandomBorder = 100000;

            var countAdd = 20000;
            var countDelete = 5000;

            var addList = new List<(int key, int value)>();
            var random = new Random();

            for (var i = 0; i < countAdd; i++)
            {
                var applicant = random.Next(leftRandomBorber, rightRandomBorder);
                if (addList.Contains((applicant, applicant)))
                {
                    i--;
                    continue;
                }
                addList.Add((applicant, applicant));
            }

            var removeList = new List<(int key, int value)>();
            for (var i = countDelete; i < countDelete * 2; i++)
            {
                removeList.Add(addList[i]);
            }

            var microsoftResult = SortedListTest(addList, removeList);
            Console.WriteLine($"Microsoft: {microsoftResult}");

            var alternativeResult = SkipListTest(addList, removeList);
            Console.WriteLine($"Alternative: {alternativeResult}");
            Console.ReadLine();
        }


        public static TimeSpan SortedListTest(List<(int key, int value)> addList, List<(int key, int value)> removeList)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var dict = new SortedList<int, int>();

            foreach (var item in addList) dict.Add(item.key, item.value);
            foreach (var item in removeList) dict.Remove(item.key);
            foreach (var item in addList) dict.ContainsKey(item.key);

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public static TimeSpan SkipListTest(List<(int key, int value)> addList, List<(int key, int value)> removeList)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var skipList = new SkipList<int, int>();

            foreach (var item in addList) skipList.Add(item.key, item.value);
            foreach (var item in removeList) skipList.Remove(item.key);
            foreach (var item in addList) skipList.Contains(item.key);

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }       
        #endregion
    }
}
