using System;
using FluentAssertions;
using Xunit;
using Moq;
using LongoToDo.Services;
using LongoToDo.Features;
using System.ComponentModel;

namespace LongoTodoTest
{
    public class UnitTest1
    {
        DependencyServiceStub dependencyService = new DependencyServiceStub();

        [Fact]
        public async void Test1()
        {
            Mock<IHttpClientService> mockHttp = new Mock<IHttpClientService>();

            mockHttp.Setup(e => e.Ping()).ReturnsAsync(true);

            dependencyService.Register<IHttpClientService>(mockHttp.Object);

            MainVM main = new MainVM(dependencyService);

            await main.OnAppearingAsync();

            //main.Ping.Should().BeTrue();
        }
    }
}