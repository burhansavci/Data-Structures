using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.BinarySearchTree
{
    public enum TraversalOrder
    {
        PreOrder = 0,
        InOrder = 1,
        PostOrder = 2,
        LevelOrder = 3
    }
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        // This BST is a rooted tree so we maintain a handle on the root node
        private Node root;

        // Flag for whether the tree can keep duplicate nodes
        private readonly bool allowDuplicates;

        public BinarySearchTree() => allowDuplicates = false;

        public BinarySearchTree(bool allowDuplicates) => this.allowDuplicates = allowDuplicates;

        // Internal node containing node references
        // and the actual node data
        private class Node
        {
            public T Data;
            public Node Left;
            public Node Right;

            public Node(Node left, Node right, T elem)
            {
                Data = elem;
                Left = left;
                Right = right;
            }
        }

        // Check if this binary tree is empty
        public bool IsEmpty => Count == 0;

        // Tracks the number of nodes in this BST
        // Get the number of nodes in this binary tree
        public int Count { get; private set; }

        // Add an element to this binary tree. Returns true
        // if we successfully perform an insertion
        public bool Add(T elem)
        {
            // Check if the value already exists in this
            // binary tree, if it throws exception
            if (!allowDuplicates && Contains(elem)) throw new InvalidOperationException("BST does not allow adding duplicate values.");
            // Otherwise add this element to the binary tree
            root = Add(root, elem);
            Count++;
            return true;
        }

        // Private method to recursively add a value in the binary tree
        private Node Add(Node node, T elem)
        {
            // Base case: found a leaf node
            if (node == null)
                node = new Node(null, null, elem);
            else // Pick a subtree to insert element
            if (elem.CompareTo(node.Data) < 0)
                node.Left = Add(node.Left, elem);
            else
                node.Right = Add(node.Right, elem);

            return node;
        }

        // Remove a value from this binary tree if it exists, O(n)
        public bool Remove(T elem)
        {
            // Make sure the node we want to remove
            // actually exists before we remove it
            if (Contains(elem))
            {
                root = Remove(root, elem);
                Count--;
                return true;
            }
            return false;
        }

        public void RemoveMin() => Remove(root, FindMin(root).Data);

        public void RemoveMax() => Remove(root, FindMax(root).Data);

        private Node Remove(Node node, T elem)
        {
            if (node == null) return null;

            var cmp = elem.CompareTo(node.Data);

            switch (cmp)
            {
                // Dig into left subtree, the value we're looking
                // for is smaller than the current value
                case < 0:
                    node.Left = Remove(node.Left, elem);
                    break;
                // Dig into right subtree, the value we're looking
                // for is greater than the current value
                case > 0:
                    node.Right = Remove(node.Right, elem);
                    break;
                // Found the node we wish to remove
                default:
                    {
                        // This is the case with only a right subtree or
                        // no subtree at all. In this situation just
                        // swap the node we wish to remove with its right child.
                        if (node.Left == null)
                            return node.Right;

                        // This is the case with only a left subtree or
                        // no subtree at all. In this situation just
                        // swap the node we wish to remove with its left child.
                        if (node.Right == null)
                            return node.Left;

                        // When removing a node from a binary tree with two links the
                        // successor of the node being removed can either be the largest
                        // value in the left subtree or the smallest value in the right
                        // subtree. In this implementation I have decided to find the
                        // smallest value in the right subtree which can be found by
                        // traversing as far left as possible in the right subtree.

                        // Find the leftmost node in the right subtree
                        var tmp = FindMin(node.Right);

                        // Swap the data
                        node.Data = tmp.Data;

                        // Go into the right subtree and remove the leftmost node we
                        // found and swapped data with. This prevents us from having
                        // two nodes in our tree with the same value.
                        node.Right = Remove(node.Right, tmp.Data);

                        // If instead we wanted to find the largest node in the left
                        // subtree as opposed to smallest node in the right subtree
                        // here is what we would do:
                        // Node tmp = findMax(node.left);
                        // node.data = tmp.data;
                        // node.left = Remove(node.left, tmp.data);
                        break;
                    }
            }
            return node;
        }

        // Find the leftmost node (which has the smallest value)
        public T FindMin() => FindMin(root).Data;
        
        private Node FindMin(Node node)
        {
            while (node.Left != null) node = node.Left;
            return node;
        }

        // Find the rightmost node (which has the largest value)
        public T FindMax() => FindMax(root).Data;
        
        private Node FindMax(Node node)
        {
            while (node.Right != null) node = node.Right;
            return node;
        }

        // returns true is the element exists in the tree
        public bool Contains(T elem) => Contains(root, elem);

        // private recursive method to find an element in the tree
        private bool Contains(Node node, T elem)
        {
            // Base case: reached bottom, value not found
            if (node == null) return false;

            var cmp = elem.CompareTo(node.Data);

            return cmp switch
                   {
                           // Dig into the left subtree because the value we're
                           // looking for is smaller than the current value
                           < 0 => Contains(node.Left, elem),
                           // Dig into the right subtree because the value we're
                           // looking for is greater than the current value
                           > 0 => Contains(node.Right, elem),
                           // We found the value we were looking for
                           _ => true
                   };
        }

        // Computes the height of the tree, O(n)
        public int Height() => Height(root);

        // Recursive helper method to compute the height of the tree
        private int Height(Node node)
        {
            if (node == null) return 0;
            return Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        // Clears all elements from tree.
        public void Clear()
        {
            root = null;
            Count = 0;
        }

        // This method returns an iterator for a given TreeTraversalOrder.
        // The ways in which you can traverse the tree are in four different ways:
        // preorder, inorder, postorder and levelorder.
        public IEnumerator<T> Traverse(TraversalOrder order)
        {
            return order switch
                   {
                           TraversalOrder.PreOrder => new PreOrderEnumerator(this),
                           TraversalOrder.InOrder => new InOrderEnumerator(this),
                           TraversalOrder.PostOrder => new PostOrderEnumerator(this),
                           TraversalOrder.LevelOrder => new LevelOrderEnumerator(this),
                           _ => null
                   };
        }

        // Returns as iterator to traverse the tree in pre order
        private class PreOrderEnumerator : IEnumerator<T>
        {
            private Node current;
            private BinarySearchTree<T> tree;
            private readonly Stack<Node> traverseStack;

            public PreOrderEnumerator(BinarySearchTree<T> tree)
            {
                this.tree = tree;

                //Build stack
                traverseStack = new Stack<Node>();
                traverseStack.Push(this.tree.root);
            }

            public T Current => current.Data;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                current = null;
                tree = null;
            }

            public void Reset() => current = null;

            public bool MoveNext()
            {
                current = traverseStack.Count > 0 ? traverseStack.Pop() : null;

                if (current?.Right != null) traverseStack.Push(current.Right);
                if (current?.Left != null) traverseStack.Push(current.Left);

                return current != null;
            }
        }

        // Returns as iterator to traverse the tree in order
        private class InOrderEnumerator : IEnumerator<T>
        {
            private Node current;
            private Node traverser;
            private BinarySearchTree<T> tree;
            private readonly Stack<Node> traverseStack;

            public InOrderEnumerator(BinarySearchTree<T> tree)
            {
                this.tree = tree;

                //Build queue
                traverseStack = new Stack<Node>();
                traverser = this.tree.root;
                traverseStack.Push(this.tree.root);
            }

            public T Current => current.Data;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                current = null;
                tree = null;
            }

            public void Reset() => current = null;

            public bool MoveNext()
            {
                // Dig left
                while (traverser != null && traverser.Left != null)
                {
                    traverseStack.Push(traverser.Left);
                    traverser = traverser.Left;
                }

                current = traverseStack.Count > 0 ? traverseStack.Pop() : null;

                // Try moving down right once
                if (current?.Right != null)
                {
                    traverseStack.Push(current.Right);
                    traverser = current.Right;
                }

                return current != null;
            }
        }

        // Returns as iterator to traverse the tree in post order
        private class PostOrderEnumerator : IEnumerator<T>
        {
            private Node current;
            private BinarySearchTree<T> tree;
            private readonly Stack<Node> postStack;

            public PostOrderEnumerator(BinarySearchTree<T> tree)
            {
                this.tree = tree;

                //Build stack
                var traverseStack = new Stack<Node>();
                postStack = new Stack<Node>();

                traverseStack.Push(this.tree.root);
                while (traverseStack.Count != 0)
                {
                    var node = traverseStack.Pop();
                    if (node != null)
                    {
                        postStack.Push(node);
                        if (node.Left != null) traverseStack.Push(node.Left);
                        if (node.Right != null) traverseStack.Push(node.Right);
                    }
                }
            }

            public T Current => current.Data;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                current = null;
                tree = null;
            }

            public void Reset() => current = null;

            public bool MoveNext()
            {
                current = postStack.Count > 0 ? postStack.Pop() : null;

                return current != null;
            }
        }

        // Returns as iterator to traverse the tree in level order
        private class LevelOrderEnumerator : IEnumerator<T>
        {
            private Node current;
            private BinarySearchTree<T> tree;
            private readonly Queue<Node> traverseQueue;

            public LevelOrderEnumerator(BinarySearchTree<T> tree)
            {
                this.tree = tree;

                //Build queue
                traverseQueue = new Queue<Node>();
                traverseQueue.Enqueue(this.tree.root);
            }

            public T Current => current.Data;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                current = null;
                tree = null;
            }

            public void Reset() => current = null;

            public bool MoveNext()
            {
                current = traverseQueue.Count > 0 ? traverseQueue.Dequeue() : null;

                if (current?.Left != null) traverseQueue.Enqueue(current?.Left);
                if (current?.Right != null) traverseQueue.Enqueue(current?.Right);

                return current != null;
            }
        }
    }
}