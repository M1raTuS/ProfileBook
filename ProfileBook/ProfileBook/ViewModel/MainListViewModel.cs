using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Profile;
using ProfileBook.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    //TODO: Сделать изменение языка контекстном меню
    public class MainListViewModel : BaseViewModel
    {

        private readonly INavigationService _navigationService;
        private readonly IProfileService _profile;

        public MainListViewModel(INavigationService navigationService,
                                 IProfileService profile)
        {
            _navigationService = navigationService;
            _profile = profile;

            LoadUsers();
        }

        #region -Public properties-

        private UserModel _selectedItem;
        public UserModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private bool _labelVisible;
        public bool LabelVisible
        {
            get => _labelVisible;
            set => SetProperty(ref _labelVisible, value);
        }

        private bool _listEnabled;
        public bool ListEnabled
        {
            get => _listEnabled;
            set => SetProperty(ref _listEnabled, value);
        }
        public ICommand LogOutCommand => new Command(LogOut);
        public ICommand AddProfileFloatingButtonCommand => new Command(AddProfileFloatingButton);
        public ICommand SelectedCommand => new Command(TapCommand);
        public ICommand EditContext => new Command(EditContextMenu);
        public ICommand DeleteContext => new Command(DeleteContextMenu);
        public ICommand SettingsCommand => new Command(SettingsMenu);

        #endregion

        #region -Methods-

        private async void LogOut()
        {
            await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
        }
        private async void AddProfileFloatingButton()
        {
            await _navigationService.NavigateAsync(nameof(AddEditProfileView));
        }
        private async void SettingsMenu()
        {
            await _navigationService.NavigateAsync(nameof(SettingsView));
        }
        private async void TapCommand(object user)
        {
            var nav = new NavigationParameters();
            nav.Add(nameof(UserModel), (UserModel)user);


            await _navigationService.NavigateAsync(nameof(ProfileImageView), nav, true, false);
        }
        private async void EditContextMenu(object obj)
        {
            var nav = new NavigationParameters();
            nav.Add(nameof(UserModel), (UserModel)obj);

            await _navigationService.NavigateAsync(nameof(AddEditProfileView), nav, false, true);

        }
        private async void DeleteContextMenu(object obj)
        {
            if (await Application.Current.MainPage.DisplayAlert("Alert", "Подтверждаете ли вы удаление?", "Ok", "Cancel"))
            {
                await _profile.DeleteProfileAsync((UserModel)obj);

                LoadUsers();
            }
        }
        private async void LoadUsers()
        {
            var _users = await _profile.GetProfileListByIdAsync();
            Users = new ObservableCollection<UserModel>(_users);
        }
        private async void SortingByName()
        {
            var _users = await _profile.GetProfileListByIdAsync();
            _users = _users.OrderBy(i => i.Name).ToList();
            Users = new ObservableCollection<UserModel>(_users);
        }
        private async void SortingByNickName()
        {
            var _users = await _profile.GetProfileListByIdAsync();
            _users = _users.OrderBy(i => i.NickName).ToList();
            Users = new ObservableCollection<UserModel>(_users);
        }
        private async void SortingByDate()
        {
            var _users = await _profile.GetProfileListByIdAsync();
            _users = _users.OrderBy(i => i.DateCreate).ToList();
            Users = new ObservableCollection<UserModel>(_users);
        }

        #endregion

        #region -Overrides-
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(Users))
            {
                if (Users.Count > 0)
                {
                    LabelVisible = false;
                    ListEnabled = true;
                }
                else
                {
                    LabelVisible = true;
                    ListEnabled = false;
                }
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(nameof(UserModel), out UserModel user))
            {
                LoadUsers();
            }
            if (parameters.TryGetValue("RadioCheck", out int Value))
            {
                switch (Value)
                {
                    case 1:
                        SortingByName();
                        break;
                    case 2:
                        SortingByNickName();
                        break;
                    case 3:
                        SortingByDate();
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

    }
}

