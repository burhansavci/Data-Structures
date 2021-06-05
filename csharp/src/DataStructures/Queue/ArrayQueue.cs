using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Queue
{
    public class ArrayQueue<T> : IEnumerable<T>, IQueue<T>
    {
        private int size;
        private int capacity;
        private int head;
        private int tail;
        private T[] data;

        public ArrayQueue()
        {
            capacity = 16;
            data = new T[capacity];
        }

        public int Size() => size;

        public bool IsEmpty() => size == 0;

        public void Enqueue(T elem)
        {
            if (size == capacity)
                IncreaseCapacity();

            data[tail++] = elem;

            // Reset the tail
            if (tail == data.Length) tail = 0;

            size++;
        }

        public T Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue is empty");

            var firstItem = data[head];
            data[head] = default;

            size--;

            head++;

            // Reset the head
            if (head == data.Length) head = 0;

            return firstItem;
        }

        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue is empty");
            return data[head];
        }

        // Increase the capacity to store more elements.
        private void IncreaseCapacity()
        {
            capacity *= 2;

            var newData = new T[capacity];

            if (head < tail)
            {
                Array.Copy(data, head, newData, 0, size);
            }
            else
            {
                //Copy from head of the queue to end of the data array
                Array.Copy(data, head, newData, 0, data.Length - head);
               
                //Copy from start of the data array to the tail of the queue
                Array.Copy(data, 0, newData, data.Length - head, tail);
            }
            data = newData;
            head = 0;
            tail = size == capacity ? 0 : size;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var elem in data)
                yield return elem;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}