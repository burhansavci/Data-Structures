using System;
using System.Numerics;

namespace DataStructures.HashTable
{
    public class HashTableQuadraticProbing<TKey, TValue> : HashTableOpenAddressingBase<TKey, TValue> where TKey : new()
    {
        public HashTableQuadraticProbing() { }

        public HashTableQuadraticProbing(int capacity) : base(capacity) { }

        public HashTableQuadraticProbing(int capacity, double loadFactor) : base(capacity, loadFactor) { }

        // Given a number this method finds the next
        // power of two above this value.
        private static int NextPowerOfTwo(int n) => (n == 0 ? 0 : 1 << BitOperations.Log2((uint)n)) << 1;

        protected override void SetupProbing(TKey key) { }

        // Increase the capacity of the hashtable to the next power of two.
        protected override int Probe(int x)
        {
            // Quadratic probing function (x^2+x)/2
            return (x * x + x) >> 1;
        }

        // Increase the capacity of the hashtable to the next power of two.
        protected override void IncreaseCapacity() => Capacity = NextPowerOfTwo(Capacity);

        protected override void AdjustCapacity()
        {
            var pow2 = Capacity == 0 ? 0 : 1 << BitOperations.Log2((uint)Capacity);
            if (Capacity == pow2) return;
            IncreaseCapacity();
        }
    }
}