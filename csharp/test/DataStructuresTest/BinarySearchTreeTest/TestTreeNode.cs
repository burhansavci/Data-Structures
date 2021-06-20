using System.Collections.Generic;

namespace DataStructuresTest.BinarySearchTreeTest
{
    internal class TestTreeNode
    {
        private readonly int data;
        private TestTreeNode left, right;

        private TestTreeNode(int data, TestTreeNode l, TestTreeNode r)
        {
            this.data = data;
            right = r;
            left = l;
        }

        public static TestTreeNode Add(TestTreeNode node, int data)
        {
            if (node == null)
            {
                node = new TestTreeNode(data, null, null);
            }
            else
            {
                // Place lower elem values on left
                if (data < node.data)
                {
                    node.left = Add(node.left, data);
                }
                else
                {
                    node.right = Add(node.right, data);
                }
            }
            return node;
        }

        public static void PreOrder(List<int> lst, TestTreeNode node)
        {
            if (node == null) return;

            lst.Add(node.data);
            if (node.left != null) PreOrder(lst, node.left);
            if (node.right != null) PreOrder(lst, node.right);
        }

        public static void InOrder(List<int> lst, TestTreeNode node)
        {
            if (node == null) return;

            if (node.left != null) InOrder(lst, node.left);
            lst.Add(node.data);
            if (node.right != null) InOrder(lst, node.right);
        }

        public static void PostOrder(List<int> lst, TestTreeNode node)
        {
            if (node == null) return;

            if (node.left != null) PostOrder(lst, node.left);
            if (node.right != null) PostOrder(lst, node.right);
            lst.Add(node.data);
        }

        public static void LevelOrder(List<int> lst, TestTreeNode node)
        {
            var q = new Queue<TestTreeNode>();
            if (node != null) q.Enqueue(node);

            while (q.Count != 0)
            {
                node = q.Dequeue();
                lst.Add(node.data);
                if (node.left != null) q.Enqueue(node.left);
                if (node.right != null) q.Enqueue(node.right);
            }
        }
    }
}