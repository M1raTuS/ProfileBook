using Prism.Navigation;
using ProfileBook.Enum;
using ProfileBook.Helpers;
using ProfileBook.Interface;
using ProfileBook.Services.Settings;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ISettingsManager _settings;
        public SettingsViewModel(ISettingsManager settings)
        {
            _settings = settings;
            SelectedLanguage = _settings.SelectedLanguage;
        }


        #region -Sort-

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

        #endregion

        #region -Language-

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
                return _selectedIndex = Languages.IndexOf(SelectedLanguage);
            }
            set
            {
                if (value != -1)
                {
                    _selectedIndex = value;
                }
            }
        }

        private string _selectedLanguage;

        public string SelectedLanguage
        {
            get
            {
                return _selectedLanguage;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _selectedLanguage, value);
                    SetLanguage();

                }
            }
        }

        private void SetLanguage()
        {
            App.CurrentLanguage = SelectedLanguage;
            MessagingCenter.Send<object, CultureChangedMessage>(this,
                    string.Empty, new CultureChangedMessage(SelectedLanguage));
        }
        #endregion

        #region -Themechoise-

        private bool _themeCheck;

        public bool ThemeCheck
        {
            get
            {
                if (App.AppTheme == Theme.Dark)
                {
                    return true;
                }

                return _themeCheck;
            }
            set
            {
                SetTheme(value);
                SetProperty(ref _themeCheck, value);
            }
        }
        Theme themeRequested;
        void SetTheme(bool status)
        {

            if (status)
            {
                themeRequested = Theme.Dark;
            }
            else
            {
                themeRequested = Theme.Light;
            }

            DependencyService.Get<IAppTheme>().SetAppTheme(themeRequested);
        }

        #endregion


        #region -override-

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add(nameof(RadioCheck), Value);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(SelectedLanguage))
            {
                _settings.SelectedLanguage = SelectedLanguage;
            }
        }
        #endregion



    }
}
