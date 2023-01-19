using System;
using LongoToDo.Features;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LongoToDo
{
    public partial class App : Application
    {
        public static INavigation Nav { get; set; }

        public App ()
        {
            InitializeComponent();

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
    }
}

