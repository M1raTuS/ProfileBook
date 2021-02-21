using Xamarin.Essentials;

namespace ProfileBook.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        public int Count
        {
            get => Preferences.Get(nameof(Count), 5);
            set => Preferences.Set(nameof(Count), value);
        }
    }
}
