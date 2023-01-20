using Xamarin.Forms;

namespace LongoToDo.Base
{
    public class BaseContentPage<T> : ContentPage where T : BaseVM, new()
    {
        protected double width;
        protected double height;

        protected T viewModel;
        public T ViewModel => viewModel == null ? new T() { Page = this } : viewModel;

        public BaseContentPage() => BindingContext = ViewModel;

        protected override void OnAppearing() => (BindingContext as BaseVM).OnAppearingAsync();

        protected override void OnDisappearing() => (BindingContext as BaseVM).OnDisappearingAsync();
    }
}