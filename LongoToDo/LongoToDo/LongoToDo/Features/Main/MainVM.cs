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
using LongoToDo.Resources.Resx;
using System.Diagnostics;

namespace LongoToDo.Features
{
	public class MainVM : BaseVM
	{
		public ObservableCollection<ToDoItems> ItemList { get; set; }

        public ICommand NewItemCommand { get; protected set; }
        public ICommand DeleteItemCommand { get; protected set; }
        public ICommand TapCommand { get; protected set; }
        public ICommand RefreshCommand { get; protected set; }

        public MainVM() { }

		public MainVM(IDependencyService dependency) : base(dependency) { }


        public async override Task OnAppearingAsync()
        {
			try
			{
				IsBusy = true;
				IsVisible = true;

				InitCommand();
				await GetItems();	
			}
			catch (Exception ex)
			{
                Debug.WriteLine(ex.Message);
            }

            IsVisible = false;
			IsVisible = false;
		}

        private void InitCommand()
        {
            NewItemCommand = new Command(async () => await NewItemAsync());
            DeleteItemCommand = new Command<ToDoItems>(async (ToDoItems toDoItems) => await DeleteItemAsync(toDoItems));
            TapCommand = new Command<ToDoItems>((ToDoItems toDoItems) => ChangeState(toDoItems));
            RefreshCommand = new Command(async () => await RefreshAsync());
        }

        public bool ChangeState(ToDoItems toDoItems)
        {
            try
            {
                if (toDoItems != null)
                {
                    int index = ItemList.IndexOf(toDoItems);

                    if (index != -1)
                    {
                        toDoItems.IsComplete = !toDoItems.IsComplete;
                        ItemList[index] = toDoItems;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }

        private async Task RefreshAsync()
        {
            try
            {
                IsRefreshing = true;
                await GetItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            IsRefreshing = false;
        }

        private async Task GetItems()
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
            }
			catch (Exception ex)
			{
                Debug.WriteLine(ex.Message);
            }

            IsVisible = false;
            IsVisible = false;
        }

        private async Task NewItemAsync()
		{
			try
			{
                IsBusy = true;
                IsVisible = true;

                await PushAsync(new NewItemView());
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
            IsVisible = false;
            IsVisible = false;
        }

        private async Task DeleteItemAsync(ToDoItems toDoItems)
		{
            try
            {
                IsBusy = true;
                IsVisible = true;

                bool response = await new ToDoItems(dependencyService).DeleteItem(toDoItems);

				if (response)
				{
                    await AlertDialogService.Instance.ShowDialogAsync(AppResources.Info, $"ToDo item {toDoItems.Name} has been deleted correctly", AppResources.BtnOk);
                    await GetItems();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            IsVisible = false;
            IsVisible = false;
        }
    }
}