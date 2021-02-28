using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    //TODO: Сделать адекватно тулбар view
    //TODO: Сделать нормально выделение из списка для контекстного меню
    public class MainListViewModel : BaseViewModel
    {

        private INavigationService _navigationService;
        private IRepository _repository;

        public MainListViewModel(INavigationService navigationService,
                                 IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;

            Users = new ObservableCollection<UserModel>();
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
        private async void TapCommand()
        {
            //await _navigationService.NavigateAsync(nameof(AddEditProfileView));
        }
        private async void EditContextMenu()
        {
            if (SelectedItem != null)
            {
                var user = new UserModel()
                {
                    Id = SelectedItem.Id,
                    Name = SelectedItem.Name,
                    NickName = SelectedItem.NickName,
                    Description = SelectedItem.Description
                };
                await _repository.UpdateAsync(user);
                await _navigationService.NavigateAsync(nameof(AddEditProfileView));
            }
        }
        private async void DeleteContextMenu()
        {
            if (SelectedItem != null)
            {
                if (await Application.Current.MainPage.DisplayAlert("Alert", "Подтверждаете ли вы удаление?", "Ok", "Cancel"))
                {
                    await _repository.DeleteAsync(SelectedItem);

                    Users.Remove(SelectedItem);
                    LoadUsers();
                }
            }
        }
        private async void LoadUsers()
        {
            var _users = await _repository.GetAllAsync<UserModel>();
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

        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            LoadUsers();
        }
        #endregion
    }
}

