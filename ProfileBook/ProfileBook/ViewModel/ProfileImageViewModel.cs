using Prism.Navigation;
using ProfileBook.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class ProfileImageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public ProfileImageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #region -Public properties-

        private string _profileImage;
        public string ProfileImage
        {
            get => _profileImage;
            set => SetProperty(ref _profileImage, value);
        }

        public ICommand TapCommand => new Command(TapCommands);
        #endregion

        #region -Methods-
        private void TapCommands()
        {
            NavigationParameters nav = new NavigationParameters();
            _navigationService.GoBackAsync(nav, true, false);
        }

        #endregion

        #region -Overrides-

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.TryGetValue(nameof(UserModel), out UserModel user))
            {
                ProfileImage = user.ProfileImage;
            }
        }

        #endregion
    }
}
