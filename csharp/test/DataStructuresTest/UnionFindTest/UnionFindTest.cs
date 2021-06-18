using System;
using System.Collections.Generic;
using DataStructures.UnionFind;
using Xunit;

namespace DataStructuresTest.UnionFindTest
{
    public class UnionFindTest
    {
        private readonly List<IUnionFind<int>> ufs;
        private readonly List<IUnionFind<int>> ufs2;
        private const int Sz = 7;

        public UnionFindTest()
        {
            ufs = new List<IUnionFind<int>>
            {
                    new IntUnionFind(5),
                    new UnionFind<int>(new[] {0, 1, 2, 3, 4})
            };
            ufs2 = new List<IUnionFind<int>>
            {
                    new IntUnionFind(Sz),
                    new UnionFind<int>(new[] {0, 1, 2, 3, 4, 5, 6})
            };
        }

        [Fact]
        public void TestNumComponents()
        {
            foreach (var uf in ufs)
            {
                Assert.Equal(5, uf.NumComponents);

                uf.Unify(0, 1);
                Assert.Equal(4, uf.NumComponents);

                uf.Unify(1, 0);
                Assert.Equal(4, uf.NumComponents);

                uf.Unify(1, 2);
                Assert.Equal(3, uf.NumComponents);

                uf.Unify(0, 2);
                Assert.Equal(3, uf.NumComponents);

                uf.Unify(2, 1);
                Assert.Equal(3, uf.NumComponents);

                uf.Unify(3, 4);
                Assert.Equal(2, uf.NumComponents);

                uf.Unify(4, 3);
                Assert.Equal(2, uf.NumComponents);

                uf.Unify(1, 3);
                Assert.Equal(1, uf.NumComponents);

                uf.Unify(4, 0);
                Assert.Equal(1, uf.NumComponents);
            }
        }

        [Fact]
        public void TestComponentSize()
        {
            foreach (var uf in ufs)
            {
                Assert.Equal(1, uf.ComponentSize(0));
                Assert.Equal(1, uf.ComponentSize(1));
                Assert.Equal(1, uf.ComponentSize(2));
                Assert.Equal(1, uf.ComponentSize(3));
                Assert.Equal(1, uf.ComponentSize(4));

                uf.Unify(0, 1);
                Assert.Equal(2, uf.ComponentSize(0));
                Assert.Equal(2, uf.ComponentSize(1));
                Assert.Equal(1, uf.ComponentSize(2));
                Assert.Equal(1, uf.ComponentSize(3));
                Assert.Equal(1, uf.ComponentSize(4));

                uf.Unify(1, 0);
                Assert.Equal(2, uf.ComponentSize(0));
                Assert.Equal(2, uf.ComponentSize(1));
                Assert.Equal(1, uf.ComponentSize(2));
                Assert.Equal(1, uf.ComponentSize(3));
                Assert.Equal(1, uf.ComponentSize(4));

                uf.Unify(1, 2);
                Assert.Equal(3, uf.ComponentSize(0));
                Assert.Equal(3, uf.ComponentSize(1));
                Assert.Equal(3, uf.ComponentSize(2));
                Assert.Equal(1, uf.ComponentSize(3));
                Assert.Equal(1, uf.ComponentSize(4));

                uf.Unify(0, 2);
                Assert.Equal(3, uf.ComponentSize(0));
                Assert.Equal(3, uf.ComponentSize(1));
                Assert.Equal(3, uf.ComponentSize(2));
                Assert.Equal(1, uf.ComponentSize(3));
                Assert.Equal(1, uf.ComponentSize(4));

                uf.Unify(2, 1);
                Assert.Equal(3, uf.ComponentSize(0));
                Assert.Equal(3, uf.ComponentSize(1));
                Assert.Equal(3, uf.ComponentSize(2));
                Assert.Equal(1, uf.ComponentSize(3));
                Assert.Equal(1, uf.ComponentSize(4));

                uf.Unify(3, 4);
                Assert.Equal(3, uf.ComponentSize(0));
                Assert.Equal(3, uf.ComponentSize(1));
                Assert.Equal(3, uf.ComponentSize(2));
                Assert.Equal(2, uf.ComponentSize(3));
                Assert.Equal(2, uf.ComponentSize(4));

                uf.Unify(4, 3);
                Assert.Equal(3, uf.ComponentSize(0));
                Assert.Equal(3, uf.ComponentSize(1));
                Assert.Equal(3, uf.ComponentSize(2));
                Assert.Equal(2, uf.ComponentSize(3));
                Assert.Equal(2, uf.ComponentSize(4));

                uf.Unify(1, 3);
                Assert.Equal(5, uf.ComponentSize(0));
                Assert.Equal(5, uf.ComponentSize(1));
                Assert.Equal(5, uf.ComponentSize(2));
                Assert.Equal(5, uf.ComponentSize(3));
                Assert.Equal(5, uf.ComponentSize(4));

                uf.Unify(4, 0);
                Assert.Equal(5, uf.ComponentSize(0));
                Assert.Equal(5, uf.ComponentSize(1));
                Assert.Equal(5, uf.ComponentSize(2));
                Assert.Equal(5, uf.ComponentSize(3));
                Assert.Equal(5, uf.ComponentSize(4));
            }
        }

        [Fact]
        public void TestConnectivity()
        {
            foreach (var uf in ufs2)
            {
                for (var i = 0; i < Sz; i++) Assert.True(uf.Connected(i, i));
                uf.Unify(0, 2);
                Assert.True(uf.Connected(0, 2));
                Assert.True(uf.Connected(2, 0));
                Assert.False(uf.Connected(0, 1));
                Assert.False(uf.Connected(3, 1));
                Assert.False(uf.Connected(6, 4));
                Assert.False(uf.Connected(5, 0));

                for (var i = 0; i < Sz; i++) Assert.True(uf.Connected(i, i));
                uf.Unify(3, 1);
                Assert.True(uf.Connected(0, 2));
                Assert.True(uf.Connected(2, 0));
                Assert.True(uf.Connected(1, 3));
                Assert.True(uf.Connected(3, 1));
                Assert.False(uf.Connected(0, 1));
                Assert.False(uf.Connected(1, 2));
                Assert.False(uf.Connected(2, 3));
                Assert.False(uf.Connected(1, 0));
                Assert.False(uf.Connected(2, 1));
                Assert.False(uf.Connected(3, 2));
                Assert.False(uf.Connected(1, 4));
                Assert.False(uf.Connected(2, 5));
                Assert.False(uf.Connected(3, 6));

                for (var i = 0; i < Sz; i++) Assert.True(uf.Connected(i, i));
                uf.Unify(2, 5);
                Assert.True(uf.Connected(0, 2));
                Assert.True(uf.Connected(2, 0));
                Assert.True(uf.Connected(1, 3));
                Assert.True(uf.Connected(3, 1));
                Assert.True(uf.Connected(0, 5));
                Assert.True(uf.Connected(5, 0));
                Assert.True(uf.Connected(5, 2));
                Assert.True(uf.Connected(2, 5));
                Assert.False(uf.Connected(0, 1));
                Assert.False(uf.Connected(1, 2));
                Assert.False(uf.Connected(2, 3));
                Assert.False(uf.Connected(1, 0));
                Assert.False(uf.Connected(2, 1));
                Assert.False(uf.Connected(3, 2));
                Assert.False(uf.Connected(4, 6));
                Assert.False(uf.Connected(4, 5));
                Assert.False(uf.Connected(1, 6));


                for (var i = 0; i < Sz; i++) Assert.True(uf.Connected(i, i));
                // Connect everything
                uf.Unify(1, 2);
                uf.Unify(3, 4);
                uf.Unify(4, 6);

                for (var i = 0; i < Sz; i++)
                for (var j = 0; j < Sz; j++)
                    Assert.True(uf.Connected(i, j));
            }
        }

        [Fact]
        public void TestSize()
        {
            foreach (var uf in ufs)
            {
                Assert.Equal(5, uf.Size);

                uf.Unify(0, 1);
                uf.Find(3);
                Assert.Equal(5, uf.Size);

                uf.Unify(1, 2);
                Assert.Equal(5, uf.Size);

                uf.Unify(0, 2);
                uf.Find(1);
                Assert.Equal(5, uf.Size);

                uf.Unify(2, 1);
                Assert.Equal(5, uf.Size);

                uf.Unify(3, 4);
                uf.Find(0);
                Assert.Equal(5, uf.Size);

                uf.Unify(4, 3);
                uf.Find(3);
                Assert.Equal(5, uf.Size);

                uf.Unify(1, 3);
                Assert.Equal(5, uf.Size);

                uf.Find(2);
                uf.Unify(4, 0);
                Assert.Equal(5, uf.Size);
            }
        }

        [Fact]
        public void TestBadUnionFindCreation1() => Assert.Throws<ArgumentException>(() => new IntUnionFind(-1));

        [Fact]
        public void TestBadUnionFindCreation2() => Assert.Throws<ArgumentException>(() => new IntUnionFind(-3463));

        [Fact]
        public void TestBadUnionFindCreation3() => Assert.Throws<ArgumentException>(() => new IntUnionFind(0));

        [Fact]
        public void TestBadUnionFindCreation4() => Assert.Throws<ArgumentNullException>(() => new UnionFind<int>(null));
    }
}