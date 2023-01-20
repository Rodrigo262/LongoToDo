using System.Threading.Tasks;
using Xamarin.Forms;

namespace LongoToDo.Services
{
    public class AlertDialogService
    {
        private static AlertDialogService _instance;

        public static AlertDialogService Instance => _instance == null ? new AlertDialogService(): _instance;

        public async Task ShowDialogAsync(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> ShowDialogConfirmAsync(string title, string message, string cancel, string confirm)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, confirm, cancel);
        }

        public async Task<string> ShowDialogDisplayActionSheet(string title, string cancel, string[] buttons)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, cancel, null, buttons);
        }

        public async Task<string> ShowDialogDisplayActionSheet(string title, string message, string cancel, string confirm)
        {
            return await Application.Current.MainPage.DisplayPromptAsync(title, message, confirm, cancel);
        }
    }
}