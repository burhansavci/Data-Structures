using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.HashTable;
using Xunit;

namespace DataStructuresTest.HashTableTest
{
    public class HashTableDoubleHashingTest
    {
        private const int Loops = 500;
        private static readonly int MaxSize = TestUtil.GetRandomInteger(1, 750);
        private static readonly int MaxRandNum = TestUtil.GetRandomInteger(1, 750);
        private HashTableDoubleHashing<DoubleHashingTestObject, int> hashTableDoubleHashing;

        public HashTableDoubleHashingTest() => hashTableDoubleHashing = new HashTableDoubleHashing<DoubleHashingTestObject, int>();

        [Fact]
        public void TestNullKey() => Assert.Throws<ArgumentException>(() => hashTableDoubleHashing.Add(null, 5));

        [Fact]
        public void TestIllegalCreation1()
        {
            var exception = Assert.Throws<ArgumentException>(() => new HashTableDoubleHashing<DoubleHashingTestObject, int>(-3, 0.5));
            Assert.Equal("Illegal capacity: " + -3, exception.Message);
        }

        [Fact]
        public void TestIllegalCreation2()
        {
            var exception =
                    Assert.Throws<ArgumentException>(() => new HashTableDoubleHashing<DoubleHashingTestObject, int>(5, double.PositiveInfinity));
            Assert.Equal("Illegal loadFactor: " + double.PositiveInfinity, exception.Message);
        }

        [Fact]
        public void TestIllegalCreation3()
        {
            var exception = Assert.Throws<ArgumentException>(() => new HashTableDoubleHashing<DoubleHashingTestObject, int>(6, -0.5));
            Assert.Equal("Illegal loadFactor: " + -0.5, exception.Message);
        }

        [Fact]
        public void TestUpdatingValue()
        {
            var o1 = new DoubleHashingTestObject(1);
            var o5 = new DoubleHashingTestObject(5);
            var on7 = new DoubleHashingTestObject(-7);

            hashTableDoubleHashing.Add(o1, 1);
            Assert.Equal(1, hashTableDoubleHashing.Get(o1));

            hashTableDoubleHashing.Add(o5, 5);
            Assert.Equal(5, hashTableDoubleHashing.Get(o5));

            hashTableDoubleHashing.Add(on7, -7);
            Assert.Equal(-7, hashTableDoubleHashing.Get(on7));
        }

        [Fact]
        public void TestIterator()
        {
            var dictionary = new Dictionary<DoubleHashingTestObject, DoubleHashingTestObject>();
            var doubleHashing = new HashTableDoubleHashing<DoubleHashingTestObject, DoubleHashingTestObject>();
            for (var loop = 0; loop < Loops; loop++)
            {
                doubleHashing.Clear();
                dictionary.Clear();
                Assert.True(doubleHashing.IsEmpty);

                var randNums = TestUtil.GenRandList(MaxSize).Select(x => new DoubleHashingTestObject(x)).ToList();

                foreach (var key in randNums)
                {
                    if (dictionary.ContainsKey(key))
                        dictionary[key] = key;
                    else
                        dictionary.Add(key, key);

                    doubleHashing.Add(key, key);
                    Assert.Equal(dictionary[key], doubleHashing.Get(key));
                }

                var count = 0;
                foreach (var key in doubleHashing)
                {
                    Assert.Equal(key, doubleHashing.Get(key));
                    Assert.Equal(dictionary[key], doubleHashing.Get(key));
                    Assert.True(doubleHashing.HasKey(key));
                    Assert.Contains(key, randNums);
                    count++;
                }

                foreach (var key in dictionary.Keys)
                    Assert.Equal(key, doubleHashing.Get(key));

                var set = new HashSet<DoubleHashingTestObject>(randNums);

                Assert.Equal(set.Count, count);
                Assert.Equal(dictionary.Count, count);
            }
        }

        [Fact]
        public void RandomRemove()
        {
            for (var loop = 0; loop < Loops; loop++)
            {
                var hashTable = new HashTableDoubleHashing<DoubleHashingTestObject, int>();

                // Add some random values
                var keysSet = new HashSet<DoubleHashingTestObject>();
                for (var i = 0; i < MaxSize; i++)
                {
                    var randomInteger = TestUtil.GetRandomInteger(-MaxRandNum, MaxRandNum);
                    var obj = new DoubleHashingTestObject(randomInteger);
                    keysSet.Add(obj);
                    hashTable.Add(obj, 5);
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
            var o11 = new DoubleHashingTestObject(11);
            var o12 = new DoubleHashingTestObject(12);
            var o13 = new DoubleHashingTestObject(13);

            // Add three elements
            var hashTable = new HashTableDoubleHashing<DoubleHashingTestObject, int>(7)
            {
                    {o11, 0},
                    {o12, 0},
                    {o13, 0}
            };

            Assert.Equal(3, hashTable.Count);

            // Add ten more
            for (var i = 1; i <= 10; i++) hashTable.Add(new DoubleHashingTestObject(i), 0);
            Assert.Equal(13, hashTable.Count);

            // Remove ten
            for (var i = 1; i <= 10; i++) hashTable.Remove(new DoubleHashingTestObject(i));
            Assert.Equal(3, hashTable.Count);

            // remove three
            hashTable.Remove(o11);
            hashTable.Remove(o12);
            hashTable.Remove(o13);

            Assert.Equal(0, hashTable.Count);
        }

        [Fact]
        public void TestRandomMapOperations()
        {
            var dictionary = new Dictionary<DoubleHashingTestObject, int>();

            for (var loop = 0; loop < Loops; loop++)
            {
                hashTableDoubleHashing.Clear();
                dictionary.Clear();
                Assert.Equal(hashTableDoubleHashing.Count, dictionary.Count);

                hashTableDoubleHashing = new HashTableDoubleHashing<DoubleHashingTestObject, int>();

                var probability1 = TestUtil.GetRandomDouble(double.MinValue, double.MaxValue);
                var probability2 = TestUtil.GetRandomDouble(double.MinValue, double.MaxValue);

                var nums = TestUtil.GenRandList(MaxSize).Select(x => new DoubleHashingTestObject(x)).ToList();
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

                        hashTableDoubleHashing.Add(key, val);
                        Assert.Equal(dictionary[key], hashTableDoubleHashing.Get(key));
                    }

                    dictionary.TryGetValue(key, out var value1);
                    Assert.Equal(value1, hashTableDoubleHashing.Get(key));
                    Assert.Equal(dictionary.ContainsKey(key), hashTableDoubleHashing.ContainsKey(key));
                    Assert.Equal(dictionary.Count, hashTableDoubleHashing.Count);

                    if (randomDouble > probability2)
                    {
                        dictionary.Remove(key, out val);
                        Assert.Equal(hashTableDoubleHashing.Remove(key), val);
                    }
                    dictionary.TryGetValue(key, out var value2);
                    Assert.Equal(value2, hashTableDoubleHashing.Get(key));
                    Assert.Equal(dictionary.ContainsKey(key), hashTableDoubleHashing.ContainsKey(key));
                    Assert.Equal(dictionary.Count, hashTableDoubleHashing.Count);
                }
            }
        }

        [Fact]
        public void RandomIteratorTests()
        {
            var hashTable = new HashTableDoubleHashing<DoubleHashingTestObject, LinkedList<int>>();
            var dictionary = new Dictionary<DoubleHashingTestObject, LinkedList<int>>();

            for (var loop = 0; loop < Loops; loop++)
            {
                hashTable.Clear();
                dictionary.Clear();
                Assert.Equal(hashTableDoubleHashing.Count, dictionary.Count);

                var sz = TestUtil.GetRandomInteger(1, MaxSize);
                hashTable = new HashTableDoubleHashing<DoubleHashingTestObject, LinkedList<int>>(sz);
                dictionary = new Dictionary<DoubleHashingTestObject, LinkedList<int>>(sz);

                var probability = TestUtil.GetRandomDouble(double.MinValue, double.MaxValue);

                for (var i = 0; i < MaxSize; i++)
                {
                    var index = new DoubleHashingTestObject(TestUtil.GetRandomInteger(0, MaxSize - 1));
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
    }
}