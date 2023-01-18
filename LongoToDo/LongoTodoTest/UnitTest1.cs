using System;
using FluentAssertions;
using Xunit;

namespace LongoTodoTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            false.Should().BeFalse();
        }
    }
}

