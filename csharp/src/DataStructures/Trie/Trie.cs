using System;
using System.Collections.Generic;

namespace DataStructures.Trie
{
    public class Trie
    {
        // The root character is an arbitrarily picked
        // character chosen for the root node.
        private const char RootCharacter = '\0';
        private Node root = new(RootCharacter);

        private class Node
        {
            char ch;
            public int Count;
            public bool IsWordEnding;
            public Dictionary<char, Node> Children = new();

            public Node(char ch) => this.ch = ch;

            public void AddChild(Node node, char c) => Children.Add(c, node);
        }

        // Returns true if the string being inserted
        // contains a prefix already in the trie
        public bool Insert(string key, int numInserts = 1)
        {
            if (key == null) throw new ArgumentException("Null not permitted in trie");
            if (numInserts <= 0)
                throw new ArgumentException("numInserts has to be greater than zero");

            var node = root;
            var createdNewNode = false;
            var isPrefix = false;

            // Process each character one at a time
            foreach (var ch in key)
            {
                if (!node.Children.TryGetValue(ch, out var nextNode))
                {
                    // The next character in this string does not yet exist in trie
                    nextNode = new Node(ch);
                    node.AddChild(nextNode, ch);
                    createdNewNode = true;
                }
                else
                {
                    // Next character exists in trie.
                    if (nextNode.IsWordEnding) isPrefix = true;
                }

                node = nextNode;
                node.Count += numInserts;
            }

            // The root itself is not a word ending. It is simply a placeholder.
            if (node != root) node.IsWordEnding = true;

            return isPrefix || !createdNewNode;
        }

        // This delete function allows you to delete keys from the trie
        // (even those which were not previously inserted into the trie).
        // This means that it may be the case that you delete a prefix which
        // cuts off the access to numerous other strings starting with
        // that prefix.
        public bool Delete(string key, int numDeletions = 1)
        {
            // We cannot delete something that doesn't exist
            if (!Contains(key)) return false;

            if (numDeletions <= 0) throw new ArgumentException("numDeletions has to be positive");

            var node = root;
            foreach (var ch in key)
            {
                var curNode = node.Children[ch];
                curNode.Count -= numDeletions;

                // Cut this edge if the current node has a count <= 0
                // This means that all the prefixes below this point are inaccessible
                if (curNode.Count <= 0)
                {
                    node.Children.Remove(ch);
                    curNode.Children = null;
                    curNode = null;
                    return true;
                }

                node = curNode;
            }
            return true;
        }

        // Returns true if this string is contained inside the trie
        public bool Contains(string key) => Count(key) != 0;

        // Returns the count of a particular prefix
        public int Count(string key)
        {
            if (key == null) throw new ArgumentException("Null not permitted");

            var node = root;

            // Dig down into trie until we reach the bottom or stop
            // early because the string we're looking for doesn't exist
            foreach (var ch in key)
            {
                if (node == null) return 0;
                node = node.Children.GetValueOrDefault(ch);
            }

            return node?.Count ?? 0;
        }

        // Recursively clear the trie freeing memory to help GC
        private void Clear(Node node)
        {
            if (node == null) return;

            foreach (var ch in node.Children.Keys)
            {
                var nextNode = node.Children[ch];
                Clear(nextNode);
                nextNode = null;
            }
            
            node.Children.Clear();
            node.Children = null;
        }

        // Clear the trie
        public void Clear()
        {
            root.Children = null;
            root = new Node(RootCharacter);
        }
    }
}