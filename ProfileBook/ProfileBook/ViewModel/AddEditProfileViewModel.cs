using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class AddEditProfileViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;

        public AddEditProfileViewModel(INavigationService navigationService,
                                        IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;

            _user = new UserModel();
            _reg = new RegistrateModel();
        }
        public ICommand SaveCommand => new Command(Save);

        private async void Save()
        {
            try
            {
                if (CanSave())
                {
                    var user = new UserModel()
                    {
                        NickName = NickName,
                        Name = Name,
                        DateCreate = DateTime.Now,
                        Description = Description,
                        RegId = Id

                    };
                    await _repository.InsertAsync(user);

                    await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");
                    //_navigationService.GoBackAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Заполните поля NickName и Name", "Ok");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }


        private bool CanSave()
        {
            if (!String.IsNullOrEmpty(NickName) && !String.IsNullOrEmpty(Name))
            {
                return true;
            }
            return false;
        }
        #region -Public properties-

        #endregion

        #region -Overrides-
        public async override Task InitializeAsync(INavigationParameters parameters)
        {
            var _user = await _repository.GetAllAsync<UserModel>();
            Users = new ObservableCollection<UserModel>(_user);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(nameof(UserModel), out UserModel user))
            {
                Name = user.Name;
                NickName = user.NickName;
                Description = user.Description;
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == Users.ToString())
            {

            }
        }
        #endregion
    }
}
