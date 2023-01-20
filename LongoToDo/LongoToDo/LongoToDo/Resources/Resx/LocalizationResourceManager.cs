using System;
using System.Globalization;
using System.Resources;
using Xamarin.Essentials;
using LongoToDo.Base;
using LongoToDo.Resources.Resx;

namespace LongoToDo
{
    public class LocalizationResourceManager : ObservableObject
    {
        private const string LanguageKey = nameof(LanguageKey);

        static readonly Lazy<LocalizationResourceManager> currentHolder = new Lazy<LocalizationResourceManager>(() => new LocalizationResourceManager());

        public static LocalizationResourceManager Current => currentHolder.Value;

        ResourceManager resourceManager;
        CultureInfo currentCulture = new CultureInfo(Preferences.Get(LanguageKey, CultureInfo.CurrentCulture.TwoLetterISOLanguageName));

        //public static LocalizationResourceManager Instance { get; } = new LocalizationResourceManager();

        private LocalizationResourceManager() { }
        public void Init(ResourceManager resource) => resourceManager = resource;

        public string GetValue(string text)
        {
            if (resourceManager == null)
                throw new Exception("Debe llamar primero al Init");

            return resourceManager.GetString(text, CurrentCulture) ?? throw new NullReferenceException($"{nameof(text)}: {text} not found");
        }

        public string this[string text] => GetValue(text);

        public CultureInfo CurrentCulture
        {
            get => currentCulture;
            set => SetProperty(ref currentCulture, value, null);
        }

        public void SetCulture(CultureInfo language)
        {
            Preferences.Set(LanguageKey, language.TwoLetterISOLanguageName);
            currentCulture = new CultureInfo(Preferences.Get(LanguageKey, language.TwoLetterISOLanguageName));
            Init(AppResources.ResourceManager);
            Invalidate();
        }
    }
}