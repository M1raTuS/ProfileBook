﻿using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Validation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;

        public SignUpViewModel(INavigationService navigationService,
                                IRepository repository)
        {
            Title = "Users SignUp";

            _navigationService = navigationService;
            _repository = repository;

        }

        #region -Public properties-

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
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

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }
        private bool _buttonEnabled;
        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set => SetProperty(ref _buttonEnabled, value);
        }

        public ICommand AddUserCommand => new Command(AddUser);

        #endregion

        #region -Methods-
        private async void AddUser()
        {
            if (Password == ConfirmPassword)
            {
                var LoginValidation = Validator.StringValid(Login, Validator.Login);
                var PasswordValidation = Validator.StringValid(Password, Validator.Password);

                if (!LoginValidation)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Логин должен быть не менее 4 и не более 16 символов. Логин не должен начинаться с цифер", "Ok");
                }
                else if (!PasswordValidation)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Пароль должен быть не менее 8 и не более 16 символов. Пароль должен содержать минимум одну заглавную букву, одну строчную букву и одну цифру", "Ok");
                }
                else if (!CheckDb(Login))
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Этот логин уже занят", "Ok");
                }
                else
                {
                    var reg = new RegistrateModel()
                    {
                        Login = Login,
                        Password = Password
                    };
                    var id = await _repository.InsertAsync(reg);

                    reg.Id = id;

                    Regs.Add(reg);

                    //_navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
                    await _navigationService.GoBackAsync();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Значения в полях Password и ConfirmPassword должны совпадать.", "Ok");
            }
        }

        private bool CanSignIn()
        {
            if (!String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password) && !String.IsNullOrEmpty(ConfirmPassword))
            {
                return true;
            }
            return false;
        }

        private bool CheckDb(string log)
        {
            foreach (var item in Regs)
            {
                if (item.Login == log.ToString())
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region -Overrides-

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            var _reg = await _repository.GetAllAsync<RegistrateModel>();
            Regs = new ObservableCollection<RegistrateModel>(_reg);
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Login) ||
                args.PropertyName == nameof(Password) ||
                args.PropertyName == nameof(ConfirmPassword))
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
        #endregion
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }
    }
}
