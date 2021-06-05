using System;
using System.Collections.Generic;
using DataStructures.Queue;
using Xunit;

namespace DataStructuresTest.QueueTest
{
    public class QueueTest
    {
        private readonly List<IQueue<int>> queues = new();

        public QueueTest()
        {
            queues.Add(new ArrayQueue<int>());
            queues.Add(new LinkedListQueue<int>());
        }


        [Fact]
        public void TestEmptyQueue()
        {
            foreach (var queue in queues)
            {
                Assert.True(queue.IsEmpty());
                Assert.Equal(0, queue.Size());
            }
        }

        [Fact]
        public void TestDequeueOnEmpty()
        {
            foreach (var queue in queues)
                Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [Fact]
        public void TestPeekOnEmpty()
        {
            foreach (var queue in queues)
                Assert.Throws<InvalidOperationException>(() => queue.Peek());
        }

        [Fact]
        public void TestEnqueue()
        {
            foreach (var queue in queues)
            {
                queue.Enqueue(2);
                Assert.Equal(1, queue.Size());
            }
        }

        [Fact]
        public void TestPeek()
        {
            foreach (var queue in queues)
            {
                queue.Enqueue(2);
                Assert.Equal(2, queue.Peek());
                Assert.Equal(1, queue.Size());
            }
        }

        [Fact]
        public void TestDequeue()
        {
            foreach (var queue in queues)
            {
                queue.Enqueue(2);
                Assert.Equal(2, queue.Dequeue());
                Assert.Equal(0, queue.Size());
            }
        }

        [Fact]
        public void TestExhaustively()
        {
            foreach (var queue in queues)
            {
                Assert.True(queue.IsEmpty());
                queue.Enqueue(1);
                Assert.False(queue.IsEmpty());

                for (var i = 2; i <= 17; i++)
                    queue.Enqueue(i);
                for (var i = 1; i <= 16; i++)
                    queue.Dequeue();
                Assert.Equal(17, queue.Peek());

                for (var i = 1; i <= 33; i++)
                    queue.Enqueue(i);
                Assert.Equal(17, queue.Peek());
                Assert.Equal(34, queue.Size());
                Assert.Equal(17, queue.Dequeue());
                Assert.Equal(33, queue.Size());
                Assert.Equal(1, queue.Peek());
                Assert.Equal(33, queue.Size());
                Assert.Equal(1, queue.Dequeue());
                Assert.Equal(32, queue.Size());

                for (var i = 1; i <= 32; i++)
                    queue.Dequeue();
                Assert.True(queue.IsEmpty());
            }
        }
    }
}