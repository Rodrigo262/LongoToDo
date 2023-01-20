using System;
using System.Threading.Tasks;
using LongoToDo.Base;
using LongoToDo.Services;
using Xamarin.Forms;
using LongoToDo.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace LongoToDo.Features
{
	public class MainVM : BaseVM
	{
		public ObservableCollection<ToDoItems> ItemList { get; set; }

        public ICommand NewItemCommand { get; protected set; }

        public MainVM() { }

		public MainVM(IDependencyService dependency) : base(dependency) { }


        public async override Task OnAppearingAsync()
        {
			try
			{
				IsBusy = true;
				IsVisible = true;

				InitCommand();

				IEnumerable<ToDoItems> list = await new ToDoItems(dependencyService).GetAll();

				if (list.Any())
				{
					ItemList = new ObservableCollection<ToDoItems>(list);
				}
			}
			catch (Exception ex)
			{

			}

			IsVisible = false;
			IsVisible = false;
		}

        private void InitCommand()
        {
            NewItemCommand = new Command(async () => await NewItemAsync());
        }

		public async Task NewItemAsync()
		{
			try
			{
				await PushAsync(new NewItemView());
			}
			catch (Exception ex)
			{

			}
		}
    }
}