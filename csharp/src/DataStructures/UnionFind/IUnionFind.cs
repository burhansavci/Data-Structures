namespace DataStructures.UnionFind
{
    public interface IUnionFind<T>
    {
        // Find which component/set 'p' belongs to, takes amortized constant time.
        T Find(T p);

        // Return whether or not the elements 'p' and
        // 'q' are in the same components/set.
        bool Connected(T p, T q);

        // Return the size of the components/set 'p' belongs to
        int ComponentSize(T p);

        // Unify the components/sets containing elements 'p' and 'q'
        void Unify(T p, T q);

        // The number of elements in this union find
        // Return the number of elements in this IntUnionFind/Disjoint set
        int Size { get; }
        
        // Tracks the number of components in the union find
        // Returns the number of remaining components/sets
        int NumComponents { get; }
    }
}