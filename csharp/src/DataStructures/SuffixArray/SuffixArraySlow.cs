using System;

namespace DataStructures.SuffixArray
{
    public class SuffixArraySlow : SuffixArray
    {
        private class Suffix : IComparable<Suffix>
        {
            // Starting position of suffix in text
            public readonly int Index;
            private readonly int len;
            private readonly int[] text;

            public Suffix(int[] text, int index)
            {
                len = text.Length - index;
                Index = index;
                this.text = text;
            }

            // Compare the two suffixes inspired by Robert Sedgewick and Kevin Wayne
            public int CompareTo(Suffix other)
            {
                if (this == other) return 0;
                var minLen = Math.Min(len, other.len);
                for (var i = 0; i < minLen; i++)
                {
                    if (text[Index + i] < other.text[other.Index + i]) return -1;
                    if (text[Index + i] > other.text[other.Index + i]) return +1;
                }
                return len - other.len;
            }
        }

        // Contains all the suffixes of the SuffixArray
        private Suffix[] suffixes;

        public SuffixArraySlow(string text) : base(ToIntArray(text)) { }

        public SuffixArraySlow(int[] text) : base(text) { }

        // Suffix array construction. This actually takes O(n^2log(n)) time since sorting takes on
        // average O(nlog(n)) and each String comparison takes O(n).
        protected override void Construct()
        {
            SortedSuffixArray = new int[N];
            suffixes = new Suffix[N];

            for (var i = 0; i < N; i++)
                suffixes[i] = new Suffix(Text, i);

            Array.Sort(suffixes);

            for (var i = 0; i < N; i++)
            {
                var suffix = suffixes[i];
                SortedSuffixArray[i] = suffix.Index;
                suffixes[i] = null;
            }

            suffixes = null;
        }
    }
}