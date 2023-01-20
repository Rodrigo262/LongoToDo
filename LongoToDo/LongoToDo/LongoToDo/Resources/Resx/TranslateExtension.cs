using System;
namespace LongoToDo
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [ContentProperty(nameof(Text))]
    public class TranslateExtension : IMarkupExtension<BindingBase>
    {
        public string Text { get; set; } = string.Empty;
        public string StringFormat { get; set; }

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
#if NETSTANDARD1_0
                throw new NotSupportedException("Traslate XAML MarkupExtension is not supported on .NET Standard 1.0");
#else
            #region Required work-around to preven linker from removing the implementation
            #endregion

            Binding binding = new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $"[{Text}]",
                Source = LocalizationResourceManager.Current,
                StringFormat = StringFormat
            };
            return binding;
#endif
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);
    }
}