using Graph.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Graph.Tests
{
    public class BigInMemoryStackTests
    {
        public const int MaxHashSetSize = 110921543;

        public static IEnumerable<object[]> StackTestSizes()
        {
            yield return new object[] { 1 };
            yield return new object[] { 10 };
            yield return new object[] { 1000 };
            yield return new object[] { MaxHashSetSize };
            //yield return new object[] { int.MaxValue };
            yield return new object[] { long.MaxValue };
        }

        [Theory]
        [MemberData(nameof(StackTestSizes))]
        public void BigInMemoryStack_PushAndPop_DoesNotThrow(long size)
        {
            // Arrange
            var stack = new BigInMemoryStack<long>();

            // Act
            for(var i = 0; i <= size; ++i)
            {
                stack.Push(size);
            }

            for(var i = 0; i <= size; ++i)
            {
                stack.Pop();
            }

            // Assert
            Assert.True(stack.IsEmpty);
        }
    }
}
