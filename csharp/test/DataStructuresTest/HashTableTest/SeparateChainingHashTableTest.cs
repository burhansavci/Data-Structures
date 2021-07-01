using System;
using System.Collections.Generic;
using DataStructures.HashTable;
using Xunit;

namespace DataStructuresTest.HashTableTest
{
    public class SeparateChainingHashTableTest
    {
        private static readonly Random Random = new();
        private const int Loops = 500;
        private static readonly int MaxSize = GetRandomInteger(1, 750);
        private static readonly int MaxRandNum = GetRandomInteger(1, 350);

        private SeparateChainingHashTable<int, int> separateChainingHashTable;

        public SeparateChainingHashTableTest() => separateChainingHashTable = new SeparateChainingHashTable<int, int>();

        [Fact]
        public void TestNullKey()
        {
            var nullableMap = new SeparateChainingHashTable<int?, int?>();
            Assert.Throws<ArgumentException>(() => nullableMap.Add(null, 5));
        }

        [Fact]
        public void TestIllegalCreation1()
        {
            var exception = Assert.Throws<ArgumentException>(() => new SeparateChainingHashTable<int, int>(-3, 0.5));
            Assert.Equal("Illegal capacity", exception.Message);
        }

        [Fact]
        public void TestIllegalCreation2()
        {
            var exception = Assert.Throws<ArgumentException>(() => new SeparateChainingHashTable<int, int>(5, double.PositiveInfinity));
            Assert.Equal("Illegal maxLoadFactor", exception.Message);
        }

        [Fact]
        public void TestIllegalCreation3()
        {
            var exception = Assert.Throws<ArgumentException>(() => new SeparateChainingHashTable<int, int>(6, -0.5));
            Assert.Equal("Illegal maxLoadFactor", exception.Message);
        }

        [Fact]
        public void TestLegalCreation()
        {
            var exception = Record.Exception(() => new SeparateChainingHashTable<int, int>(6, 0.9));
            Assert.Null(exception);
        }

        [Fact]
        public void TestUpdatingValue()
        {
            separateChainingHashTable.Add(1, 1);
            Assert.Equal(1, separateChainingHashTable.Get(1));

            separateChainingHashTable.Add(1, 5);
            Assert.Equal(5, separateChainingHashTable.Get(1));

            separateChainingHashTable.Add(1, -7);
            Assert.Equal(-7, separateChainingHashTable.Get(1));
        }


        [Fact]
        public void TestIterator()
        {
            var dictionary = new Dictionary<int, int>();

            for (var loop = 0; loop < Loops; loop++)
            {
                separateChainingHashTable.Clear();
                dictionary.Clear();
                Assert.True(separateChainingHashTable.IsEmpty);

                separateChainingHashTable = new SeparateChainingHashTable<int, int>();

                var randNums = TestUtil.GenRandList(MaxSize);
                foreach (var key in randNums)
                {
                    dictionary.Add(key, key);
                    separateChainingHashTable.Add(key, key);
                    Assert.Equal(dictionary[key], separateChainingHashTable.Get(key));
                }

                var count = 0;
                foreach (var key in separateChainingHashTable)
                {
                    Assert.Equal(key, separateChainingHashTable.Get(key));
                    Assert.Equal(dictionary[key], separateChainingHashTable.Get(key));
                    Assert.True(separateChainingHashTable.HasKey(key));
                    Assert.Contains(key, randNums);
                    count++;
                }

                foreach (var key in dictionary.Keys)
                    Assert.Equal(key, separateChainingHashTable.Get(key));

                var set = new HashSet<int>(randNums);

                Assert.Equal(set.Count, count);
                Assert.Equal(dictionary.Count, count);
            }
        }

        [Fact]
        public void RandomRemove()
        {
            for (var loop = 0; loop < Loops; loop++)
            {
                var hashTable = new SeparateChainingHashTable<int, int>();

                // Add some random values
                var keysSet = new HashSet<int>();
                for (var i = 0; i < MaxSize; i++)
                {
                    var randomInteger = GetRandomInteger(-MaxRandNum, MaxRandNum);
                    keysSet.Add(randomInteger);
                    hashTable.Add(randomInteger, 5);
                }

                Assert.Equal(keysSet.Count, hashTable.Count);

                var keys = hashTable.Keys();
                foreach (var key in keys)
                {
                    hashTable.Remove(key);
                }

                Assert.True(hashTable.IsEmpty);
            }
        }

        [Fact]
        public void RemoveTest()
        {
            var hashTable = new SeparateChainingHashTable<int, int>(7)
            {
                    {11, 0},
                    {12, 0},
                    {13, 0}
            };

            // Add three elements
            Assert.Equal(3, hashTable.Count);

            // Add ten more
            for (var i = 1; i <= 10; i++) hashTable.Add(i, 0);
            Assert.Equal(13, hashTable.Count);

            // Remove ten
            for (var i = 1; i <= 10; i++) hashTable.Remove(i);
            Assert.Equal(3, hashTable.Count);

            // remove three
            hashTable.Remove(11);
            hashTable.Remove(12);
            hashTable.Remove(13);

            Assert.Equal(0, hashTable.Count);
        }

        [Fact]
        public void RemoveTestComplex1()
        {
            var hashTable = new SeparateChainingHashTable<HashObject, int>();

            var o1 = new HashObject(88, 1);
            var o2 = new HashObject(88, 2);
            var o3 = new HashObject(88, 3);
            var o4 = new HashObject(88, 4);

            hashTable.Add(o1, 111);
            hashTable.Add(o2, 111);
            hashTable.Add(o3, 111);
            hashTable.Add(o4, 111);

            hashTable.Remove(o2);
            hashTable.Remove(o3);
            hashTable.Remove(o1);
            hashTable.Remove(o4);

            Assert.Equal(0, hashTable.Count);
        }

        [Fact]
        public void TestRandomMapOperations()
        {
            var dictionary = new Dictionary<int, int>();

            for (var loop = 0; loop < Loops; loop++)
            {
                separateChainingHashTable.Clear();
                dictionary.Clear();
                Assert.Equal(separateChainingHashTable.Count, dictionary.Count);

                separateChainingHashTable = new SeparateChainingHashTable<int, int>();

                var probability1 = GetRandomDouble(double.MinValue, double.MaxValue);
                var probability2 = GetRandomDouble(double.MinValue, double.MaxValue);

                var nums = TestUtil.GenRandList(MaxSize);
                for (var i = 0; i < MaxSize; i++)
                {
                    var randomDouble = GetRandomDouble(double.MinValue, double.MinValue);

                    var key = nums[i];
                    var val = i;

                    if (randomDouble < probability1)
                    {
                        dictionary.Add(key, val);
                        separateChainingHashTable.Add(key, val);
                        Assert.Equal(dictionary[key], separateChainingHashTable.Get(key));
                    }

                    Assert.Equal(dictionary[key], separateChainingHashTable.Get(key));
                    Assert.Equal(dictionary.ContainsKey(key), separateChainingHashTable.ContainsKey(key));
                    Assert.Equal(dictionary.Count, separateChainingHashTable.Count);

                    if (randomDouble > probability2)
                    {
                        dictionary.Remove(key, out val);
                        Assert.Equal(separateChainingHashTable.Remove(key), val);
                    }

                    Assert.Equal(dictionary[key], separateChainingHashTable.Get(key));
                    Assert.Equal(dictionary.ContainsKey(key), separateChainingHashTable.ContainsKey(key));
                    Assert.Equal(dictionary.Count, separateChainingHashTable.Count);
                }
            }
        }

        [Fact]
        public void RandomIteratorTests()
        {
            var hashTable = new SeparateChainingHashTable<int, LinkedList<int>>();
            var dictionary = new Dictionary<int, LinkedList<int>>();

            for (var loop = 0; loop < Loops; loop++)
            {
                hashTable.Clear();
                dictionary.Clear();
                Assert.Equal(separateChainingHashTable.Count, dictionary.Count);

                var sz = GetRandomInteger(1, MaxSize);
                hashTable = new SeparateChainingHashTable<int, LinkedList<int>>(sz);
                dictionary = new Dictionary<int, LinkedList<int>>(sz);

                var probability = GetRandomDouble(double.MinValue, double.MaxValue);

                for (var i = 0; i < MaxSize; i++)
                {
                    var index = GetRandomInteger(0, MaxSize - 1);
                    var l1 = hashTable.Get(index);
                    dictionary.TryGetValue(index, out var l2);

                    if (l2 == null)
                    {
                        l1 = new LinkedList<int>();
                        l2 = new LinkedList<int>();
                        hashTable.Add(index, l1);
                        dictionary.Add(index, l2);
                    }

                    var randVal = GetRandomInteger(-MaxSize, MaxSize);

                    if (GetRandomDouble(double.MinValue, double.MaxValue) < probability)
                    {
                        l1.Remove(randVal);
                        l2.Remove(randVal);
                    }
                    else
                    {
                        l1.AddLast(randVal);
                        l2.AddLast(randVal);
                    }

                    Assert.Equal(hashTable.Count, dictionary.Count);
                    Assert.Equal(l1, l2);
                }
            }
        }

        private static int GetRandomInteger(int min, int max) => Random.Next(max - min + 1) + min;

        private static double GetRandomDouble(double minimum, double maximum) => Random.NextDouble() * (maximum - minimum) + minimum;
        
        private class HashObject
        {
            private readonly int hash;
            private readonly int data;

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
}