using System;
using System.Collections.Generic;

namespace DataStructuresTest
{
    public static class TestUtil
    {
        // Generate a list of random numbers
        public static List<int> GenRandList(int sz)
        {
            var rand = new Random();

            var list = new List<int>(sz);
            for (var i = 0; i < sz; i++) list.Add(rand.Next());
            Shuffle(list);
            return list;
        }

        // Generate a list of unique random numbers
        public static List<int> GenUniqueRandList(int sz)
        {
            var list = new List<int>(sz);
            for (var i = 0; i < sz; i++) list.Add(i);
            Shuffle(list);
            return list;
        }
        
        public static void Shuffle<T>(IList<T> list)
        {
            var rand = new Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rand.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}