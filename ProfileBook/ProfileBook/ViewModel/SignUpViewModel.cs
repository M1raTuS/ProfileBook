﻿using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Autentification;
using ProfileBook.Services.Autorization;
using ProfileBook.Services.Profile;
using ProfileBook.Services.Repository;
using ProfileBook.Validation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;
        private readonly IAutorizationService _autorization;
        private readonly IProfileService _profile;
        private readonly IAutentificationService _autentification;

        public SignUpViewModel(INavigationService navigationService,
                                IRepository repository,
                                IAutorizationService autorization,
                                IProfileService profile,
                                IAutentificationService autentification)
        {
            _navigationService = navigationService;
            _repository = repository;
            _autorization = autorization;
            _profile = profile;
            _autentification = autentification;

            Regs = new ObservableCollection<RegistrateModel>();
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

        public ICommand AddUserCommand => new Command(AddUserAsync);

        #endregion

        #region -Methods-
        private async void AddUserAsync(object obj)
        {
            if (Password == ConfirmPassword)
            {
                var LoginValidation = Validator.StringValid(Login, Validator.Login);
                var PasswordValidation = Validator.StringValid(Password, Validator.Password);

                if (!LoginValidation)
                {
                    UserDialogs.Instance.Alert("Логин должен быть не менее 4 и не более 16 символов. Логин не должен начинаться с цифер", "Alert", "Ok");
                }
                else if (!PasswordValidation)
                {
                    UserDialogs.Instance.Alert("Пароль должен быть не менее 8 и не более 16 символов. Пароль должен содержать минимум одну заглавную букву, одну строчную букву и одну цифру", "Alert", "Ok");
                }
                else if (_autentification.CheckLogin(Login))
                {
                    UserDialogs.Instance.Alert("Этот логин уже занят", "Alert", "Ok");
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

                    var nav = new NavigationParameters();
                    nav.Add(nameof(RegistrateModel), (RegistrateModel)reg);

                    await _navigationService.GoBackAsync(nav, false, true);
                }
            }
            else
            {
                UserDialogs.Instance.Alert("Значения в полях Password и ConfirmPassword должны совпадать.", "Alert", "Ok");
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

        #endregion

        #region -Overrides-

        public async override void Initialize(INavigationParameters parameters)
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

        public async override void OnNavigatedFrom(INavigationParameters parameters)
        {
            var _reg = await _repository.GetAllAsync<RegistrateModel>();
            Regs = new ObservableCollection<RegistrateModel>(_reg);
        }

        #endregion
    }
}

