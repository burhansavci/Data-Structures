using System;
using System.Collections.Generic;

namespace DataStructuresTest
{
    public static class TestUtil
    {
        private static readonly Random Rand = new();

        // Generate a list of random numbers
        public static List<int> GenRandList(int sz)
        {
            var list = new List<int>(sz);
            for (var i = 0; i < sz; i++) list.Add(Rand.Next());
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
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rand.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static int GetRandomInteger(int min, int max) => Rand.Next(max - min + 1) + min;

        public static double GetRandomDouble(double minimum, double maximum) => Rand.NextDouble() * (maximum - minimum) + minimum;
    }

    // You can set the hash value of this object to be whatever you want
    // This makes it great for testing special cases.
    public class HashObject
    {
        private readonly int hash, data;

        public HashObject() { }

        public HashObject(int hash, int data)
        {
            this.hash = hash;
            this.data = data;
        }

        public override int GetHashCode() => hash;

        public override bool Equals(object? obj)
        {
            var ho = (HashObject)obj;
            return GetHashCode() == ho.GetHashCode() && data == ho.data;
        }
    }
}