using Prism.Ioc;
using Prism.Unity;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using ProfileBook.View;
using ProfileBook.ViewModel;
using Xamarin.Forms;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        public App()
        {

        }
        #region ---Ovverides---
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());

            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
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
