using System;
using LongoToDo.Features;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LongoToDo.Services;
using LongoToDo.Resources.Resx;

namespace LongoToDo
{
    public partial class App : Application
    {
        
        public static INavigation Nav { get; set; }

        public App ()
        {
            InitializeComponent();
            ConfigurarApp();

            NavigationPage navigationPage = new NavigationPage(new MainView());
            NavigationPage.SetHasNavigationBar(navigationPage.CurrentPage, false);
            MainPage = navigationPage;

            
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }

        private void ConfigurarApp()
            => LocalizationResourceManager.Current.Init(AppResources.ResourceManager);
    }
}