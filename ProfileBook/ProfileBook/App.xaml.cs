using Prism.Ioc;
using Prism.Unity;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using ProfileBook.View;
using ProfileBook.ViewModel;
using System;
using System.IO;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        public const string DATABASE_NAME = "users.db";
        public static Repository database;
        public static Repository Database
        {
            get
            {
                if (database == null)
                {
                    database = new Repository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }
        public App()
        {
            //MainPage = new NavigationPage(new SignInView());
        }
        #region ---Ovverides---
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"NavigationPage/{nameof(SignInView)}");
            // NavigationService.NavigateAsync($"{nameof(MainListView)}");
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion

    }
}
