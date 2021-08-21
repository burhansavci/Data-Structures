using DataStructures.SuffixArray;
using Xunit;

namespace DataStructuresTest.SuffixArrayTest
{
    public class SuffixArrayTest
    {
        private const string AsciiLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        [Fact]
        public void SuffixArrayLength()
        {
            const string str = "ABCDE";

            SuffixArray sa1 = new SuffixArraySlow(str);

            Assert.Equal(str.Length, sa1.GetSuffixArray.Length);
        }

        [Fact]
        public void LcsUniqueCharacters()
        {
            SuffixArray sa1 = new SuffixArraySlow(AsciiLetters);

            for (var i = 0; i < sa1.GetSuffixArray.Length; i++)
            {
                Assert.Equal(0, sa1.LcpArray[i]);
            }
        }

        [Fact]
        public void IncreasingLcpTest()
        {
            const string uniqueChars = "KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK";

            SuffixArray sa1 = new SuffixArraySlow(uniqueChars);

            for (var i = 0; i < sa1.GetSuffixArray.Length; i++)
            {
                Assert.Equal(i, sa1.LcpArray[i]);
            }
        }

        [Fact]
        public void LcpTest1()
        {
            const string text = "ABBABAABAA";
            int[] lcpValues = {0, 1, 2, 1, 4, 2, 0, 3, 2, 1};

            SuffixArray sa1 = new SuffixArraySlow(text);

            for (var i = 0; i < sa1.GetSuffixArray.Length; i++)
            {
                Assert.Equal(lcpValues[i], sa1.LcpArray[i]);
            }
        }

        [Fact]
        public void LcpTest2()
        {
            const string text = "ABABABAABB";
            int[] lcpValues = {0, 1, 3, 5, 2, 0, 1, 2, 4, 1};

            SuffixArray sa1 = new SuffixArraySlow(text);

            for (var i = 0; i < sa1.GetSuffixArray.Length; i++)
            {
                Assert.Equal(lcpValues[i], sa1.LcpArray[i]);
            }
        }
    }
}