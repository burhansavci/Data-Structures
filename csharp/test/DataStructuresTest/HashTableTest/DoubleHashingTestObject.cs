using System;
using DataStructures.HashTable;

namespace DataStructuresTest.HashTableTest
{
    public class DoubleHashingTestObject : ISecondaryHash
    {
        private int hash;
        private int hash2;
        private readonly int intData;
        private readonly int[] vectorData;
        private readonly string stringData;

        private static long[] RandomVector;
        private static readonly Random Rand = new();
        private const int MaxVectorSize = 100000;
        private readonly bool isIntData;

        public DoubleHashingTestObject()
        {
            RandomVector = new long[MaxVectorSize];
            for (var i = 0; i < MaxVectorSize; i++)
            {
                var val = LongRandom(long.MinValue, long.MaxValue, Rand);
                while (val % 2 == 0) val = LongRandom(long.MinValue, long.MaxValue, Rand);
                RandomVector[i] = val;
            }
        }

        public DoubleHashingTestObject(int data)
        {
            intData = data;
            isIntData = true;
            IntHash();
            ComputeHash();
        }

        public DoubleHashingTestObject(int[] data)
        {
            vectorData = data ?? throw new ArgumentException("Cannot be null");
            VectorHash();
            ComputeHash();
        }

        public DoubleHashingTestObject(string data)
        {
            stringData = data ?? throw new ArgumentException("Cannot be null");
            StringHash();
            ComputeHash();
        }

        private void IntHash() => hash2 = intData;

        private void VectorHash()
        {
            for (var i = 0; i < vectorData.LongLength; i++)
            {
                hash2 += (int)(RandomVector[i] * vectorData[i]);
            }
        }

        private void StringHash()
        {
            // Multipicative hash function:
            const int initialValue = 0;
            var prime = 17;
            var power = 1;
            hash = initialValue;
            foreach (int ch in stringData)
            {
                hash2 += power * ch;
                power *= prime;
            }
        }

        private void ComputeHash()
        {
            if (isIntData)
            {
                hash = intData.GetHashCode();
            }
            else if (stringData != null)
            {
                hash = stringData.GetHashCode();
            }
            else
            {
                hash = vectorData.GetHashCode();
            }
        }

        public override int GetHashCode() => hash;

        public int GetSecondaryHashCode() => hash2;

        public override bool Equals(object? o)
        {
            var obj = (DoubleHashingTestObject)o;
            if (obj != null && hash != obj.hash) return false;
            if (isIntData) return obj != null && intData.Equals(obj.intData);
            return obj != null && (vectorData != null ? Array.Equals(vectorData, obj.vectorData) : stringData.Equals(obj.stringData));
        }

        private static long LongRandom(long min, long max, Random rand)
        {
            var buf = new byte[8];
            rand.NextBytes(buf);
            var longRand = BitConverter.ToInt64(buf, 0);

            return Math.Abs(longRand % (max - min)) + min;
        }
    }
}