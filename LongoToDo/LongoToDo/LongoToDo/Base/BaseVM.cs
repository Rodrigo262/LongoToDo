using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LongoToDo.Services;
using Xamarin.Forms;

namespace LongoToDo.Base
{
	public class BaseVM : ObservableObject
    {
        public INavigation FormsNavigation { get => App.Nav; }
        public ContentPage Page { get; set; }
        public bool IsBusy { get; set; }
        public bool IsVisible { get; set; }
        public bool IsRefreshing { get; set; }

        public IDependencyService DependencyService;

        public BaseVM()
		{
		}
        public BaseVM(CustomDependecyService dependecyService)
        {
            //DependencyService = dependecyService.Get<IHttpClientService>();
        }

        private ICommand navigationBackCommand;
        private ICommand navigationBackModalCommand;

        public ICommand NavigationBackCommand
         => navigationBackCommand == null ? new Command(async () => await NavigationBack()) : navigationBackCommand;

        public ICommand NavigationBackModalCommand
         => navigationBackModalCommand == null ? new Command(async () => await NavigationModalBack()) : navigationBackModalCommand;

        public virtual Task OnAppearingAsync() => Task.CompletedTask;
        public virtual Task OnDisappearingAsync() => Task.CompletedTask;


        protected Task SetMainPage(Page page, bool setHasNavigationBar = true)
        {
            NavigationPage navigationPage = new NavigationPage(page);
            NavigationPage.SetHasNavigationBar(navigationPage.CurrentPage, setHasNavigationBar);

            Device.InvokeOnMainThreadAsync(() =>
            {
                Application.Current.MainPage = navigationPage;
                App.Nav = App.Current.MainPage.Navigation;
            });
            return Task.FromResult(true);
        }

        protected Task PushAsync(Page page)
            => Device.InvokeOnMainThreadAsync(() => FormsNavigation.PushAsync(page, true));
        protected Task PushModalAsync(Page page)
            => Device.InvokeOnMainThreadAsync(() => FormsNavigation.PushModalAsync(page, true));
        protected Task NavigationBack() => FormsNavigation.PopAsync(true);
        protected Task NavigationModalBack() => FormsNavigation.PopModalAsync(true);
    }
}

