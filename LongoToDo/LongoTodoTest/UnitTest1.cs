using System;
using FluentAssertions;
using Xunit;
using Moq;
using LongoToDo.Services;

namespace LongoTodoTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Mock<IHttpClientService> mockHttp = new Mock<IHttpClientService>();

            mockHttp.Setup(e => e.Ping()).ReturnsAsync(true);

            mockHttp.Verify();

            //false.Should().BeFalse();
        }
    }
}

