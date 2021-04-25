﻿using System;
using DataStructures.DynamicArray;
using Xunit;

namespace DataStructuresTest.DynamicArrayTest
{
    public class DynamicArrayTest
    {
        [Fact]
        public void TestEmptyList()
        {
            var list = new DynamicArray<int>();
            Assert.Empty(list);
        }

        [Fact]
        public void TestRemovingEmpty()
        {
            var list = new DynamicArray<int>();
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(0));
        }

        [Fact]
        public void TestIndexOutOfBounds()
        {
            var list = new DynamicArray<int>
            {
                    -56,
                    -53,
                    -55
            };
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(3));
        }

        [Fact]
        public void TestIndexOutOfBounds2()
        {
            var list = new DynamicArray<int>();
            for (var i = 0; i < 1000; i++) list.Add(789);
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(1000));
        }

        [Fact]
        public void TestIndexOutOfBounds3()
        {
            var list = new DynamicArray<int>();
            for (var i = 0; i < 1000; i++) list.Add(789);
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(-1));
        }

        [Fact]
        public void TestIndexOutOfBounds4()
        {
            var list = new DynamicArray<int>();
            for (var i = 0; i < 15; i++) list.Add(123);
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(-66));
        }

        [Fact]
        public void TestIndexOutOfBounds5()
        {
            var list = new DynamicArray<int>();
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Set(-1, 3));
        }

        [Fact]
        public void TestIndexOutOfBounds6()
        {
            var list = new DynamicArray<int>();
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Set(10, 3));
        }

        [Fact]
        public void TestIndexOutOfBounds7()
        {
            var list = new DynamicArray<int>();
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Set(15, 3));
        }

        [Fact]
        public void TestIndexOutOfBounds8()
        {
            var list = new DynamicArray<int>();
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Get(-2));
        }

        [Fact]
        public void TestIndexOutOfBounds9()
        {
            var list = new DynamicArray<int>();
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Get(10));
        }

        [Fact]
        public void TestIndexOutOfBounds10()
        {
            var list = new DynamicArray<int>();
            for (var i = 0; i < 10; i++) list.Add(12);
            Assert.Throws<IndexOutOfRangeException>(() => list.Get(15));
        }

        [Fact]
        public void TestRemoving()
        {
            var list = new DynamicArray<string>();
            string[] strs = {"a", "b", "c", "d", "e", null, "g", "h"};
            foreach (var s in strs) list.Add(s);

            var ret = list.Remove("c");
            Assert.True(ret);

            ret = list.Remove("c");
            Assert.False(ret);

            ret = list.Remove("h");
            Assert.True(ret);

            ret = list.Remove(null);
            Assert.True(ret);

            ret = list.Remove("a");
            Assert.True(ret);

            ret = list.Remove("a");
            Assert.False(ret);

            ret = list.Remove("h");
            Assert.False(ret);

            ret = list.Remove(null);
            Assert.False(ret);
        }

        [Fact]
        public void TestRemoving2()
        {
            var list = new DynamicArray<string>();
            string[] strs = {"a", "b", "c", "d"};
            foreach (var s in strs) list.Add(s);

            Assert.True(list.Remove("a"));
            Assert.True(list.Remove("b"));
            Assert.True(list.Remove("c"));
            Assert.True(list.Remove("d"));

            Assert.False(list.Remove("a"));
            Assert.False(list.Remove("b"));
            Assert.False(list.Remove("c"));
            Assert.False(list.Remove("d"));
        }

        [Fact]
        public void TestAddingElements()
        {
            var list = new DynamicArray<int>();

            int[] elems = {1, 2, 3, 4, 5, 6, 7};

            foreach (var elem in elems)
                list.Add(elem);

            for (var i = 0; i < elems.Length; i++)
                Assert.Equal(list.Get(i), elems[i]);
        }

        [Fact]
        public void TestAddAndRemove()
        {
            var list = new DynamicArray<long>(0);

            for (var i = 0; i < 55; i++) list.Add(44L);
            for (var i = 0; i < 55; i++) list.Remove(44L);
            Assert.True(list.IsEmpty);

            for (var i = 0; i < 55; i++) list.Add(44L);
            for (var i = 0; i < 55; i++) list.RemoveAt(0);
            Assert.True(list.IsEmpty);

            for (var i = 0; i < 155; i++) list.Add(44L);
            for (var i = 0; i < 155; i++) list.Remove(44L);
            Assert.True(list.IsEmpty);

            for (var i = 0; i < 155; i++) list.Add(44L);
            for (var i = 0; i < 155; i++) list.RemoveAt(0);
            Assert.True(list.IsEmpty);
        }

        [Fact]
        public void TestAddSetRemove()
        {
            var list = new DynamicArray<long>(0);

            for (var i = 0; i < 55; i++) list.Add(44L);
            for (var i = 0; i < 55; i++) list.Set(i, 33L);
            for (var i = 0; i < 55; i++) list.Remove(33L);
            Assert.True(list.IsEmpty);

            for (var i = 0; i < 55; i++) list.Add(44L);
            for (var i = 0; i < 55; i++) list.Set(i, 33L);
            for (var i = 0; i < 55; i++) list.RemoveAt(0);
            Assert.True(list.IsEmpty);

            for (var i = 0; i < 155; i++) list.Add(44L);
            for (var i = 0; i < 155; i++) list.Set(i, 33L);
            for (var i = 0; i < 155; i++) list.Remove(33L);
            Assert.True(list.IsEmpty);

            for (var i = 0; i < 155; i++) list.Add(44L);
            for (var i = 0; i < 155; i++) list.RemoveAt(0);
            Assert.True(list.IsEmpty);
        }

        [Fact]
        public void TestSize()
        {
            var list = new DynamicArray<int?>();

            int?[] elems = {-76, 45, 66, 3, null, 54, 33};
            for (int i = 0, sz = 1; i < elems.Length; i++, sz++)
            {
                list.Add(elems[i]);
                Assert.Equal(list.Size, sz);
            }
        }
    }
}