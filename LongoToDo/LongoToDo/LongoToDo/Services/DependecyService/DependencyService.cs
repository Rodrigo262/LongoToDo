using System;
using Xamarin.Forms;

namespace LongoToDo.Services
{
    public class CustomDependecyService : IDependencyService
    {
        private static CustomDependecyService instance;

        private CustomDependecyService() { }

        public static IDependencyService Instance => instance == null ? new CustomDependecyService() : instance;

        public TService Get<TService>() where TService : class => DependencyService.Get<TService>();
    }
}