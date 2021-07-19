﻿using System;
using System.Collections.Generic;
using DataStructures.HashTable;
using Xunit;

namespace DataStructuresTest.HashTableTest
{
    public class HashTableQuadraticProbingTest
    {
        private const int Loops = 500;
        private static readonly int MaxSize = TestUtil.GetRandomInteger(1, 750);
        private static readonly int MaxRandNum = TestUtil.GetRandomInteger(1, 750);
        private HashTableQuadraticProbing<int, int> hashTableQuadraticProbing;

        public HashTableQuadraticProbingTest() => hashTableQuadraticProbing = new HashTableQuadraticProbing<int, int>();

        [Fact]
        public void TestNullKey()
        {
            var map2 = new HashTableQuadraticProbing<int?, int?>();
            Assert.Throws<ArgumentException>(() => map2.Add(null, 5));
        }

        [Fact]
        public void TestIllegalCreation1()
        {
            var exception = Assert.Throws<ArgumentException>(() => new HashTableQuadraticProbing<int, int>(-3, 0.5));
            Assert.Equal("Illegal capacity: " + -3, exception.Message);
        }

        [Fact]
        public void TestIllegalCreation2()
        {
            var exception = Assert.Throws<ArgumentException>(() => new HashTableQuadraticProbing<int, int>(5, double.PositiveInfinity));
            Assert.Equal("Illegal loadFactor: " + double.PositiveInfinity, exception.Message);
        }

        [Fact]
        public void TestIllegalCreation3()
        {
            var exception = Assert.Throws<ArgumentException>(() => new HashTableQuadraticProbing<int, int>(6, -0.5));
            Assert.Equal("Illegal loadFactor: " + -0.5, exception.Message);
        }

        [Fact]
        public void TestUpdatingValue()
        {
            hashTableQuadraticProbing.Add(1, 1);
            Assert.Equal(1, hashTableQuadraticProbing.Get(1));

            hashTableQuadraticProbing.Add(1, 5);
            Assert.Equal(5, hashTableQuadraticProbing.Get(1));

            hashTableQuadraticProbing.Add(1, -7);
            Assert.Equal(-7, hashTableQuadraticProbing.Get(1));
        }

        [Fact]
        public void TestTableSize()
        {
            const int loops = 10000;
            for (var sz = 1; sz <= 32; sz++)
            {
                var ht = new HashTableQuadraticProbing<int, int>(sz);
                for (var i = 0; i < loops; i++)
                {
                    AssertCapacityIsPowerOfTwo(ht);
                    ht.Add(i, i);
                }
            }
        }

        [Fact]
        public void TestIterator()
        {
            var dictionary = new Dictionary<int, int>();

            for (var loop = 0; loop < Loops; loop++)
            {
                hashTableQuadraticProbing.Clear();
                dictionary.Clear();
                Assert.True(hashTableQuadraticProbing.IsEmpty);

                hashTableQuadraticProbing = new HashTableQuadraticProbing<int, int>();

                var randNums = TestUtil.GenRandList(MaxSize);

                foreach (var key in randNums)
                {
                    if (dictionary.ContainsKey(key))
                        dictionary[key] = key;
                    else
                        dictionary.Add(key, key);

                    hashTableQuadraticProbing.Add(key, key);
                    Assert.Equal(dictionary[key], hashTableQuadraticProbing.Get(key));
                }
                var count = 0;
                foreach (var key in hashTableQuadraticProbing)
                {
                    Assert.Equal(key, hashTableQuadraticProbing.Get(key));
                    Assert.Equal(dictionary[key], hashTableQuadraticProbing.Get(key));
                    Assert.True(hashTableQuadraticProbing.HasKey(key));
                    Assert.Contains(key, randNums);
                    count++;
                }

                foreach (var key in dictionary.Keys)
                    Assert.Equal(key, hashTableQuadraticProbing.Get(key));

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
                var hashTable = new HashTableQuadraticProbing<int, int>();

                // Add some random values
                var keysSet = new HashSet<int>();
                for (var i = 0; i < MaxSize; i++)
                {
                    var randomInteger = TestUtil.GetRandomInteger(-MaxRandNum, MaxRandNum);
                    keysSet.Add(randomInteger);
                    hashTable.Add(randomInteger, 5);
                }

                Assert.Equal(keysSet.Count, hashTable.Count);

                var keys = hashTable.GetKeys();
                foreach (var key in keys)
                    hashTable.Remove(key);

                Assert.True(hashTable.IsEmpty);
            }
        }

        [Fact]
        public void RemoveTest()
        {
            // Add three elements
            var hashTable = new HashTableQuadraticProbing<int, int>(7)
            {
                    {11, 0},
                    {12, 0},
                    {13, 0}
            };

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
            var hashTable = new HashTableQuadraticProbing<HashObject, int>();

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
                hashTableQuadraticProbing.Clear();
                dictionary.Clear();
                Assert.Equal(hashTableQuadraticProbing.Count, dictionary.Count);

                hashTableQuadraticProbing = new HashTableQuadraticProbing<int, int>();

                var probability1 = TestUtil.GetRandomDouble(double.MinValue, double.MaxValue);
                var probability2 = TestUtil.GetRandomDouble(double.MinValue, double.MaxValue);

                var nums = TestUtil.GenRandList(MaxSize);
                for (var i = 0; i < MaxSize; i++)
                {
                    var randomDouble = TestUtil.GetRandomDouble(double.MinValue, double.MinValue);

                    var key = nums[i];
                    var val = i;

                    if (randomDouble < probability1)
                    {
                        if (dictionary.ContainsKey(key))
                            dictionary[key] = val;
                        else
                            dictionary.Add(key, val);

                        hashTableQuadraticProbing.Add(key, val);
                        Assert.Equal(dictionary[key], hashTableQuadraticProbing.Get(key));
                    }

                    dictionary.TryGetValue(key, out var value1);
                    Assert.Equal(value1, hashTableQuadraticProbing.Get(key));
                    Assert.Equal(dictionary.ContainsKey(key), hashTableQuadraticProbing.ContainsKey(key));
                    Assert.Equal(dictionary.Count, hashTableQuadraticProbing.Count);

                    if (randomDouble > probability2)
                    {
                        dictionary.Remove(key, out val);
                        Assert.Equal(hashTableQuadraticProbing.Remove(key), val);
                    }
                    dictionary.TryGetValue(key, out var value2);
                    Assert.Equal(value2, hashTableQuadraticProbing.Get(key));
                    Assert.Equal(dictionary.ContainsKey(key), hashTableQuadraticProbing.ContainsKey(key));
                    Assert.Equal(dictionary.Count, hashTableQuadraticProbing.Count);
                }
            }
        }

        [Fact]
        public void RandomIteratorTests()
        {
            var hashTable = new HashTableQuadraticProbing<int, LinkedList<int>>();
            var dictionary = new Dictionary<int, LinkedList<int>>();

            for (var loop = 0; loop < Loops; loop++)
            {
                hashTable.Clear();
                dictionary.Clear();
                Assert.Equal(hashTableQuadraticProbing.Count, dictionary.Count);

                var sz = TestUtil.GetRandomInteger(1, MaxSize);
                hashTable = new HashTableQuadraticProbing<int, LinkedList<int>>(sz);
                dictionary = new Dictionary<int, LinkedList<int>>(sz);

                var probability = TestUtil.GetRandomDouble(double.MinValue, double.MaxValue);

                for (var i = 0; i < MaxSize; i++)
                {
                    var index = TestUtil.GetRandomInteger(0, MaxSize - 1);
                    var l1 = hashTable.Get(index);
                    dictionary.TryGetValue(index, out var l2);

                    if (l2 == null)
                    {
                        l1 = new LinkedList<int>();
                        l2 = new LinkedList<int>();
                        hashTable.Add(index, l1);
                        dictionary.Add(index, l2);
                    }

                    var randVal = TestUtil.GetRandomInteger(-MaxSize, MaxSize);

                    if (TestUtil.GetRandomDouble(double.MinValue, double.MaxValue) < probability)
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

        private void AssertCapacityIsPowerOfTwo(HashTableQuadraticProbing<int, int> ht)
        {
            var sz = ht.Capacity;
            if (sz == 0) return;
            var f = sz & (sz - 1);
            Assert.Equal(0, f);
        }
    }
}