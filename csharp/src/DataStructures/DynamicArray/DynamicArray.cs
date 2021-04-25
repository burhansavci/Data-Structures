using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.DynamicArray
{
    public class DynamicArray<T> : IEnumerable<T>
    {
        private T[] arr = new T[0];
        private int len;
        private int capacity;

        public DynamicArray() { }

        public DynamicArray(int capacity)
        {
            if (capacity < 0) throw new ArgumentException($"Illegal Capacity {capacity}");
            this.capacity = capacity;
            arr = new T[this.capacity];
        }

        public int Size => len;
        public bool IsEmpty => Size == 0;

        public T Get(int index)
        {
            if (index >= len || index < 0) throw new IndexOutOfRangeException();
            return arr[index];
        }

        public void Set(int index, T obj)
        {
            if (index >= len || index < 0) throw new IndexOutOfRangeException();
            arr[index] = obj;
        }

        public void Clear()
        {
            for (var i = 0; i < capacity; i++)
                arr[i] = default;
            len = 0;
        }

        public int IndexOf(T obj)
        {
            for (var i = 0; i < len; i++)
                if (Equals(arr[i], obj))
                    return i;
            return -1;
        }

        public bool Contains(T obj) => IndexOf(obj) != -1;

        public void Add(T obj)
        {
            if (len + 1 >= capacity)
            {
                capacity = capacity == 0 ? 1 : capacity * 2;
                var newArr = new T[capacity];
                for (var i = 0; i < len; i++)
                    newArr[i] = arr[i];
                arr = newArr;
            }
            arr[len++] = obj;
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= len) throw new IndexOutOfRangeException();

            var data = arr[index];
            var newArr = new T[len - 1];

            for (int i = 0, j = 0; i < len; i++, j++)
                if (i == index)
                    j--;
                else
                    newArr[j] = arr[i];

            arr = newArr;
            capacity = --len;
            return data;
        }

        public bool Remove(T obj)
        {
            var index = IndexOf(obj);
            if (index == -1) return false;
            RemoveAt(index);
            return true;
        }


        // Normal implementation for IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var elem in arr)
                yield return elem;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}