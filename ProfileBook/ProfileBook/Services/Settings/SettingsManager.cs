using ProfileBook.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProfileBook.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        public int Count
        {
            get => Preferences.Get(nameof(Count), 5);
            set => Preferences.Set(nameof(Count), value);
        }

        private string _SelectedLanguage;

        public string SelectedLanguage
        {
            get => Preferences.Get(nameof(_SelectedLanguage), nameof(SelectedLanguage));
            
            set
            {
                Preferences.Set(nameof(_SelectedLanguage), value);
                SetLanguage();
            }
        }


        private void SetLanguage()
        {
            App.CurrentLanguage = SelectedLanguage;
            MessagingCenter.Send<object, CultureChangedMessage>(this,
                    string.Empty, new CultureChangedMessage(SelectedLanguage));
        }
       
    }
}
