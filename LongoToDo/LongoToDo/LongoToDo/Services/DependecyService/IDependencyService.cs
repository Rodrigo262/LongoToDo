using System;
namespace LongoToDo.Services
{
	public interface IDependencyService
	{
        TService Get<TService>() where TService : class;
    }
}