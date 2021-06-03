using System;
using System.Collections.Generic;
using DataStructures.Stack;
using Xunit;

namespace DataStructuresTest.StackTest
{
    public class StackTest
    {
        private readonly List<IStack<int>> stacks = new();

        public StackTest()
        {
            stacks.Add(new LinkedListStack<int>());
            stacks.Add(new ArrayStack<int>());
        }

        [Fact]
        public void TestEmptyStack()
        {
            foreach (var stack in stacks)
            {
                Assert.True(stack.IsEmpty());
                Assert.Equal(0, stack.Size());
            }
        }

        [Fact]
        public void TestPopOnEmpty()
        {
            foreach (var stack in stacks)
                Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }

        [Fact]
        public void TestPeekOnEmpty()
        {
            foreach (var stack in stacks)
                Assert.Throws<InvalidOperationException>(() => stack.Peek());
        }

        [Fact]
        public void TestPush()
        {
            foreach (var stack in stacks)
            {
                stack.Push(2);
                Assert.Equal(1, stack.Size());
            }
        }

        [Fact]
        public void TestPeek()
        {
            foreach (var stack in stacks)
            {
                stack.Push(2);
                Assert.Equal(2, stack.Peek());
                Assert.Equal(1, stack.Size());
            }
        }

        [Fact]
        public void TestPop()
        {
            foreach (var stack in stacks)
            {
                stack.Push(2);
                Assert.Equal(2, stack.Pop());
                Assert.Equal(0, stack.Size());
            }
        }

        [Fact]
        public void TestExhaustively()
        {
            foreach (var stack in stacks)
            {
                Assert.True(stack.IsEmpty());
                stack.Push(1);
                Assert.False(stack.IsEmpty());
                stack.Push(2);
                Assert.Equal(2, stack.Size());
                Assert.Equal(2, stack.Peek());
                Assert.Equal(2, stack.Size());
                Assert.Equal(2, stack.Pop());
                Assert.Equal(1, stack.Size());
                Assert.Equal(1, stack.Peek());
                Assert.Equal(1, stack.Size());
                Assert.Equal(1, stack.Pop());
                Assert.Equal(0, stack.Size());
                Assert.True(stack.IsEmpty());
            }
        }
    }
}