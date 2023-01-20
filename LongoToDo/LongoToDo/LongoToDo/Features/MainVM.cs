using System;
using System.Threading.Tasks;
using LongoToDo.Base;
using LongoToDo.Services;
using Xamarin.Forms;

namespace LongoToDo.Features
{
	public class MainVM : BaseVM
	{
		public MainVM() { }

		public MainVM(IDependencyService dependency) : base(dependency) { }

        public async override Task OnAppearingAsync()
        {
			try
			{
				IsBusy = true;
				IsVisible = false;

				//Ping = await dependencyService.Get<IHttpClientService>().Ping();
				//var b = await dependencyService.Get<IHttpClientService>().CallApi<object, object>(null, string.Empty, HttpRequestType.Get, 80, false);
			}
			catch (Exception ex)
			{

			}

			IsVisible = false;
			IsVisible = false;
		}
    }
}