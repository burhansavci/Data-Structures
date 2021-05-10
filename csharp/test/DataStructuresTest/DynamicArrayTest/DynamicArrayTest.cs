using System;
using DataStructures.DynamicArray;
using Xunit;

namespace DataStructuresTest.DynamicArrayTest
{
    public class DynamicArrayTest
    {
        private readonly DynamicArray<int?> list;
        public DynamicArrayTest() => list = new DynamicArray<int?>();

        [Fact]
        public void TestEmptyList() => Assert.Empty(list);

        [Fact]
        public void TestRemovingEmpty() => Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(0));

        [Fact]
        public void TestIndexOutOfBounds()
        {
            list.Add(-56);
            list.Add(-53);
            list.Add(-55);
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(3));
        }

        [Fact]
        public void TestIndexOutOfBounds2()
        {
            for (var i = 0; i < 1000; i++) list.Add(789);
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(1000));
        }

        [Fact]
        public void TestIndexOutOfBounds3()
        {
            for (var i = 0; i < 1000; i++) list.Add(789);
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(-1));
        }

        [Fact]
        public void TestIndexOutOfBounds4()
        {
            for (var i = 0; i < 15; i++) list.Add(123);
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(-66));
        }

        [Fact]
        public void TestIndexOutOfBounds5()
        {
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Set(-1, 3));
        }

        [Fact]
        public void TestIndexOutOfBounds6()
        {
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Set(10, 3));
        }

        [Fact]
        public void TestIndexOutOfBounds7()
        {
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Set(15, 3));
        }

        [Fact]
        public void TestIndexOutOfBounds8()
        {
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Get(-2));
        }

        [Fact]
        public void TestIndexOutOfBounds9()
        {
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Get(10));
        }

        [Fact]
        public void TestIndexOutOfBounds10()
        {
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Get(15));
        }

        [Fact]
        public void TestRemoving()
        {
            var stringList = new DynamicArray<string>();
            string[] strs = {"a", "b", "c", "d", "e", null, "g", "h"};
            foreach (var s in strs) stringList.Add(s);

            var ret = stringList.Remove("c");
            Assert.True(ret);

            ret = stringList.Remove("c");
            Assert.False(ret);

            ret = stringList.Remove("h");
            Assert.True(ret);

            ret = stringList.Remove(null);
            Assert.True(ret);

            ret = stringList.Remove("a");
            Assert.True(ret);

            ret = stringList.Remove("a");
            Assert.False(ret);

            ret = stringList.Remove("h");
            Assert.False(ret);

            ret = stringList.Remove(null);
            Assert.False(ret);
        }

        [Fact]
        public void TestRemoving2()
        {
            var stringList = new DynamicArray<string>();
            string[] strs = {"a", "b", "c", "d"};
            foreach (var s in strs) stringList.Add(s);

            Assert.True(stringList.Remove("a"));
            Assert.True(stringList.Remove("b"));
            Assert.True(stringList.Remove("c"));
            Assert.True(stringList.Remove("d"));

            Assert.False(stringList.Remove("a"));
            Assert.False(stringList.Remove("b"));
            Assert.False(stringList.Remove("c"));
            Assert.False(stringList.Remove("d"));
        }

        [Fact]
        public void TestAddingElements()
        {
            int[] elems = {1, 2, 3, 4, 5, 6, 7};

            foreach (var elem in elems)
                list.Add(elem);

            for (var i = 0; i < elems.Length; i++)
                Assert.Equal(list.Get(i), elems[i]);
        }

        [Fact]
        public void TestAddAndRemove()
        {
            var longList = new DynamicArray<long>(0);

            for (var i = 0; i < 55; i++) longList.Add(44L);
            for (var i = 0; i < 55; i++) longList.Remove(44L);
            Assert.True(longList.IsEmpty);

            for (var i = 0; i < 55; i++) longList.Add(44L);
            for (var i = 0; i < 55; i++) longList.RemoveAt(0);
            Assert.True(longList.IsEmpty);

            for (var i = 0; i < 155; i++) longList.Add(44L);
            for (var i = 0; i < 155; i++) longList.Remove(44L);
            Assert.True(longList.IsEmpty);

            for (var i = 0; i < 155; i++) longList.Add(44L);
            for (var i = 0; i < 155; i++) longList.RemoveAt(0);
            Assert.True(longList.IsEmpty);
        }

        [Fact]
        public void TestAddSetRemove()
        {
            var longList = new DynamicArray<long>(0);

            for (var i = 0; i < 55; i++) longList.Add(44L);
            for (var i = 0; i < 55; i++) longList.Set(i, 33L);
            for (var i = 0; i < 55; i++) longList.Remove(33L);
            Assert.True(longList.IsEmpty);

            for (var i = 0; i < 55; i++) longList.Add(44L);
            for (var i = 0; i < 55; i++) longList.Set(i, 33L);
            for (var i = 0; i < 55; i++) longList.RemoveAt(0);
            Assert.True(longList.IsEmpty);

            for (var i = 0; i < 155; i++) longList.Add(44L);
            for (var i = 0; i < 155; i++) longList.Set(i, 33L);
            for (var i = 0; i < 155; i++) longList.Remove(33L);
            Assert.True(longList.IsEmpty);

            for (var i = 0; i < 155; i++) longList.Add(44L);
            for (var i = 0; i < 155; i++) longList.RemoveAt(0);
            Assert.True(longList.IsEmpty);
        }

        [Fact]
        public void TestSize()
        {
            int?[] elems = {-76, 45, 66, 3, null, 54, 33};
            for (int i = 0, sz = 1; i < elems.Length; i++, sz++)
            {
                list.Add(elems[i]);
                Assert.Equal(list.Size, sz);
            }
        }
    }
}