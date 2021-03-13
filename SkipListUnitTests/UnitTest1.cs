using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkipList2020;

namespace SkipListUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CountIncreasesAfterAdding()
        {
            int n = 10;
            var lib = new SkipList<int, int>();

            for(int i=0; i<n; i++)
            {
                lib.Add(i, i);
            }
            Assert.AreEqual(n, lib.Count);
        }
        [TestMethod]
        public void ItemsExistAfterAdding()
        {
            var lib = new SkipList<int, int>();
            var nums = new List<int>(new[] { 44, 22, 1 , 56, 3, 90, 31, 15, 26 });
            int n = nums.Count;
            for (int i = 0; i < n; i++)
            {
                lib.Add(nums[i], i);
            }
            nums.Sort();
            int j = 0;
            foreach(var pair in lib)
            {
                Assert.AreEqual(nums[j], pair.Key);
                j++;
            }
            Assert.AreEqual(n, lib.Count);

        }

        [TestMethod]
        public void ContainsReturnsTrueIfItemExists()
        {
            var lib = new SkipList<int, int>();
            lib.Add(15, 1);
            Assert.AreEqual(true, lib.Contains(15));
        }
        [TestMethod]
        public void ContainsReturnsFalseIfItemDoesNotExist()
        {
            var lib = new SkipList<int, int>();
            Assert.AreEqual(false, lib.Contains(15));
        }
        [TestMethod]
        public void CountDecreasesAfterRemoving()
        {
            int n = 10;
            var lib = new SkipList<int, int>();

            for (int i = 0; i < n; i++)
            {
                lib.Add(i, i);
            }
            lib.Remove(5);
            Assert.AreEqual(n-1, lib.Count);
        }
        [TestMethod]
        public void ItemsDoNotExistAfterRemoving()
        {
            var lib = new SkipList<int, int>();
            var nums = new List<int>(new[] { 44, 22, 1, 56, 3, 90, 31, 15, 26 });
            int n = nums.Count;
            for (int i = 0; i < n; i++)
            {
                lib.Add(nums[i], i);
            }
            nums.Sort();
            nums = new List<int>(new[] { 44, 22, 1, 56, 3, 90 });
            nums.Sort();
            lib.Remove(31);
            lib.Remove(15);
            lib.Remove(26);
            int j = 0;
            foreach (var pair in lib)
            {
                Assert.AreEqual(nums[j], pair.Key);
                j++;
            }
            Assert.AreEqual(n, lib.Count);

        }

    }
}
