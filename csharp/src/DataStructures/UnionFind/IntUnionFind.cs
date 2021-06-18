using System;

namespace DataStructures.UnionFind
{
    public class IntUnionFind : IUnionFind<int>
    {
        // Used to track the size of each of the component
        private readonly int[] sz;

        // id[i] points to the parent of i, if id[i] = i then i is a root node
        private readonly int[] id;

        public IntUnionFind(int size)
        {
            if (size <= 0) throw new ArgumentException("Size <= 0 is not allowed");

            Size = NumComponents = size;
            sz = new int[size];
            id = new int[size];

            for (var i = 0; i < size; i++)
            {
                id[i] = i;
                sz[i] = 1;
            }
        }
        
        public int Find(int p)
        {
            var root = p;
            while (root != id[root]) root = id[root];
            
            while (p != root)
            {
                var next = id[p];
                id[p] = root;
                p = next;
            }

            return root;
        }
        
        public bool Connected(int p, int q) => Find(p) == Find(q);
        
        public int ComponentSize(int p) => sz[Find(p)];
        
        public int Size { get; }
        
        public int NumComponents { get; private set; }
        
        public void Unify(int p, int q)
        {
            if (Connected(p, q)) return;

            var root1 = Find(p);
            var root2 = Find(q);
            
            if (sz[root1] < sz[root2])
            {
                sz[root2] += sz[root1];
                id[root1] = root2;
            }
            else
            {
                sz[root1] += sz[root2];
                id[root2] = root1;
            }
            
            NumComponents--;
        }
    }
}