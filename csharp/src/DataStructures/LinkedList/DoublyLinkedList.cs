using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LinkedList
{
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private int size;
        private Node<T> head;
        private Node<T> tail;

        // Internal node class to represent data
        private class Node<TY>
        {
            public TY Data;
            public Node<TY> Prev;
            public Node<TY> Next;

            public Node(TY data, Node<TY> prev, Node<TY> next)
            {
                Data = data;
                Prev = prev;
                Next = next;
            }
        }

        // Empty this linked list, O(n)
        public void Clear()
        {
            var currentNode = head;
            while (currentNode != null)
            {
                var next = currentNode.Next;
                currentNode.Prev = currentNode.Next = null;
                currentNode.Data = default;
                currentNode = next;
            }
            head = tail = null;
            size = 0;
        }

        // Return the size of this linked list
        public int Count => size;

        // Is this linked list empty?
        public bool IsEmpty() => Count == 0;

        // Add an element to the tail of the linked list, O(1)
        public void Add(T data)
        {
            AddLast(data);
        }

        // Add a node to the tail of the linked list, O(1)
        public void AddLast(T data)
        {
            if (IsEmpty())
                head = tail = new Node<T>(data, null, null);
            else
            {
                tail.Next = new Node<T>(data, tail, null);
                tail = tail.Next;
            }
            size++;
        }

        // Add an element to the beginning of this linked list, O(1)
        public void AddFirst(T data)
        {
            if (IsEmpty())
                head = tail = new Node<T>(data, null, null);
            else
            {
                head.Prev = new Node<T>(data, null, head);
                head = head.Prev;
            }
            size++;
        }

        // Add an element at a specified index
        public void AddAt(int index, T data)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException();

            if (index == 0)
                AddFirst(data);
            else if (index == Count)
                AddLast(data);
            else
            {
                var currentNode = head;
                for (var i = 0; i < index - 1; i++)
                    currentNode = currentNode.Next;

                var newNode = new Node<T>(data, currentNode, currentNode.Next);
                currentNode.Next.Prev = newNode;
                currentNode.Next = newNode;
                size++;
            }
        }

        // Check the value of the first node if it exists, O(1)
        public T PeekFirst()
        {
            if (IsEmpty()) throw new IndexOutOfRangeException("List is empty.");
            return head.Data;
        }

        // Check the value of the last node if it exists, O(1)
        public T PeekLast()
        {
            if (IsEmpty()) throw new IndexOutOfRangeException("List is empty.");
            return tail.Data;
        }

        // Remove the first value at the head of the linked list, O(1)
        public T RemoveFirst()
        {
            if (IsEmpty()) throw new IndexOutOfRangeException("List is empty.");

            // Extract the data at the head and move
            // the head pointer forwards one node
            var data = head.Data;
            head = head.Next;
            size--;

            if (IsEmpty())
                tail = null; // If the list is empty set the tail to null
            else
                head.Prev = null; // Do a memory cleanup of the previous node

            // Return the data that was at the first node we just removed
            return data;
        }

        // Remove the last value at the tail of the linked list, O(1)
        public T RemoveLast()
        {
            if (IsEmpty()) throw new IndexOutOfRangeException("List is empty.");

            // Extract the data at the tail and move
            // the tail pointer backwards one node
            var data = tail.Data;
            tail = tail.Prev;
            size--;

            if (IsEmpty())
                head = null; // If the list is empty set the head to null
            else
                tail.Next = null; // Do a memory clean of the node that was just removed

            // Return the data that was at the last node we just removed
            return data;
        }

        // Remove an arbitrary node from the linked list, O(1)
        private T Remove(Node<T> node)
        {
            // If the node to remove is somewhere either at the
            // head or the tail handle those independently
            if (node.Prev == null) return RemoveFirst();
            if (node.Next == null) return RemoveLast();

            // Make the pointers of adjacent nodes skip over 'node'
            node.Next.Prev = node.Prev;
            node.Prev.Next = node.Next;

            // Temporarily store the data we want to return
            var data = node.Data;

            // Memory cleanup
            node.Data = default;
            node = node.Prev = node.Next = null;

            size--;

            // Return the data in the node we just removed
            return data;
        }

        // Remove a node at a particular index, O(n)
        public T RemoveAt(int index)
        {
            // Handle index out of bound errors
            if (IsEmpty() || index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            int i;
            Node<T> currentNode;

            // Search from the front of the list
            if (index < size / 2)
                for (i = 0, currentNode = head; i != index; i++)
                    currentNode = currentNode.Next;
            // Search from the back of the list
            else
                for (i = size - 1, currentNode = tail; i != index; i--)
                    currentNode = currentNode.Prev;

            return Remove(currentNode);
        }

        // Remove a particular value in the linked list, O(n)
        public bool Remove(T data)
        {
            for (var currentNode = head; currentNode != null; currentNode = currentNode.Next)
                if (Equals(currentNode.Data, data))
                {
                    Remove(currentNode);
                    return true;
                }

            return false;
        }

        // Find the index of a particular value in the linked list, O(n)
        public int IndexOf(T data)
        {
            var index = 0;
            for (var currentNode = head; currentNode != null; currentNode = currentNode.Next, index++)
                if (Equals(currentNode.Data, data))
                    return index;

            return -1;
        }

        // Check is a value is contained within the linked list
        public bool Contains(T data) => IndexOf(data) != -1;

        public IEnumerator<T> GetEnumerator()
        {
            var node = head;
            while (node != null)
            {
                yield return node.Data;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}