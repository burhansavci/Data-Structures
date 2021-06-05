using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Queue
{
    public class LinkedListQueue<T> : IEnumerable<T>, IQueue<T>
    {
        private readonly LinkedList<T> linkedList = new();

        public void Enqueue(T elem) => linkedList.AddLast(elem);

        public T Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue is empty");

            var firstElem = linkedList.First;
            linkedList.RemoveFirst();
            return firstElem!.Value;
        }

        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue is empty");

            return linkedList.First!.Value;
        }

        public int Size() => linkedList.Count;

        public bool IsEmpty() => Size() == 0;
        
        public IEnumerator<T> GetEnumerator() => linkedList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}