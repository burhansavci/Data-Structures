using System;
using System.Linq;
using DataStructures.PriorityQueue;
using Xunit;

namespace DataStructuresTest.PriorityQueueTest
{
    public class PriorityQueueTest
    {
        [Fact]
        public void TestOrderInPriorityQueue_RandomOrder()
        {
            var priorityQueue = new PriorityQueue<long>();

            priorityQueue.Add(23);
            priorityQueue.Add(42);
            priorityQueue.Add(4);
            priorityQueue.Add(16);
            priorityQueue.Add(8);
            priorityQueue.Add(1);
            priorityQueue.Add(3);
            priorityQueue.Add(100);
            priorityQueue.Add(5);
            priorityQueue.Add(7);

            var isRightOrder = IsRightOrderInPriorityQueue(priorityQueue);
            Assert.True(isRightOrder);
        }

        [Fact]
        public void TestOrderInPriorityQueue_AscendingOrder()
        {
            var priorityQueue = new PriorityQueue<long>();

            priorityQueue.Add(1);
            priorityQueue.Add(2);
            priorityQueue.Add(3);
            priorityQueue.Add(4);
            priorityQueue.Add(5);
            priorityQueue.Add(6);
            priorityQueue.Add(7);
            priorityQueue.Add(8);
            priorityQueue.Add(9);
            priorityQueue.Add(10);

            var isRightOrder = IsRightOrderInPriorityQueue(priorityQueue);
            Assert.True(isRightOrder);
        }

        [Fact]
        public void TestOrderInPriorityQueue_DecreasingOrder()
        {
            var priorityQueue = new PriorityQueue<long>();

            priorityQueue.Add(10);
            priorityQueue.Add(9);
            priorityQueue.Add(8);
            priorityQueue.Add(7);
            priorityQueue.Add(6);
            priorityQueue.Add(5);
            priorityQueue.Add(4);
            priorityQueue.Add(3);
            priorityQueue.Add(2);
            priorityQueue.Add(1);

            var isRightOrder = IsRightOrderInPriorityQueue(priorityQueue);
            Assert.True(isRightOrder);
        }

        private bool IsRightOrderInPriorityQueue<T>(PriorityQueue<T> priorityQueue) where T : IComparable<T>
        {
            var array = priorityQueue.ToArray();

            for (var i = 0; i * 2 + 1 < array.Length; ++i)
            {
                var leftChildIndex = i * 2 + 1;
                var rightChildIndex = leftChildIndex + 1;

                if (array[i].CompareTo(array[leftChildIndex]) > 0)
                    return false;

                if (rightChildIndex < array.Length && array[i].CompareTo(array[rightChildIndex]) > 0)
                    return true;
            }

            return true;
        }
    }
}