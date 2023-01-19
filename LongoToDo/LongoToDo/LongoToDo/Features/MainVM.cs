using System;
using System.Threading.Tasks;
using LongoToDo.Base;
using LongoToDo.Services;
using Xamarin.Forms;

namespace LongoToDo.Features
{
	public class MainVM : BaseVM
	{
		//private IDependencyService dependecyService;

		public MainVM()
		{
		}

        public MainVM(object t)
        {

        }
        public async override Task OnAppearingAsync()
        {
			try
			{
				IsBusy = true;
				IsVisible = false;

				var a = await Xamarin.Forms.DependencyService.Get<IHttpClientService>().Ping();
				await DependencyService.Get<IHttpClientService>().Ping();
				
			}
			catch (Exception ex)
			{

			}

			IsVisible = false;
			IsVisible = false;
		}
    }
}