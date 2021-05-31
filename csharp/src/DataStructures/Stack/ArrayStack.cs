using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Stack
{
    public class ArrayStack<T> : IEnumerable<T>, IStack<T>
    {
        private int size;
        private int capacity;
        private T[] data;

        public ArrayStack()
        {
            capacity = 16;
            data = new T[capacity];
        }

        public int Size() => size;

        public bool IsEmpty() => size == 0;

        public void Push(T elem)
        {
            if (size == capacity)
                IncreaseCapacity();

            data[size++] = elem;
        }

        public T Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack is empty");
            var lastItem = data[--size];
            data[size] = default;
            return lastItem;
        }

        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack is empty");
            return data[size - 1];
        }

        // Increase the capacity to store more elements.
        private void IncreaseCapacity()
        {
            capacity *= 2;
            var newData = new T[capacity];
            Array.Copy(data, newData, size);
            data = newData;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var elem in data)
                yield return elem;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}