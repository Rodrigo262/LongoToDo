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
    }
    public partial class MainViewViewXaml : BaseContentPage<MainVM> { }
}