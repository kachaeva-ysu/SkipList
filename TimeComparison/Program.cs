using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkipList2020;
namespace TimeComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 10000;
            var rd = new Random(DateTime.Now.Millisecond);
            var set = new HashSet<int>();
            while (set.Count < n)
            {
                set.Add(rd.Next(0, 3 * n));
            }
            var sortedList = new SortedList<int, int>();
            var t = new Stopwatch();
            t.Start();
            foreach(var num in set)
            {
                sortedList.Add(num,60);
            }
            t.Stop();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(t.ElapsedMilliseconds);
            var skipList = new SkipList<int, int>(14, 0.5);
            t = new Stopwatch();
            t.Start();
            foreach (var num in set)
            {
                skipList.Add(num, 60);
            }
            t.Stop();
            Console.WriteLine(t.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
