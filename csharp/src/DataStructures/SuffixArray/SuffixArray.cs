using System;

namespace DataStructures.SuffixArray
{
    public abstract class SuffixArray
    {
        // Length of the suffix array
        protected readonly int N;

        // T is the text
        protected readonly int[] Text;

        // The sorted suffix array values.
        protected int[] SortedSuffixArray;

        // Longest Common Prefix array
        protected int[] Lcp;

        private bool constructedSa = false;
        private bool constructedLcpArray = false;

        public SuffixArray(int[] text)
        {
            Text = text ?? throw new ArgumentException("Text cannot be null.");
            N = text.Length;
        }

        public int TextLength => Text.Length;

        // Returns the suffix array.
        public int[] GetSuffixArray
        {
            get
            {
                BuildSuffixArray();
                return SortedSuffixArray;
            }
        }

        // Returns the LCP array.
        public int[] LcpArray
        {
            get
            {
                BuildLcpArray();
                return Lcp;
            }
        }

        // Builds the suffix array by calling the construct() method.
        protected void BuildSuffixArray()
        {
            if (constructedSa) return;
            Construct();
            constructedSa = true;
        }

        // Builds the LCP array by first creating the SA and then running the kasai algorithm.
        protected void BuildLcpArray()
        {
            if (constructedLcpArray) return;
            BuildSuffixArray();
            Kasai();
            constructedLcpArray = true;
        }

        protected static int[] ToIntArray(string s)
        {
            if (s == null) return null;
            var t = new int[s.Length];
            for (var i = 0; i < s.Length; i++)
                t[i] = s[i];
            return t;
        }

        // The suffix array construction algorithm is left undefined
        // as there are multiple ways to do this.
        protected abstract void Construct();

        // Use Kasai algorithm to build LCP array
        // http://www.mi.fu-berlin.de/wiki/pub/ABI/RnaSeqP4/suffix-array.pdf
        private void Kasai()
        {
            Lcp = new int[N];
            var rank = new int[N];
           
            for (var i = 0; i < N; i++)
                rank[SortedSuffixArray[i]] = i;

            for (int i = 0, len = 0; i < N; i++)
            {
                if (rank[i] > 0)
                {
                    var k = SortedSuffixArray[rank[i] - 1];
                    
                    while (i + len < N && k + len < N && Text[i + len] == Text[k + len])
                        len++;
                    
                    Lcp[rank[i]] = len;
                    if (len > 0) len--;
                }
            }
        }
    }
}