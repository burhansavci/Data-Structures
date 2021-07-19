namespace DataStructures.HashTable
{
    public class HashTableLinearProbing<TKey, TValue> : HashTableOpenAddressingBase<TKey, TValue> where TKey : new()
    {
        // This is the linear constant used in the linear probing, it can be
        // any positive number. The table capacity will be adjusted so that
        // the GCD(capacity, LINEAR_CONSTANT) = 1 so that all buckets can be probed.
        private const int LinearConstant = 17;

        public HashTableLinearProbing() { }

        public HashTableLinearProbing(int capacity) : base(capacity) { }

        public HashTableLinearProbing(int capacity, double loadFactor) : base(capacity, loadFactor) { }

        protected override void SetupProbing(TKey key) { }

        protected override int Probe(int x)
        {
            return LinearConstant * x;
        }

        // Adjust the capacity so that the linear constant and
        // the table capacity are relatively prime.
        protected override void AdjustCapacity()
        {
            while (Gcd(LinearConstant, Capacity) != 1) Capacity++;
        }
    }
}