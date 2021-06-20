using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.BinarySearchTree;
using Xunit;

namespace DataStructuresTest.BinarySearchTreeTest
{
    public class BinarySearchTreeTest
    {
        private const int Loops = 100;

        [Fact]
        public void TestIsEmpty()
        {
            var tree = new BinarySearchTree<string>();
            Assert.True(tree.IsEmpty);

            tree.Add("Hello World!");
            Assert.False(tree.IsEmpty);
        }

        [Fact]
        public void TestSize()
        {
            var tree = new BinarySearchTree<string>();
            Assert.Equal(0, tree.Count);

            tree.Add("Hello World!");
            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void TestHeight()
        {
            var tree = new BinarySearchTree<string>();

            // Tree should look like:
            //        M
            //      J  S
            //    B   N Z
            //  A

            // No tree
            Assert.Equal(0, tree.Height());

            // Layer One
            tree.Add("M");
            Assert.Equal(1, tree.Height());

            // Layer Two
            tree.Add("J");
            Assert.Equal(2, tree.Height());
            tree.Add("S");
            Assert.Equal(2, tree.Height());

            // Layer Three
            tree.Add("B");
            Assert.Equal(3, tree.Height());
            tree.Add("N");
            Assert.Equal(3, tree.Height());
            tree.Add("Z");
            Assert.Equal(3, tree.Height());

            // Layer 4
            tree.Add("A");
            Assert.Equal(4, tree.Height());
        }

        [Fact]
        public void TestAdd()
        {
            // Add element which does not yet exist
            var tree = new BinarySearchTree<char>();
            Assert.True(tree.Add('A'));

            // Add duplicate element
            Assert.Throws<InvalidOperationException>(() => tree.Add('A'));

            // Add a second element which is not a duplicate
            Assert.True(tree.Add('B'));
        }

        [Fact]
        public void TestRemove()
        {
            // Try removing an element which doesn't exist
            var tree = new BinarySearchTree<char>();
            tree.Add('A');
            Assert.Equal(1, tree.Count);
            Assert.False(tree.Remove('B'));
            Assert.Equal(1, tree.Count);

            // Try removing an element which does exist
            tree.Add('B');
            Assert.Equal(2, tree.Count);
            Assert.True(tree.Remove('B'));
            Assert.Equal(1, tree.Count);
            Assert.Equal(1, tree.Height());

            // Try removing the root
            Assert.True(tree.Remove('A'));
            Assert.Equal(0, tree.Count);
            Assert.Equal(0, tree.Height());
        }

        [Fact]
        public void TestContains()
        {
            // Setup tree
            var tree = new BinarySearchTree<char>();

            tree.Add('B');
            tree.Add('A');
            tree.Add('C');

            // Try looking for an element which doesn't exist
            Assert.False(tree.Contains('D'));

            // Try looking for an element which exists in the root
            Assert.True(tree.Contains('B'));

            // Try looking for an element which exists as the left child of the root
            Assert.True(tree.Contains('A'));

            // Try looking for an element which exists as the right child of the root
            Assert.True(tree.Contains('C'));
        }

        [Fact]
        public void RandomRemoveTests()
        {
            for (var i = 0; i < Loops; i++)
            {
                var tree = new BinarySearchTree<int>();
                var lst = TestUtil.GenRandList(i);
                foreach (var value in lst) tree.Add(value);
                TestUtil.Shuffle(lst);

                // Remove all the elements we just placed in the tree
                for (var j = 0; j < i; j++)
                {
                    var value = lst[j];

                    Assert.True(tree.Remove(value));
                    Assert.False(tree.Contains(value));
                    Assert.Equal(i - j - 1, tree.Count);
                }

                Assert.True(tree.IsEmpty);
            }
        }

        [Fact]
        public void TestPreOrderTraversal()
        {
            for (var i = 0; i < Loops; i++)
            {
                var input = TestUtil.GenRandList(i);
                Assert.True(ValidateTreeTraversal(TraversalOrder.PreOrder, input));
            }
        }

        [Fact]
        public void TestInOrderTraversal()
        {
            for (var i = 0; i < Loops; i++)
            {
                var input = TestUtil.GenRandList(i);
                Assert.True(ValidateTreeTraversal(TraversalOrder.InOrder, input));
            }
        }

        [Fact]
        public void TestPostOrderTraversal()
        {
            for (var i = 3; i < Loops; i++)
            {
                var input = TestUtil.GenRandList(i);
                Assert.True(ValidateTreeTraversal(TraversalOrder.PostOrder, input));
            }
        }

        [Fact]
        public void TestLevelOrderTraversal()
        {
            for (var i = 0; i < Loops; i++)
            {
                var input = TestUtil.GenRandList(i);
                Assert.True(ValidateTreeTraversal(TraversalOrder.LevelOrder, input));
            }
        }

        [Fact]
        public static void TestTreeWithDuplicatesElements()
        {
            // New tree which doesn't allow duplicates
            var binarySearchTree = new BinarySearchTree<int>(true);

            var values = new[] {15, 25, 5, 12, 1, 16, 20, 9, 9, 7, 7, 7, -1, 11, 19, 30, 8, 10, 13, 28, 39};

            // Insert values with duplicates
            foreach (var value in values)
                binarySearchTree.Add(value);

            // ASSERT COUNT = 21 (allows duplicates)
            Assert.Equal(21, binarySearchTree.Count);

            // Test contains/find
            Assert.True(binarySearchTree.Contains(10));

            // ASSERT MIN ITEM
            Assert.True(binarySearchTree.FindMin() == -1);

            // ASSERT MAX ITEM
            Assert.True(binarySearchTree.FindMax() == 39);

            // Remove min & max
            binarySearchTree.RemoveMin();
            binarySearchTree.RemoveMax();

            // ASSERT MIN AFTER REMOVE-MIN
            Assert.True(binarySearchTree.FindMin() == 1);

            // ASSERT MAX AFTER REMOVE MAX
            Assert.True(binarySearchTree.FindMax() == 30);

            // Remove min twice
            binarySearchTree.RemoveMin();
            binarySearchTree.RemoveMin();

            // ASSERT MIN
            Assert.True(binarySearchTree.FindMin() == 7);

            // 7 STILL EXISTS BECAUSE IT WAS DUPLICATED
            binarySearchTree.RemoveMin();
            Assert.True(binarySearchTree.FindMin() == 7);

            // Remove max thrice
            binarySearchTree.RemoveMax();
            binarySearchTree.RemoveMax();
            binarySearchTree.RemoveMax();

            // ASSERT MAX AFTER REMOVE-MAX 3 TIMES
            Assert.True(binarySearchTree.FindMax() == 20);

            // Test removing an element with subtrees
            // doesn't exist!
            Assert.False(binarySearchTree.Remove(1000));
            // does exist!
            Assert.True(binarySearchTree.Remove(16));

            var enumerator = binarySearchTree.Traverse(TraversalOrder.InOrder);
            enumerator.MoveNext();
            Assert.Equal(7, enumerator.Current);

            enumerator.MoveNext();
            enumerator.MoveNext();
            Assert.True(enumerator.Current == 8);
        }

        [Fact]
        public static void TestTreeWithUniqueElements()
        {
            // New tree which doesn't allow duplicates
            var binarySearchTree = new BinarySearchTree<int>();

            var values = new[] {14, 15, 25, 5, 12, 1, 16, 20, 9, 9, 9, 7, 7, 7, -1, 11, 19, 30, 8, 10, 13, 28, 39, 39};

            Assert.Throws<InvalidOperationException>(() =>
            {
                foreach (var value in values)
                {
                    binarySearchTree.Add(value);
                }
            });

            // Reduce values array to an array of distinct values
            binarySearchTree.Clear();
            values = values.Distinct().ToArray();

            foreach (var value in values)
                Assert.True(binarySearchTree.Add(value));

            // ASSERT COUNT
            Assert.Equal(binarySearchTree.Count, values.Length);
        }

        private bool ValidateTreeTraversal(TraversalOrder traversalOrder, IEnumerable<int> input)
        {
            var actual = new List<int>();
            var expected = new List<int>();

            TestTreeNode testTree = null;
            var tree = new BinarySearchTree<int>();

            // Construct Binary Tree and test tree
            foreach (var value in input)
            {
                testTree = TestTreeNode.Add(testTree, value);
                tree.Add(value);
            }

            switch (traversalOrder)
            {
                case TraversalOrder.PreOrder:
                    TestTreeNode.PreOrder(expected, testTree);
                    break;
                case TraversalOrder.InOrder:
                    TestTreeNode.InOrder(expected, testTree);
                    break;
                case TraversalOrder.PostOrder:
                    TestTreeNode.PostOrder(expected, testTree);
                    break;
                case TraversalOrder.LevelOrder:
                    TestTreeNode.LevelOrder(expected, testTree);
                    break;
            }

            // Get traversal output
            var iter = tree.Traverse(traversalOrder);
            while (iter.MoveNext()) actual.Add(iter.Current);

            // The actual and the expected size better be the same size
            if (actual.Count != expected.Count) return false;

            // Compare actual to expected
            return !actual.Where((t, i) => expected[i] != t).Any();
        }
    }
}