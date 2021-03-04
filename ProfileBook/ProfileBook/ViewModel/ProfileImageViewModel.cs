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

        public ICommand AddUserCommand => new Command(TapCommand);


        private string _profileImage;
        public string ProfileImage
        {
            get => _profileImage;
            set => SetProperty(ref _profileImage, value);
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.TryGetValue(nameof(UserModel), out UserModel user))
            {
                ProfileImage = user.ProfileImage;
            }
        }

        private void TapCommand()
        {
            _navigationService.GoBackAsync();
        }
    }
}
