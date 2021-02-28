using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;

        public SignInViewModel(INavigationService navigationService,
                                IRepository repository)
        {
            Title = "Users SignIn";

            _navigationService = navigationService;
            _repository = repository;

        }

        #region -Public properties-

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

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
            if (CheckDb(Login, Password))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Invalid login or password!", "Ok");
                Password = "";
            }
            else
            {
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");

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

        private bool CheckDb(string log, string pas)
        {
            foreach (var item in Regs)
            {
                if (item.Login == log.ToString() && item.Password == pas.ToString())
                {
                    return false;
                }
            }
            return true;
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

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            var _reg = await _repository.GetAllAsync<RegistrateModel>();
            Regs = new ObservableCollection<RegistrateModel>(_reg);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
        #endregion
    }
}
