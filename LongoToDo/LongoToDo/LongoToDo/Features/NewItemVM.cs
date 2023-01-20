using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LongoToDo.Base;
using LongoToDo.Services;
using Xamarin.Forms;
using LongoToDo.Model;
using LongoToDo.Resources.Resx;

namespace LongoToDo.Features
{
	public class NewItemVM : BaseVM
	{
        public string NameItem { get; set; }

        public NewItemVM()
		{
		}

        public ICommand SaveItemCommand { get; protected set; }

        public NewItemVM(IDependencyService dependency) : base(dependency) { }

        public async override Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();
            InitCommand();
        }

        private void InitCommand()
        {
            SaveItemCommand = new Command(async () => await SaveItemAsync());
        }

        public async Task SaveItemAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(NameItem))
                {
                    await AlertDialogService.Instance.ShowDialogAsync(AppResources.Info, AppResources.MsgRequiredField, AppResources.BtnOk);
                }
                else
                {

                    bool response = await new ToDoItems().SaveItem(new ToDoItems() { Name = NameItem});
                    if (response)
                        await NavigationBack();
                    else
                        await AlertDialogService.Instance.ShowDialogAsync(AppResources.Error, AppResources.Error, AppResources.BtnOk);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}