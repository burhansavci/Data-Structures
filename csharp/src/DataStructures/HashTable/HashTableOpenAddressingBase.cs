using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Double;

namespace DataStructures.HashTable
{
    public abstract class HashTableOpenAddressingBase<TKey, TValue> : IEnumerable<TKey> where TKey : notnull, new()
    {
        private class Entry<TEntryKey, TEntryValue>
        {
            public TEntryKey Key;
            public TEntryValue Value;

            public Entry(TEntryKey key, TEntryValue value)
            {
                Key = key;
                Value = value;
            }

            public bool IsTombstone { get; set; }
        }

        private Entry<TKey, TValue>[] table;
        private readonly double LoadFactor;
        private int Threshold;

        // 'usedBuckets' counts the total number of used buckets inside the hash-table (includes cells marked as deleted).
        private int UsedBuckets;

        private const int DefaultCapacity = 7;
        private const double DefaultLoadFactor = 0.65;

        protected HashTableOpenAddressingBase() : this(DefaultCapacity) { }

        // Designated constructor
        protected HashTableOpenAddressingBase(int capacity, double loadFactor = DefaultLoadFactor)
        {
            if (capacity <= 0) throw new ArgumentException("Illegal capacity: " + capacity);

            if (loadFactor is <= 0 or NaN || IsInfinity(loadFactor))
                throw new ArgumentException("Illegal loadFactor: " + loadFactor);

            LoadFactor = loadFactor;
            Capacity = Math.Max(DefaultCapacity, capacity);
            AdjustCapacity();
            Threshold = (int)(Capacity * loadFactor);

            table = new Entry<TKey, TValue>[Capacity];
        }

        // These three methods are used to dictate how the probing is to actually
        // occur for whatever open addressing scheme you are implementing.
        protected abstract void SetupProbing(TKey key);

        protected abstract int Probe(int x);

        // Adjusts the capacity of the hash table after it's been made larger.
        // This is important to be able to override because the size of the hashtable
        // controls the functionality of the probing function.
        protected abstract void AdjustCapacity();

        // Increases the capacity of the hash table.
        protected virtual void IncreaseCapacity() => Capacity = 2 * Capacity + 1;

        public void Clear()
        {
            Array.Fill(table, null);
            Count = UsedBuckets = 0;
        }

        // Returns the number of keys currently inside the hash-table
        public int Count { get; private set; }

        // Returns the capacity of the hashtable (used mostly for testing)
        public int Capacity { get; protected set; }

        // Returns true/false depending on whether the hash-table is empty
        public bool IsEmpty => Count == 0;

        // Returns true/false on whether a given key exists within the hash-table.
        public bool ContainsKey(TKey key) => HasKey(key);

        // Returns a list of keys found in the hash table
        public List<TKey> GetKeys() =>
                table.Where(entry => entry != null && entry.Key != null && !entry.IsTombstone).Select(entry => entry.Key).ToList();

        // Returns a list of non-unique values found in the hash table
        public List<TValue> GetValues() =>
                table.Where(entry => entry != null && entry.Key != null && !entry.IsTombstone).Select(entry => entry.Value).ToList();

        // Double the size of the hash-table
        protected void ResizeTable()
        {
            IncreaseCapacity();
            AdjustCapacity();

            Threshold = (int)(Capacity * LoadFactor);

            var oldEntryTable = new Entry<TKey, TValue>[Capacity];

            // Perform entry table pointer swap
            var entryTableTmp = table;
            table = oldEntryTable;
            oldEntryTable = entryTableTmp;

            // Reset the key count and buckets used since we are about to
            // re-insert all the keys into the hash-table.
            Count = UsedBuckets = 0;

            foreach (var entry in oldEntryTable)
                if (entry != null && !entry.IsTombstone)
                    Add(entry.Key, entry.Value);
        }

        // Converts a hash value to an index. Essentially, this strips the
        // negative sign and places the hash value in the domain [0, capacity)
        protected int NormalizeIndex(int keyHash) => (keyHash & 0x7FFFFFFF) % Capacity;

        // Finds the greatest common denominator of a and b.
        protected static int Gcd(int a, int b)
        {
            while (true)
            {
                if (b == 0) return a;
                var a1 = a;
                a = b;
                b = a1 % b;
            }
        }

        // Place a key-value pair into the hash-table. If the value already
        // exists inside the hash-table then the value is updated.
        public TValue Add(TKey key, TValue val)
        {
            if (key == null) throw new ArgumentException("Null key");
            if (UsedBuckets >= Threshold) ResizeTable();

            SetupProbing(key);
            var offset = NormalizeIndex(key.GetHashCode());
            for (int i = offset, tombstoneIndex = -1, x = 1;; i = NormalizeIndex(offset + Probe(x++)))
            {
                if (table[i] != null && table[i].IsTombstone) // The current slot was previously deleted
                {
                    if (tombstoneIndex == -1) tombstoneIndex = i;
                }
                else if (table[i] != null) // The current cell already contains a key
                {
                    // The key we're trying to insert already exists in the hash-table,
                    // so update its value with the most recent value
                    if (table[i].Key.Equals(key))
                    {
                        var oldValue = table[i].Value;
                        if (tombstoneIndex == -1)
                        {
                            table[i].Value = val;
                        }
                        else
                        {
                            table[i].IsTombstone = true;
                            table[tombstoneIndex].Key = key;
                            table[tombstoneIndex].Value = val;
                            table[tombstoneIndex].IsTombstone = false;
                        }
                        return oldValue;
                    }
                }
                else // Current cell is null so an insertion/update can occur
                {
                    // No previously encountered deleted buckets
                    if (tombstoneIndex == -1)
                    {
                        table[i] = new Entry<TKey, TValue>(key, val);
                        UsedBuckets++;
                        Count++;
                    }
                    else
                    {
                        // Previously seen deleted bucket. Instead of inserting
                        // the new element at i where the null element is insert
                        // it where the deleted token was found.
                        Count++;
                        table[tombstoneIndex].Key = key;
                        table[tombstoneIndex].Value = val;
                        table[tombstoneIndex].IsTombstone = false;
                    }
                    return default;
                }
            }
        }

        // Returns true/false on whether a given key exists within the hash-table
        public bool HasKey(TKey key)
        {
            if (key == null) throw new ArgumentException("Null key");

            SetupProbing(key);
            var offset = NormalizeIndex(key.GetHashCode());

            // Start at the original hash value and probe until we find a spot where our key
            // is or hit a null element in which case our element does not exist.
            for (int i = offset, tombstoneIndex = -1, x = 1;; i = NormalizeIndex(offset + Probe(x++)))
            {
                // Ignore deleted cells, but record where the first index
                // of a deleted cell is found to perform lazy relocation later.
                if (table[i] != null && table[i].IsTombstone)
                {
                    if (tombstoneIndex == -1) tombstoneIndex = i;
                }
                else if (table[i] != null) // We hit a non-null key, perhaps it's the one we're looking for.
                {
                    // The key we want is in the hash-table!
                    if (table[i].Key.Equals(key))
                    {
                        // If tombstoneIndex != -1 this means we previously encountered a deleted cell.
                        // We can perform an optimization by swapping the entries in cells
                        // i and tombstoneIndex so that the next time we search for this key it will be
                        // found faster. This is called lazy deletion/relocation.
                        if (tombstoneIndex != -1)
                        {
                            // Swap the key-value pairs of positions i and tombstoneIndex.
                            table[tombstoneIndex].Key = table[i].Key;
                            table[tombstoneIndex].Value = table[i].Value;
                            table[tombstoneIndex].IsTombstone = false;
                            table[i].IsTombstone = true;
                        }
                        return true;
                    }

                    // Key was not found in the hash-table :/
                }
                else return false;
            }
        }

        // Get the value associated with the input key.
        // NOTE: returns default if the value is default AND also returns
        // default if the key does not exists.
        public TValue Get(TKey key)
        {
            if (key == null) throw new ArgumentException("Null key");

            SetupProbing(key);
            var offset = NormalizeIndex(key.GetHashCode());

            // Start at the original hash value and probe until we find a spot where our key
            // is or we hit a null element in which case our element does not exist.
            for (int i = offset, tombstoneIndex = -1, x = 1;; i = NormalizeIndex(offset + Probe(x++)))
            {
                // Ignore deleted cells, but record where the first index
                // of a deleted cell is found to perform lazy relocation later.
                if (table[i] != null && table[i].IsTombstone)
                {
                    if (tombstoneIndex == -1) tombstoneIndex = i;
                }
                else if (table[i] != null) // We hit a non-null key, perhaps it's the one we're looking for.
                {
                    // The key we want is in the hash-table!
                    if (table[i].Key.Equals(key))
                    {
                        // If tombstoneIndex != -1 this means we previously encountered a deleted cell.
                        // We can perform an optimization by swapping the entries in cells
                        // i and tombstoneIndex so that the next time we search for this key it will be
                        // found faster. This is called lazy deletion/relocation.
                        if (tombstoneIndex != -1)
                        {
                            // Swap the key-value pairs of positions i and tombstoneIndex.
                            table[tombstoneIndex].Key = table[i].Key;
                            table[tombstoneIndex].Value = table[i].Value;
                            table[tombstoneIndex].IsTombstone = false;
                            table[i].IsTombstone = true;
                            return table[tombstoneIndex].Value;
                        }

                        return table[i].Value;
                    }

                    // Element was not found in the hash-table :/
                }
                else return default;
            }
        }

        // Removes a key from the map and returns the value.
        // NOTE: returns default if the value is default AND also returns
        // default if the key does not exists.
        public TValue Remove(TKey key)
        {
            if (key == null) throw new ArgumentException("Null key");

            SetupProbing(key);
            var offset = NormalizeIndex(key.GetHashCode());

            // Starting at the original hash probe until we find a spot where our key is
            // or we hit a null element in which case our element does not exist.
            for (int i = offset, x = 1;; i = NormalizeIndex(offset + Probe(x++)))
            {
                // Ignore deleted cells
                if (table[i] != null && table[i].IsTombstone) continue;

                // Key was not found in hash-table.
                if (table[i] == null) return default;

                // The key we want to remove is in the hash-table!
                if (table[i].Key.Equals(key))
                {
                    Count--;
                    table[i].IsTombstone = true;
                    return table[i].Value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<TKey> GetEnumerator() => new HashTableOpenAddressingEnumerator(this);

        private class HashTableOpenAddressingEnumerator : IEnumerator<TKey>
        {
            private readonly HashTableOpenAddressingBase<TKey, TValue> hashTableOpenAddressingBase;
            private int index;
            private int count;

            public HashTableOpenAddressingEnumerator(HashTableOpenAddressingBase<TKey, TValue> hashTableOpenAddressingBase)
            {
                this.hashTableOpenAddressingBase = hashTableOpenAddressingBase;
                count = hashTableOpenAddressingBase.Count;
            }

            public bool MoveNext()
            {
                if (count == 0)
                    return false;

                while (hashTableOpenAddressingBase.table[index] == null || hashTableOpenAddressingBase.table[index].IsTombstone)
                    index++;

                --count;
                return true;
            }

            public void Reset() => index = count = hashTableOpenAddressingBase.Count;

            public TKey Current => hashTableOpenAddressingBase.table[index++].Key;
            object IEnumerator.Current => hashTableOpenAddressingBase.table[index++].Key;

            public void Dispose() { }
        }
    }
}