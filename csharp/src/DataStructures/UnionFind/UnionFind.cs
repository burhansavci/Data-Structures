using System;
using System.Collections.Generic;

namespace DataStructures.UnionFind
{
    public class UnionFind<T> : IUnionFind<T>
    {
        // Used to track the size of each of the component
        private readonly Dictionary<T, int> componentSizes = new();

        // components[p] points to the parent of p, if components[p] = p then p is a root node
        private readonly Dictionary<T, T> components = new();

        public UnionFind(IReadOnlyCollection<T> components)
        {
            if (components == null) throw new ArgumentNullException(nameof(components));

            Size = NumComponents = components.Count;

            foreach (var component in components)
            {
                this.components.Add(component, component); // Link to itself (self root)
                componentSizes.Add(component, 1); // Each component is originally of size one
            }
        }
        
        public T Find(T p)
        {
            // Find the root of the component/set
            var root = p;
            while (!Equals(root, components[root])) root = components[root];

            // Compress the path leading back to the root.
            // Doing this operation is called "path compression"
            // and is what gives us amortized time complexity.
            while (!Equals(p, root))
            {
                var next = components[p];
                components[p] = root;
                p = next;
            }

            return root;
        }
        
        public bool Connected(T p, T q) => Equals(Find(p), Find(q));
        
        public int ComponentSize(T p) => componentSizes[Find(p)];
        
        public int Size { get; }
        
        public int NumComponents { get; private set; }
        
        public void Unify(T p, T q)
        {
            // These elements are already in the same group!
            if (Connected(p, q)) return;

            var root1 = Find(p);
            var root2 = Find(q);

            // Merge smaller component/set into the larger one.
            if (componentSizes[root1] < componentSizes[root2])
            {
                componentSizes[root2] += componentSizes[root1];
                components[root1] = root2;
            }
            else
            {
                componentSizes[root1] += componentSizes[root2];
                components[root2] = root1;
            }

            // Since the roots found are different we know that the
            // number of components/sets has decreased by one
            NumComponents--;
        }
    }
}