using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.PriorityQueue
{
    public class PriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
    {
        // A dynamic list to track the elements inside the heap
        private readonly List<T> heap;

        // Construct and initially empty priority queue
        public PriorityQueue() : this(1) { }

        // Construct a priority queue with an initial capacity
        public PriorityQueue(int size)
        {
            heap = new List<T>(size);
        }

        // Construct a priority queue using heapify in O(n) time, a great explanation can be found at:
        // http://www.cs.umd.edu/~meesh/351/mount/lectures/lect14-heapsort-analysis-part.pdf
        public PriorityQueue(T[] elems)
        {
            int heapSize = elems.Length;
            heap = new List<T>(heapSize);

            // Place all element in heap
            for (int i = 0; i < heapSize; i++) heap.Add(elems[i]);

            // Heapify process, O(n)
            for (int i = Math.Max(0, (heapSize / 2) - 1); i >= 0; i--) SiftDown(i);
        }

        // Priority queue construction, O(n)
        public PriorityQueue(ICollection<T> elems)
        {
            var heapSize = elems.Count;
            heap = new List<T>(heapSize);

            // Add all elements of the given collection to the heap
            heap.AddRange(elems);

            // Heapify process, O(n)
            for (var i = Math.Max(0, (heapSize / 2) - 1); i >= 0; i--) SiftDown(i);
        }

        // Return the count of the heap
        public int Count => heap.Count;

        // Returns true/false depending on if the priority queue is empty
        public bool IsEmpty => (Count == 0);

        // Clears everything inside the heap, O(n)
        public void Clear() => heap.Clear();

        // Returns the value of the element with the lowest
        // priority in this priority queue. If the priority
        // queue is empty default is returned.
        public T Peek() => IsEmpty ? default : heap[0];

        // Removes the root of the heap, O(log(n))
        public T Poll() => RemoveAt(0);

        // Test if an element is in heap, O(n)
        public bool Contains(T elem)
        {
            // Linear scan to check containment
            for (var i = 0; i < Count; i++)
                if (heap[i].Equals(elem))
                    return true;
            return false;
        }

        // Adds an element to the priority queue, the
        // element must not be null, O(log(n))
        public void Add(T elem)
        {
            if (elem == null) throw new ArgumentNullException(nameof(elem), "elem cannot be null");

            heap.Add(elem);

            var indexOfLastElem = Count - 1;
            SiftUp(indexOfLastElem);
        }


        // Perform bottom up node sift up, O(log(n))
        private void SiftUp(int k)
        {
            // Grab the index of the next parent node WRT to k
            var parent = (k - 1) / 2;

            // Keep swimming while we have not reached the
            // root and while we're less than our parent.
            while (k > 0 && Less(k, parent))
            {
                // Exchange k with the parent
                Swap(parent, k);
                k = parent;

                // Grab the index of the next parent node WRT to k
                parent = (k - 1) / 2;
            }
        }

        // Top down node sift down, O(log(n))
        private void SiftDown(int k)
        {
            while (true)
            {
                var left = 2 * k + 1; // Left  node
                var right = 2 * k + 2; // Right node
                var smallest = left; // Assume left is the smallest node of the two children

                // Find which is smaller left or right
                // If right is smaller set smallest to be right
                if (right < Count && Less(right, left)) smallest = right;

                // Stop if we're outside the bounds of the tree
                // or stop early if we cannot sink k anymore
                if (left >= Count || Less(k, smallest)) break;

                // Move down the tree following the smallest node
                Swap(smallest, k);
                k = smallest;
            }
        }

        // Removes a particular element in the heap, O(n)
        public bool Remove(T element)
        {
            if (element == null) return false;

            // Linear removal via search, O(n)
            for (var i = 0; i < Count; i++)
            {
                if (element.Equals(heap[i]))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        // Removes a node at particular index, O(log(n))
        private T RemoveAt(int i)
        {
            if (IsEmpty) return default;

            var indexOfLastElem = Count - 1;
            var removedData = heap[i];
            Swap(i, indexOfLastElem);

            // Obliterate the value
            heap.RemoveAt(indexOfLastElem);

            // Check if the last element was removed
            if (i == indexOfLastElem) return removedData;
            var elem = heap[i];

            // Try sinking element
            SiftDown(i);

            // If sinking did not work try swimming
            if (heap[i].Equals(elem)) SiftUp(i);
            return removedData;
        }

        // Tests if the value of node i <= node j
        // This method assumes i & j are valid indices, O(1)
        private bool Less(int i, int j)
        {
            var node1 = heap[i];
            var node2 = heap[j];
            return node1.CompareTo(node2) <= 0;
        }


        // Swap two nodes. Assumes i & j are valid, O(1)
        private void Swap(int i, int j)
        {
            var elemI = heap[i];
            var elemJ = heap[j];

            heap[i] = elemJ;
            heap[j] = elemI;
        }

        public IEnumerator<T> GetEnumerator() => heap.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}