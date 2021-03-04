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

            //_user = new UserModel();
            //_reg = new RegistrateModel();
        }
        public ICommand SaveCommand => new Command(Save);

        private UserModel _userModel;
        public UserModel userModel
        {
            get => _userModel;
            set => SetProperty(ref _userModel, value);
        }
        private int _id;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _nickName;

        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }
        private string _description;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);

        }
        private string _profileImage;
        public string ProfileImage
        {
            get
            {
                if (_profileImage != null)
                {
                    return _profileImage;
                }

                return "pic_profile.png";
            }
            set => SetProperty(ref _profileImage, value);
        }
        private async void Save()
        {
            try
            {
                if (CanSave())
                {
                    var user = new UserModel()
                    {
                        Id = Id,
                        NickName = NickName,
                        Name = Name,
                        DateCreate = DateTime.Now,
                        Description = Description,
                        ProfileImage = ProfileImage
                       
                        //RegId = Id

                    };
                    if (Id>0)
                    {
                        var nav = new NavigationParameters();
                        nav.Add(nameof(UserModel), user);

                        await _repository.UpdateAsync(user);
                      await  _navigationService.GoBackAsync(nav);
                        //await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");
                    }
                    else
                    {
                        var nav = new NavigationParameters();
                        nav.Add(nameof(UserModel), user);

                        await _repository.InsertAsync(user);
                        //await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainListView)}");
                       await _navigationService.GoBackAsync(nav);
                    }

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

        #region -Overrides-
        //public async override void Initialize(INavigationParameters parameters)
        //{
        //    var _user = await _repository.GetAllAsync<UserModel>();
        //    Users = new ObservableCollection<UserModel>(_user);
        //}
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(nameof(UserModel), out UserModel user))
            {
                userModel = user;
                Name = user.Name;
                NickName = user.NickName;
                Description = user.Description;
                Id = user.Id;
                ProfileImage = user.ProfileImage;
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(Name) ||
                args.PropertyName == nameof(NickName) ||
                args.PropertyName == nameof(Description) ||
                args.PropertyName == nameof(Id) ||
                args.PropertyName == nameof(ProfileImage))
            {
                Name = Name;
                NickName = NickName;
                Description = Description; 
                ProfileImage = ProfileImage;
                Id = Id;
            }
        }
        #endregion
    }
}
