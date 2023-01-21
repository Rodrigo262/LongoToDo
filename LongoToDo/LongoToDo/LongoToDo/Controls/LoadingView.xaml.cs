using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LongoToDo.Controls
{	
	public partial class LoadingView : ContentView
	{	
		public LoadingView ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(LoadingView),
            string.Empty,
            BindingMode.TwoWay,
            propertyChanged: TextPropertyChanged);

        public string Text { get; set; }

        private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            LoadingView control = (LoadingView)bindable;
            control.label.Text = newValue.ToString();
        }
    }
}

