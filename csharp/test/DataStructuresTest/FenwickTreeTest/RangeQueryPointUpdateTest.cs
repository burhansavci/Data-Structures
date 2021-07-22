using System;
using DataStructures.FenwickTree;
using Xunit;

namespace DataStructuresTest.FenwickTreeTest
{
    public class RangeQueryPointUpdateTest
    {
        private const int MinRandNum = -1000;
        private const int MaxRandNum = +1000;
        private const int Loops = 1000;

        private static long UnusedVal;
        private static readonly Random Rand = new();

        public RangeQueryPointUpdateTest() => UnusedVal = RandValue();

        [Fact]
        public void TestIllegalCreation() => Assert.Throws<ArgumentException>(() => new RangeQueryPointUpdate(null));

        [Fact]
        public void TestIntervalSumPositiveValues()
        {
            long[] ar = {UnusedVal, 1, 2, 3, 4, 5, 6};
            var ft = new RangeQueryPointUpdate(ar);

            Assert.Equal(21, ft.Sum(1, 6));
            Assert.Equal(15, ft.Sum(1, 5));
            Assert.Equal(10, ft.Sum(1, 4));
            Assert.Equal(6, ft.Sum(1, 3));
            Assert.Equal(3, ft.Sum(1, 2));
            Assert.Equal(1, ft.Sum(1, 1));

            Assert.Equal(7, ft.Sum(3, 4));
            Assert.Equal(20, ft.Sum(2, 6));
            Assert.Equal(9, ft.Sum(4, 5));
            Assert.Equal(6, ft.Sum(6, 6));
            Assert.Equal(5, ft.Sum(5, 5));
            Assert.Equal(4, ft.Sum(4, 4));
            Assert.Equal(3, ft.Sum(3, 3));
            Assert.Equal(2, ft.Sum(2, 2));
            Assert.Equal(1, ft.Sum(1, 1));
        }

        [Fact]
        public void TestIntervalSumNegativeValues()
        {
            long[] ar = {UnusedVal, -1, -2, -3, -4, -5, -6};
            var ft = new RangeQueryPointUpdate(ar);

            Assert.Equal(-21, ft.Sum(1, 6));
            Assert.Equal(-15, ft.Sum(1, 5));
            Assert.Equal(-10, ft.Sum(1, 4));
            Assert.Equal(-6, ft.Sum(1, 3));
            Assert.Equal(-3, ft.Sum(1, 2));
            Assert.Equal(-1, ft.Sum(1, 1));

            Assert.Equal(-6, ft.Sum(6, 6));
            Assert.Equal(-5, ft.Sum(5, 5));
            Assert.Equal(-4, ft.Sum(4, 4));
            Assert.Equal(-3, ft.Sum(3, 3));
            Assert.Equal(-2, ft.Sum(2, 2));
            Assert.Equal(-1, ft.Sum(1, 1));
        }

        [Fact]
        public void TestIntervalSumNegativeValues2()
        {
            long[] ar = {UnusedVal, -76871, -164790};
            var ft = new RangeQueryPointUpdate(ar);

            for (int i = 0; i < Loops; i++)
            {
                Assert.Equal(-76871, ft.Sum(1, 1));
                Assert.Equal(-76871, ft.Sum(1, 1));
                Assert.Equal(-241661, ft.Sum(1, 2));
                Assert.Equal(-241661, ft.Sum(1, 2));
                Assert.Equal(-241661, ft.Sum(1, 2));
                Assert.Equal(-164790, ft.Sum(2, 2));
            }
        }

        [Fact]
        public void TestRandomizedStaticSumQueries()
        {
            for (int i = 1; i <= Loops; i++)
            {
                long[] randList = GenRandList(i);
                var ft = new RangeQueryPointUpdate(randList);

                for (var j = 0; j < Loops / 10; j++)
                    DoRandomRangeQuery(randList, ft);
            }
        }

        [Fact]
        public void TestRandomizedSetSumQueries()
        {
            for (var n = 2; n <= Loops; n++)
            {
                var randList = GenRandList(n);
                var ft = new RangeQueryPointUpdate(randList);

                for (var j = 0; j < Loops / 10; j++)
                {
                    var index = Rand.Next(1, n);
                    var randVal = RandValue();

                    randList[index] += randVal;
                    ft.Add(index, randVal);

                    DoRandomRangeQuery(randList, ft);
                }
            }
        }

        [Fact]
        public void TestReusability()
        {
            const int size = 1000;
            var ft = new RangeQueryPointUpdate(size);
            var arr = new long[size + 1];

            for (var loop = 0; loop < Loops; loop++)
            {
                for (var i = 1; i <= size; i++)
                {
                    var val = RandValue();
                    ft.Set(i, val);
                    arr[i] = val;
                }
                DoRandomRangeQuery(arr, ft);
            }
        }

        private static void DoRandomRangeQuery(long[] arr, RangeQueryPointUpdate ft)
        {
            var sum = 0L;
            var n = arr.Length - 1;

            var lo = LowBound(n);
            var hi = HighBound(lo, n);

            for (var k = lo; k <= hi; k++) sum += arr[k];

            Assert.Equal(sum, ft.Sum(lo, hi));
        }

        private static int LowBound(int n) => Rand.Next(1, n);

        private static int HighBound(int low, int n) => Math.Min(n, low + Rand.Next(1, n));

        private static long RandValue() => (long)(Rand.Next() * MaxRandNum * 2) + MinRandNum;

        // Generate a list of random numbers, one based
        private static long[] GenRandList(int sz)
        {
            var lst = new long[sz + 1];
            for (int i = 1; i <= sz; i++)
                lst[i] = RandValue();
            return lst;
        }
    }
}