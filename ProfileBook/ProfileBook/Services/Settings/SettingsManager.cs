using Xamarin.Essentials;

namespace ProfileBook.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {

        public SettingsManager()
        {
            //грубая настройка без проверки пользователя
            App.CurrentLanguage = SelectedLanguage;

        }
        public string SelectedLanguage
        {
            get => Preferences.Get(nameof(SelectedLanguage), "EN");

            set => Preferences.Set(nameof(SelectedLanguage), value);
        }
    }
}
