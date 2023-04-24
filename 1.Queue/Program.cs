using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            HashTableTestPerformance();
            //SkipListTestPerformanceWhereTKeyIsInt();
            //SkipListTestPerformanceWhereTKeyIsIntToString();
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

        static Regex wordRegex = new Regex(@"\w+");
        static void HashTableTestPerformance()
        {
            var input = File.ReadAllText("input1.txt");
            var matches = wordRegex.Matches(input);

            var dict = new Dictionary<string, int>();
            foreach (Match match in matches)
            {
                var word = match.Value;
                if (dict.ContainsKey(word))
                {
                    dict[word]++;
                }
                else
                {
                    dict.Add(word, 1);
                }
            }
            var popularWords = dict.ToArray().OrderByDescending(w => w.Value).ToList();
            var addWords = popularWords.Select(w=>new KeyValuePair<string,int>(w.Key,w.Value)).ToList();
            var removeWords = addWords.Where(w=>w.Value>27).ToList();

            var count = 300;
            var microsoftSum = 0;
            var alternativeSum = 0;


            for(var i = 0; i < count; i++)
            {
                var microsoftResult = DictTest(addWords, removeWords).Milliseconds;
                Console.WriteLine($"Microsoft: {microsoftResult}");

                var alternativeResult = HashSetTest(addWords, removeWords).Milliseconds;
                Console.WriteLine($"Alternative: {alternativeResult}");

                microsoftSum += microsoftResult;
                alternativeSum += alternativeResult;
            }
            Console.WriteLine(alternativeSum*1.0/microsoftSum);

            Console.ReadLine();
        }
        //static void HashTestPerformance()
        //{
        //    var leftRandomBorber = -100000000;
        //    var rightRandomBorder = 100000000;

        //    var countAdd = 15000;
        //    var countDelete = 5000;

        //    var addList = new List<(string key, int value)>();
        //    var random = new Random();

        //    for (var i = 0; i < countAdd; i++)
        //    {
        //        var applicant = random.Next(leftRandomBorber, rightRandomBorder);
        //        if (addList.Contains((applicant.ToString(), applicant)))
        //        {
        //            i--;
        //            continue;
        //        }
        //        addList.Add((applicant.ToString(), applicant));
        //        //addList.Add((i, i));
        //    }

        //    var removeList = new List<(string key, int value)>();
        //    for (var i = countDelete; i < countDelete * 2; i++)
        //    {
        //        removeList.Add(addList[i]);
        //    }

        //    var microsoftResult = DictTest(addList, removeList);
        //    Console.WriteLine($"Microsoft: {microsoftResult}");

        //    var alternativeResult = HashSetTest(addList, removeList);
        //    Console.WriteLine($"Alternative: {alternativeResult}");
        //    Console.ReadLine();
        //}

        public static TimeSpan HashSetTest(List<KeyValuePair<string, int>> addList, List<KeyValuePair<string, int>> removeList)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var hashTable = new OpenAddressHashTable<string, int>();

            foreach (var item in addList) hashTable.Add(item.Key, item.Value);
            foreach (var item in removeList) hashTable.Remove(item.Key);
            foreach (var item in addList) hashTable.ContainsKey(item.Key);

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public static TimeSpan DictTest(List<KeyValuePair<string,int>> addList, List<KeyValuePair<string, int>> removeList)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var dict = new Dictionary<string, int>();

            foreach (var item in addList) dict.Add(item.Key, item.Value);
            foreach (var item in removeList) dict.Remove(item.Key);
            foreach (var item in addList) dict.ContainsKey(item.Key);

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }
        #endregion

        #region SkipList
        static void SkipListTestPerformanceWhereTKeyIsInt()
        {
            var leftRandomBorber = -10000000;
            var rightRandomBorder = 10000000;

            var countAdd = 100000;
            var countDelete = 5000;

            var addList = new List<(int key, int value)>();
            var random = new Random();

            for (var i = 0; i < countAdd; i++)
            {
                var applicant = random.Next();
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
            Console.WriteLine($"Microsoft: {microsoftResult.Milliseconds}");

            var alternativeResult = SkipListTest(addList, removeList);
            Console.WriteLine($"Alternative: {alternativeResult.Milliseconds}");
            Console.ReadLine();
        }
        static void SkipListTestPerformanceWhereTKeyIsIntToString()
        {
            var leftRandomBorber = -100000;
            var rightRandomBorder = 100000;

            var countAdd = 20000;
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
            }

            var removeList = new List<(string key, int value)>();
            for (var i = countDelete; i < countDelete * 2; i++)
            {
                removeList.Add(addList[i]);
            }

            var microsoftResult = SortedListTest(addList, removeList);
            Console.WriteLine($"Microsoft: {microsoftResult.Milliseconds}");

            var alternativeResult = SkipListTest(addList, removeList);
            Console.WriteLine($"Alternative: {alternativeResult.Milliseconds}");
            Console.ReadLine();
        }

        public static TimeSpan SortedListTest<TKey, TValue>(List<(TKey key, TValue value)> addList, List<(TKey key, TValue value)> removeList)
            where TKey : IComparable<TKey>
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var dict = new SortedList<TKey, TValue>();

            foreach (var item in addList) dict.Add(item.key, item.value);
            foreach (var item in removeList) dict.Remove(item.key);
            foreach (var item in addList) dict.ContainsKey(item.key);

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public static TimeSpan SkipListTest<TKey,TValue>(List<(TKey key, TValue value)> addList, List<(TKey key, TValue value)> removeList) 
            where TKey:IComparable<TKey>
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var skipList = new SkipList<TKey, TValue>();

            foreach (var item in addList) skipList.Add(item.key, item.value);
            foreach (var item in removeList) skipList.Remove(item.key);
            foreach (var item in addList) skipList.Contains(item.key);

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }       
        #endregion
    }
}
