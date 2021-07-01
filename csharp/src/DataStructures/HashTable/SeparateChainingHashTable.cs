using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.HashTable
{
    public class SeparateChainingHashTable<TKey, TValue> : IEnumerable<TKey>
    {
        private class Entry<TEntryKey, TEntryValue>
        {
            public readonly int Hash;
            public TEntryKey Key;
            public TEntryValue Value;

            public Entry(TEntryKey key, TEntryValue value)
            {
                Key = key;
                Value = value;
                Hash = key.GetHashCode();
            }
        }

        private static readonly int DefaultCapacity = 3;
        private static readonly double DefaultLoadFactor = 0.75;

        private readonly double maxLoadFactor;
        private int capacity, threshold;
        private LinkedList<Entry<TKey, TValue>>[] table;

        public SeparateChainingHashTable() : this(DefaultCapacity, DefaultLoadFactor) { }

        public SeparateChainingHashTable(int capacity) : this(capacity, DefaultLoadFactor) { }

        // Designated constructor
        public SeparateChainingHashTable(int capacity, double maxLoadFactor)
        {
            if (capacity < 0) throw new ArgumentException("Illegal capacity");
            if (maxLoadFactor is <= 0 or double.NaN || !double.IsFinite(maxLoadFactor))
                throw new ArgumentException("Illegal maxLoadFactor");
            this.maxLoadFactor = maxLoadFactor;
            this.capacity = Math.Max(DefaultCapacity, capacity);
            threshold = (int)(this.capacity * maxLoadFactor);
            table = new LinkedList<Entry<TKey, TValue>>[this.capacity];
        }

        // Returns the number of elements currently inside the hash-table
        public int Count { get; private set; }

        // Returns true/false depending on whether the hash-table is empty
        public bool IsEmpty => Count == 0;

        // Converts a hash value to an index. Essentially, this strips the
        // negative sign and places the hash value in the domain [0, capacity)
        private int NormalizeIndex(int keyHash)
        {
            return (keyHash & 0x7FFFFFFF) % capacity;
        }

        // Clears all the contents of the hash-table
        public void Clear()
        {
            Array.Fill(table, null);
            Count = 0;
        }

        public bool ContainsKey(TKey key)
        {
            return HasKey(key);
        }

        // Returns true/false depending on whether a key is in the hash table
        public bool HasKey(TKey key)
        {
            var bucketIndex = NormalizeIndex(key.GetHashCode());
            return BucketSeekEntry(bucketIndex, key) != null;
        }

        // Insert, put and add all place a value in the hash-table
        public TValue Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentException("Null key");
            var newEntry = new Entry<TKey, TValue>(key, value);
            var bucketIndex = NormalizeIndex(newEntry.Hash);
            return BucketInsertEntry(bucketIndex, newEntry);
        }

        // Gets a key's values from the map and returns the value.
        // NOTE: returns default if the value is null AND also returns
        // default if the key does not exists, so watch out..
        public TValue Get(TKey key)
        {
            if (key == null) return default;
            var bucketIndex = NormalizeIndex(key.GetHashCode());
            var entry = BucketSeekEntry(bucketIndex, key);
            return entry != null ? entry.Value : default;
        }

        // Removes a key from the map and returns the value.
        // NOTE: returns default if the value is null AND also returns
        // default if the key does not exists.
        public TValue Remove(TKey key)
        {
            if (key == null) return default;
            var bucketIndex = NormalizeIndex(key.GetHashCode());
            return BucketRemoveEntry(bucketIndex, key);
        }

        // Removes an entry from a given bucket if it exists
        private TValue BucketRemoveEntry(int bucketIndex, TKey key)
        {
            var entry = BucketSeekEntry(bucketIndex, key);
            if (entry == null) return default;
            var links = table[bucketIndex];
            links.Remove(entry);
            --Count;
            return entry.Value;
        }

        // Inserts an entry in a given bucket only if the entry does not already
        // exist in the given bucket, but if it does then update the entry value
        private TValue BucketInsertEntry(int bucketIndex, Entry<TKey, TValue> entry)
        {
            var bucket = table[bucketIndex];
            if (bucket == null) table[bucketIndex] = bucket = new LinkedList<Entry<TKey, TValue>>();

            var existentEntry = BucketSeekEntry(bucketIndex, entry.Key);
            if (existentEntry == null)
            {
                bucket.AddLast(entry);
                if (++Count > threshold) ResizeTable();
                return default; // Use default to indicate that there was no previous entry
            }
            var oldVal = existentEntry.Value;
            existentEntry.Value = entry.Value;
            return oldVal;
        }

        // Finds and returns a particular entry in a given bucket if it exists, returns null otherwise
        private Entry<TKey, TValue> BucketSeekEntry(int bucketIndex, TKey key)
        {
            if (key == null) return null;
            var bucket = table[bucketIndex];
            return bucket?.FirstOrDefault(entry => entry.Key.Equals(key));
        }

        // Resizes the internal table holding buckets of entries
        private void ResizeTable()
        {
            capacity *= 2;
            threshold = (int)(capacity * maxLoadFactor);

            var newTable = new LinkedList<Entry<TKey, TValue>>[capacity];

            for (var i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    foreach (var entry in table[i])
                    {
                        var bucketIndex = NormalizeIndex(entry.Hash);
                        var bucket = newTable[bucketIndex];
                        if (bucket == null) newTable[bucketIndex] = bucket = new LinkedList<Entry<TKey, TValue>>();
                        bucket.AddLast(entry);
                    }

                    // Avoid memory leak. Help the GC
                    table[i].Clear();
                    table[i] = null;
                }
            }

            table = newTable;
        }

        // Returns the list of keys found within the hash table
        public List<TKey> Keys()
        {
            var keys = new List<TKey>(Count);
            keys.AddRange(table.Where(bucket => bucket != null).SelectMany(bucket => bucket, (_, entry) => entry.Key));
            return keys;
        }

        // Returns the list of values found within the hash table
        public List<TValue> Values()
        {
            var values = new List<TValue>(Count);
            values.AddRange(table.Where(bucket => bucket != null).SelectMany(bucket => bucket, (_, entry) => entry.Value));
            return values;
        }

        // Return an iterator to iterate over all the keys in this map
        public IEnumerator<TKey> GetEnumerator() => new SeparateChainingHashTableEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class SeparateChainingHashTableEnumerator : IEnumerator<TKey>
        {
            private readonly SeparateChainingHashTable<TKey, TValue> separateChainingHashTable;
            private int bucketIndex;
            private IEnumerator bucketIter;

            public TKey Current =>  ((LinkedList<Entry<TKey, TValue>>.Enumerator)bucketIter).Current!.Key;
            object IEnumerator.Current => ((LinkedList<Entry<TKey, TValue>>.Enumerator)bucketIter).Current!.Key;

            public SeparateChainingHashTableEnumerator(SeparateChainingHashTable<TKey, TValue> separateChainingHashTable)
            {
                this.separateChainingHashTable = separateChainingHashTable;
                bucketIndex = 0;
                bucketIter = separateChainingHashTable.table[0]?.GetEnumerator();
            }

            public bool MoveNext()
            {
                // No iterator or the current iterator is empty
                if (bucketIter == null || !bucketIter.MoveNext())
                {
                    // Search next buckets until a valid iterator is found
                    while (++bucketIndex < separateChainingHashTable.capacity)
                    {
                        var bucket = separateChainingHashTable.table[bucketIndex];
                        if (bucket != null)
                        {
                            // Make sure this iterator actually has elements.
                            var nextIter = bucket.GetEnumerator();
                            if (nextIter.MoveNext())
                            {
                                bucketIter = nextIter;
                                break;
                            }
                        }
                    }
                }
                return bucketIndex < separateChainingHashTable.capacity;
            }

            public void Reset() => bucketIndex = 0;

            public void Dispose() { }
        }
    }
}