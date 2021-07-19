using System;
using System.Numerics;

namespace DataStructures.HashTable
{
    public class HashTableDoubleHashing<TKey, TValue> : HashTableOpenAddressingBase<TKey, TValue> where TKey : ISecondaryHash, new()
    {
        private int hash;

        public HashTableDoubleHashing() { }

        public HashTableDoubleHashing(int capacity) : base(capacity) { }

        public HashTableDoubleHashing(int capacity, double loadFactor) : base(capacity, loadFactor) { }

        protected override void SetupProbing(TKey key)
        {
            // Cache second hash value.
            hash = NormalizeIndex(key.GetSecondaryHashCode());

            // Fail safe to avoid infinite loop.
            if (hash == 0) hash = 1;
        }

        protected override int Probe(int x) => x * hash;

        // Adjust the capacity until it is a prime number. The reason for
        // doing this is to help ensure that the GCD(hash, capacity) = 1 when
        // probing so that all the cells can be reached.
        protected override void AdjustCapacity() => Capacity = GetNextPrime(Capacity);

        private int GetNextPrime(int number)
        {
            while (true)
            {
                var isPrime = true;
                //increment the number by 1 each time
                number += 1;

                var squaredNumber = (int)Math.Sqrt(number);

                //start at 2 and increment by 1 until it gets to the squared number
                for (var i = 2; i <= squaredNumber; i++)
                {
                    //how do I check all i's?
                    if (number % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                    return number;
            }
        }
    }
}