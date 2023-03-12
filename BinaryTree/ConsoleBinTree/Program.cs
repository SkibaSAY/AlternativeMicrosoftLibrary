using BinaryTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace ConsoleBinTree
{
    class Program
    {
        static void Main()
        {
            const int size = 10000;
            int[] array = new int[size];
            BinaryTree<int,int> bintree = new BinaryTree<int, int>();

            Random randNum = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                bool flag = true;
                while (flag)
                {
                    int randInt = randNum.Next(0, size*4);
                    if (!array.Contains(randInt))
                    {
                        array[i] = randInt;
                        flag = false;
                    }
                }
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for(int i = 0; i < array.Length; i++)
            {
                bintree.Add(array[i], 0);
            }
            
            for (int i = 0; i < array.Length; i++)
            {
                bintree.Contains(array[i], 0);
            }
            stopWatch.Stop();
            Console.WriteLine("Binary Tree: {0}", stopWatch.ElapsedMilliseconds);

            SortedDictionary<int, int> sortdict = new SortedDictionary<int, int>();

            Stopwatch Watch = new Stopwatch();
            Watch.Start();

            for (int i = 0; i < array.Length; i++)
            {
                sortdict.Add(i, 0);
            }
            
            for (int i = 0; i < array.Length; i++)
            {
                sortdict.ContainsKey(i);
            }

            Watch.Stop();
            Console.WriteLine("Sorted Dictionary: {0}", Watch.ElapsedMilliseconds);
            Console.ReadKey();

        }
    }
}
