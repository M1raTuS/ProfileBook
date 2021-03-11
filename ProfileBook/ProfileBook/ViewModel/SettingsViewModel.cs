using Prism.Navigation;
using ProfileBook.Helpers;
using ProfileBook.Models;
using ProfileBook.Services.Profile;
using ProfileBook.Services.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ProfileBook.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ISettingsManager _settings;
        private readonly IProfileService _profile;

        public SettingsViewModel(IProfileService profile)
        {
            _SelectedLanguage = App.CurrentLanguage;
            _profile = profile;
        }
        //public SettingsViewModel(ISettingsManager settings)
        //{
        //    //_SelectedLanguage = App.CurrentLanguage;
        //    _settings = settings;
        //    _settings.SelectedLanguage = App.CurrentLanguage;
        //}

        //_settings.

        private int _value;

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                //OnPropertyChanged("RadioCheck1");
                //OnPropertyChanged("RadioCheck2");
                //OnPropertyChanged("RadioCheck3");
            }
        }

        public bool RadioCheck1
        {
            get => Value.Equals(1);
            set => Value = 1;
        }
        public bool RadioCheck2
        {
            get => Value.Equals(2);
            set => Value = 2;
        }
        public bool RadioCheck3
        {
            get => Value.Equals(3);
            set => Value = 3;
        }

        private bool _radioCheck;

        public bool RadioCheck
        {
            get
            {
                return _radioCheck;
            }
            set
            {
                _radioCheck = value;
            }
        }
        
        public List<string> Languages { get; set; } = new List<string>()
        {
           "EN",
           "RU"
        };
        
        private int _selectedIndex;

        public int SelectedIndex
        {
            get
            {
                //if (_selectedIndex != null)
                //{
                //    return _selectedIndex;
                //}
               return _selectedIndex = Languages.IndexOf(SelectedLanguage);
                //return _settings.SelectedLanguage;
            }
            set
            {
                if (value != -1)
                {
                    _selectedIndex = value;
                }
                //_settings.SelectedLanguage;
            }
        }

        private string _SelectedLanguage;

        public string SelectedLanguage
        {
            get
            {
                return _SelectedLanguage;
            }
            set
            {
                if (value != null)
                {
                    _SelectedLanguage = value;
                    SetLanguage();
                }
            }
        }

        private void SetLanguage()
        {
            // App.CurrentLanguage = _settings.SelectedLanguage;
            // MessagingCenter.Send<object, CultureChangedMessage>(this,
            //         string.Empty, new CultureChangedMessage(_settings.SelectedLanguage));
            App.CurrentLanguage = SelectedLanguage;
            MessagingCenter.Send<object, CultureChangedMessage>(this,
                    string.Empty, new CultureChangedMessage(SelectedLanguage));
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add(nameof(RadioCheck),Value);
        }
    }
}
