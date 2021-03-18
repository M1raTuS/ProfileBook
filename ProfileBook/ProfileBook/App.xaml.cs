using Acr.UserDialogs;
using Plugin.Media;
using Prism.Ioc;
using Prism.Unity;
using ProfileBook.Enum;
using ProfileBook.Services.Autentification;
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
        public static string CurrentLanguage = "EN";
        public static Theme AppTheme { get; set; }
        public App()
        {

        }
       
        #region ---Ovverides---
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<IAutorizationService>(Container.Resolve<AutorizationService>());
            containerRegistry.RegisterInstance<IProfileService>(Container.Resolve<ProfileService>());
            containerRegistry.RegisterInstance<IAutentificationService>(Container.Resolve<AutentificationService>());

            containerRegistry.RegisterInstance(CrossMedia.Current.Initialize());

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
            //if (IsAutorized)
            {
               // NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");
            }
           // else
            {
                NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
            }
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
