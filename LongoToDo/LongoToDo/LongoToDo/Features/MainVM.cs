using System;
using System.Threading.Tasks;
using LongoToDo.Base;
using LongoToDo.Services;
using Xamarin.Forms;
using LongoToDo.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace LongoToDo.Features
{
	public class MainVM : BaseVM
	{
		public ObservableCollection<ToDoItems> ItemList { get; set; }
		
		public MainVM() { }

		public MainVM(IDependencyService dependency) : base(dependency) { }

        public async override Task OnAppearingAsync()
        {
			try
			{
				IsBusy = true;
				IsVisible = true;

				IEnumerable<ToDoItems> list = await new ToDoItems(dependencyService).GetAll();

				if (list.Any())
				{
					ItemList = new ObservableCollection<ToDoItems>(list);
				}

				ItemList = null;
			}
			catch (Exception ex)
			{

			}

			IsVisible = false;
			IsVisible = false;
		}
    }
}