using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkipList2020;

namespace SkipListVsSortedList
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 100000;
            var rd = new Random(DateTime.Now.Millisecond);
            var set = new HashSet<int>();
            while (set.Count < n)
                set.Add(rd.Next(0, 3 * n));
            var numbers = set.ToArray();

            Console.WriteLine("Random numbers");
            var sortedList = new SortedList<int, int>();
            var t = new Stopwatch();
            t.Start();
            for (int i = 0; i < 100000; i++)
                sortedList.Add(numbers[i],1);
            for (int i = 50000; i < 70000; i++)
                sortedList.Remove(numbers[i]);
            for (int i = 0; i < 100000; i++)
                sortedList.ContainsKey(numbers[i]);
            t.Stop();
            Console.WriteLine("Sorted list " + t.ElapsedMilliseconds);

            var skipList = new SkipList<int, int>();
            t = new Stopwatch();
            t.Start();
            for (int i = 0; i < 100000; i++)
                skipList.Add(numbers[i], 1);
            for (int i = 50000; i < 70000; i++)
                skipList.Remove(numbers[i]);
            for (int i = 0; i < 100000; i++)
                skipList.Contains(numbers[i]);
            t.Stop();
            Console.WriteLine("Skip list " + t.ElapsedMilliseconds);

            Console.WriteLine("Increasing numbers");
            sortedList = new SortedList<int, int>();
            t = new Stopwatch();
            t.Start();
            for (int i = 0; i < 100000; i++)
                sortedList.Add(i, 1);
            for (int i = 50000; i < 70000; i++)
                sortedList.Remove(i);
            for (int i = 0; i < 100000; i++)
                sortedList.ContainsKey(i);
            t.Stop();
            Console.WriteLine("Sorted list " + t.ElapsedMilliseconds);

            skipList = new SkipList<int, int>();
            t = new Stopwatch();
            t.Start();
            for (int i = 0; i < 100000; i++)
                skipList.Add(i, 1);
            for (int i = 50000; i < 70000; i++)
                skipList.Remove(i);
            for (int i = 0; i < 100000; i++)
                skipList.Contains(i);
            t.Stop();
            Console.WriteLine("Skip list " + t.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
