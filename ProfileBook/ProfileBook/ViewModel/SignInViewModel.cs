using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Autentification;
using ProfileBook.Services.Autorization;
using ProfileBook.Services.Profile;
using ProfileBook.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IAutorizationService _autorization;
        private readonly IProfileService _profileService;
        private readonly IAutentificationService _autentification;

        public SignInViewModel(INavigationService navigationService,
                               IAutorizationService autorization,
                               IProfileService profileService,
                               IAutentificationService autentification)
        {
            _navigationService = navigationService;
            _autorization = autorization;
            _profileService = profileService;
            _autentification = autentification;
        }

        #region -Public properties-

        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _buttonEnabled;
        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set => SetProperty(ref _buttonEnabled, value);
        }

        public ICommand SignInCommand => new Command(SignInUser);
        public ICommand SignUpCommand => new Command(SignUpUser);

        #endregion

        #region -Methods-

        private async void SignInUser()
        {
            //TODO: Иногда не заходит после регистрации.
            var res = _autentification.SignIn(Login, Password);

            if (res)
            {
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");
            }
            else
            {
                UserDialogs.Instance.Alert("Invalid login or password!", "Alert", "Ok");
                Password = "";
            }
        }

        async void SignUpUser()
        {
            await _navigationService.NavigateAsync($"{nameof(SignUpView)}");
        }

        private bool CanSignIn()
        {
            if (!String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password))
            {
                return true;
            }
            return false;
        }

        //private bool CheckDb(string log, string pas)
        //{
        //    foreach (var item in Regs)
        //    {
        //        if (item.Login == log.ToString() && item.Password == pas.ToString())
        //        {
        //            _autorization.GetCurrentId = item.Id;
        //            _autorization.IsAutorized = true;
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        private async void Load()
        {
            var _reg = await _profileService.GetAllProfileListAsync();
            Regs = new ObservableCollection<RegistrateModel>(_reg);
        }

        #endregion

        #region -Overrides-

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Login) ||
                args.PropertyName == nameof(Password))
            {
                if (CanSignIn())
                {
                    ButtonEnabled = true;
                }
                else
                {
                    ButtonEnabled = false;
                }
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Load();

            if (parameters.TryGetValue(nameof(RegistrateModel), out RegistrateModel reg))
            {
                Login = reg.Login;
            }
        }
        #endregion
    }
}
