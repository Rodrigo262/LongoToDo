using System;
using FluentAssertions;
using Xunit;
using Moq;
using LongoToDo.Services;
using LongoToDo.Features;
using LongoToDo.Model;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
        }

        [Fact]
        public async void Test2()
        {

            NewItemVM main = new NewItemVM(dependencyService);

            await main.OnAppearingAsync();
            main.NameItem = "";

            (await main.SaveItemAsync()).Should().BeFalse();
        }

        [Fact]
        public void Test3()
        {
            MainVM main = new MainVM(dependencyService);

            main.ChangeState(null).Should().BeFalse() ;
        }

        [Fact]
        public void Test4()
        {
            MainVM main = new MainVM(dependencyService);

            ObservableCollection<ToDoItems> list = new ObservableCollection<ToDoItems>();
            main.ItemList = list;
            list.Add(new ToDoItems() { Name = "Item1" });

            main.ChangeState(new ToDoItems() { Name = "Item1" }).Should().BeFalse();
        }

        [Fact]
        public async void Test5()
        {
            Mock<IHttpClientService> mockHttp = new Mock<IHttpClientService>();

            mockHttp.Setup(e => e.CallApi<ToDoItems, object>(
                It.IsAny<ToDoItems>(),
                It.IsAny<string>(),
                It.IsAny<HttpRequestType>(),
                It.IsAny<int>(),
                It.IsAny<bool>())).Returns(Task.FromResult(new ResponseService<object>() { IsSuccessStatusCode = true }));

            dependencyService.Register<IHttpClientService>(mockHttp.Object);

            NewItemVM main = new NewItemVM(dependencyService);

            main.NameItem = "ItemTest";
            await main.SaveItemAsync();

            (await main.SaveItemAsync()).Should().BeFalse();

        }
    }
}