using Prism.Ioc;
using Prism.Unity;
using ProfileBook.Services.Autorization;
using ProfileBook.Services.Profile;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using ProfileBook.View;
using ProfileBook.ViewModel;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        private readonly IAutorizationService _autorization;
        public App()
        {

        }
        public App(IAutorizationService autorization)
        {
            _autorization = autorization;
        }
        #region ---Ovverides---
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<IAutorizationService>(Container.Resolve<AutorizationService>());
            containerRegistry.RegisterInstance<IProfileService>(Container.Resolve<ProfileService>());

            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            containerRegistry.RegisterForNavigation<ProfileImageView, ProfileImageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            //if (_autorization.IsAutorized)
            //{
            //    NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");
            //}
            //else 
            //{ 
                NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}"); 
            //}
            
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
