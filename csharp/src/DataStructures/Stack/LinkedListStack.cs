using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Stack
{
    public class LinkedListStack<T> : IEnumerable<T>, IStack<T>
    {
        private readonly LinkedList<T> linkedList = new();

        // Create an empty stack
        public LinkedListStack() { }

        // Create a Stack with an initial element
        public LinkedListStack(T firstElem) => Push(firstElem);

        public int Size() => linkedList.Count;

        public bool IsEmpty() => Size() == 0;

        public void Push(T elem) => linkedList.AddLast(elem);

        public T Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack is empty");
            var lastNode = linkedList.Last;
            linkedList.RemoveLast();
            return lastNode!.Value;
        }

        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack is empty");
            return linkedList.Last!.Value;
        }

        public IEnumerator<T> GetEnumerator() => linkedList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}