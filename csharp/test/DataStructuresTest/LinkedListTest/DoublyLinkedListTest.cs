using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.LinkedList;
using Xunit;

namespace DataStructuresTest.LinkedListTest
{
    public class DoublyLinkedListTest
    {
        private readonly DoublyLinkedList<int?> list;
        private static readonly int Loops = 10000;
        private static readonly int TestSz = 40;

        public DoublyLinkedListTest() => list = new DoublyLinkedList<int?>();

        [Fact]
        public void TestEmptyList()
        {
            Assert.Empty(list);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void TestRemoveFirstOfEmpty() => Assert.Throws<IndexOutOfRangeException>(() => list.RemoveFirst());

        [Fact]
        public void TestRemoveLastOfEmpty() => Assert.Throws<IndexOutOfRangeException>(() => list.RemoveLast());

        [Fact]
        public void TestPeekFirstOfEmpty() => Assert.Throws<IndexOutOfRangeException>(() => list.PeekFirst());

        [Fact]
        public void TestPeekLastOfEmpty() => Assert.Throws<IndexOutOfRangeException>(() => list.PeekLast());


        [Fact]
        public void TestAddFirst()
        {
            list.AddFirst(3);
            Assert.Equal(1, list.Count);
            list.AddFirst(5);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void TestAddLast()
        {
            list.AddLast(3);
            Assert.Equal(1, list.Count);
            list.AddLast(5);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void TestAddAt()
        {
            list.AddAt(0, 1);
            Assert.Equal(1, list.Count);

            list.AddAt(1, 2);
            Assert.Equal(2, list.Count);

            list.AddAt(1, 3);
            Assert.Equal(3, list.Count);

            list.AddAt(2, 4);
            Assert.Equal(4, list.Count);

            list.AddAt(1, 8);
            Assert.Equal(5, list.Count);
        }


        [Fact]
        public void TestRemoveFirst()
        {
            list.AddFirst(3);
            Assert.Equal(3, list.RemoveFirst());
            Assert.Empty(list);
        }

        [Fact]
        public void TestRemoveLast()
        {
            list.AddLast(4);
            Assert.Equal(4, list.RemoveLast());
            Assert.Empty(list);
        }

        [Fact]
        public void TestPeekFirst()
        {
            list.AddFirst(4);
            Assert.Equal(4, list.PeekFirst());
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void TestPeekLast()
        {
            list.AddLast(4);
            Assert.Equal(4, list.PeekLast());
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void TestPeeking()
        {
            // 5
            list.AddFirst(5);
            Assert.Equal(5, list.PeekFirst());
            Assert.Equal(5, list.PeekLast());

            // 6 - 5
            list.AddFirst(6);
            Assert.Equal(6, list.PeekFirst());
            Assert.Equal(5, list.PeekLast());

            // 7 - 6 - 5
            list.AddFirst(7);
            Assert.Equal(7, list.PeekFirst());
            Assert.Equal(5, list.PeekLast());


            // 7 - 6 - 5 - 8
            list.AddLast(8);
            Assert.Equal(7, list.PeekFirst());
            Assert.Equal(8, list.PeekLast());

            // 7 - 6 - 5
            list.RemoveLast();
            Assert.Equal(7, list.PeekFirst());
            Assert.Equal(5, list.PeekLast());

            // 7 - 6
            list.RemoveLast();
            Assert.Equal(7, list.PeekFirst());
            Assert.Equal(6, list.PeekLast());

            // 6
            list.RemoveFirst();
            Assert.Equal(6, list.PeekFirst());
            Assert.Equal(6, list.PeekLast());
        }

        [Fact]
        public void TestRemoving()
        {
            var strings = new DoublyLinkedList<string>
            {
                    "a",
                    "b",
                    "c",
                    "d",
                    "e",
                    "f"
            };
            strings.Remove("b");
            strings.Remove("a");
            strings.Remove("d");
            strings.Remove("e");
            strings.Remove("c");
            strings.Remove("f");
            Assert.Equal(0, strings.Count);
        }

        [Fact]
        public void TestRemoveAt()
        {
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.RemoveAt(0);
            list.RemoveAt(2);
            Assert.Equal(2, list.PeekFirst());
            Assert.Equal(3, list.PeekLast());
            list.RemoveAt(1);
            list.RemoveAt(0);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void TestClear()
        {
            list.Add(22);
            list.Add(33);
            list.Add(44);
            Assert.Equal(3, list.Count);
            list.Clear();
            Assert.Equal(0, list.Count);

            list.Add(22);
            list.Add(33);
            list.Add(44);
            Assert.Equal(3, list.Count);
            list.Clear();
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void TestRandomizedRemoving()
        {
            var systemLinkedList = new LinkedList<int>();
            for (var loops = 0; loops < Loops; loops++)
            {
                list.Clear();
                systemLinkedList.Clear();

                var randNums = TestUtil.GenRandList(TestSz);
                foreach (var value in randNums)
                {
                    systemLinkedList.AddLast(value);
                    list.Add(value);
                }
                TestUtil.Shuffle(randNums);

                foreach (var rmVal in randNums)
                {
                    Assert.Equal(systemLinkedList.Remove(rmVal), list.Remove(rmVal));
                    Assert.Equal(systemLinkedList.Count, list.Count);

                    using var iter1 = systemLinkedList.GetEnumerator();
                    using var iter2 = list.GetEnumerator();
                    while (iter1.MoveNext() && iter2.MoveNext())
                        Assert.Equal(iter1.Current, iter2.Current);
                }

                list.Clear();
                systemLinkedList.Clear();

                foreach (var value in randNums)
                {
                    systemLinkedList.AddLast(value);
                    list.Add(value);
                }

                var rand = new Random();
                // Try removing elements whether or not they exist
                for (int i = 0; i < randNums.Count; i++)
                {
                    var rmVal = rand.Next(0, list.Count);
                    Assert.Equal(systemLinkedList.Remove(rmVal), list.Remove(rmVal));
                    Assert.Equal(systemLinkedList.Count, list.Count);

                    using var iter1 = systemLinkedList.GetEnumerator();
                    using var iter2 = list.GetEnumerator();
                    while (iter1.MoveNext() && iter2.MoveNext())
                        Assert.Equal(iter1.Current, iter2.Current);
                }
            }
        }

        [Fact]
        public void TestRandomizedRemoveAt()
        {
            var systemLinkedList = new LinkedList<int>();

            for (var loops = 0; loops < Loops; loops++)
            {
                list.Clear();
                systemLinkedList.Clear();

                var randNums = TestUtil.GenRandList(TestSz);

                foreach (var value in randNums)
                {
                    systemLinkedList.AddLast(value);
                    list.Add(value);
                }

                var rand = new Random();
                for (var i = 0; i < randNums.Count; i++)
                {
                    var rmIndex = rand.Next(0, list.Count);

                    var num1 = RemoveAt(systemLinkedList, rmIndex).Value;
                    var num2 = list.RemoveAt(rmIndex);

                    Assert.Equal(num1, num2);
                    Assert.Equal(systemLinkedList.Count, list.Count);

                    using var iter1 = systemLinkedList.GetEnumerator();
                    using var iter2 = list.GetEnumerator();
                    while (iter1.MoveNext() && iter2.MoveNext())
                        Assert.Equal(iter1.Current, iter2.Current);
                }
            }
        }

        [Fact]
        public void TestRandomizedIndexOf()
        {
            var systemLinkedList = new LinkedList<int>();

            for (var loops = 0; loops < Loops; loops++)
            {
                systemLinkedList.Clear();
                list.Clear();

                var randNums = TestUtil.GenUniqueRandList(TestSz);

                foreach (var value in randNums)
                {
                    systemLinkedList.AddLast(value);
                    list.Add(value);
                }

                TestUtil.Shuffle(randNums);

                foreach (var elem in randNums)
                {
                    int index1 = systemLinkedList.Select((item, inx) => new {item, inx})
                                                 .First(x => x.item == elem).inx;
                    var index2 = list.IndexOf(elem);

                    Assert.Equal(index1, index2);
                    Assert.Equal(systemLinkedList.Count, list.Count);

                    using var iter1 = systemLinkedList.GetEnumerator();
                    using var iter2 = list.GetEnumerator();
                    while (iter1.MoveNext() && iter2.MoveNext())
                        Assert.Equal(iter1.Current, iter2.Current);
                }
            }
        }

        private static LinkedListNode<T> RemoveAt<T>(LinkedList<T> list, int index)
        {
            var currentNode = list.First;
            for (int i = 0; i <= index && currentNode != null; i++)
            {
                if (i != index)
                {
                    currentNode = currentNode.Next;
                    continue;
                }

                list.Remove(currentNode);
                return currentNode;
            }

            throw new IndexOutOfRangeException();
        }
    }
}