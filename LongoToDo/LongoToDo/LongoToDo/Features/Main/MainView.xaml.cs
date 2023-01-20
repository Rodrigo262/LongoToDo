using System;
using System.Collections.Generic;
using LongoToDo.Base;
using Xamarin.Forms;

namespace LongoToDo.Features
{
    public partial class MainView : MainViewViewXaml
    {
        public MainView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(App.Nav == null)
                App.Nav = Application.Current.MainPage.Navigation;
        }
    }
    public partial class MainViewViewXaml : BaseContentPage<MainVM> { }
}