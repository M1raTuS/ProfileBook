using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Autorization;
using ProfileBook.Services.Profile;
using ProfileBook.Services.Repository;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class AddEditProfileViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;
        private readonly IAutorizationService _autorization;
        private readonly IProfileService _profile;

        public AddEditProfileViewModel(INavigationService navigationService,
                                        IRepository repository,
                                        IAutorizationService autorization,
                                        IProfileService profile)
        {
            _navigationService = navigationService;
            _repository = repository;
            _autorization = autorization;
            _profile = profile;
        }

        #region -Property-

        public ICommand SaveCommand => new Command(Save);
        public ICommand ImageTappedCommand => new Command(ImageTap);


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
        private int _regId;

        public int RegId
        {
            get => _regId;
            set => SetProperty(ref _regId, value);
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

        #endregion


        #region -Overrides-
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

        #region -Methods-

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
                        ProfileImage = ProfileImage,
                        RegId = _autorization.GetCurrentId
                    };
                    if (Id > 0)
                    {
                        var nav = new NavigationParameters();
                        nav.Add(nameof(UserModel), user);


                        await _profile.UpdateProfileAsync(user);
                        await _navigationService.GoBackAsync(nav);
                    }
                    else
                    {
                        var nav = new NavigationParameters();
                        nav.Add(nameof(UserModel), user);


                        await _profile.SaveProfileAsync(user);
                        await _navigationService.GoBackAsync(nav);
                    }

                }
                else
                {
                    UserDialogs.Instance.Alert("Заполните поля NickName и Name", "Alert", "Ok"); 
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

        private void ImageTap()
        {
            var file = new ActionSheetConfig()
                .SetTitle("Choose your action")
                .Add("Camera", () => OpenCamera(), "ic_camera_alt")
                .Add("Gallery", () => OpenGalery(), "ic_collections")
                .SetCancel();

            UserDialogs.Instance.ActionSheet(file);
        }

        private async void OpenGalery()
        {
            try
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile img = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions());
                    if (img != null)
                    {
                        ProfileImage = img.Path;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private async void OpenCamera()
        {
            try
            {
                if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile img = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                        SaveToAlbum = true,
                        SaveMetaData = true,
                        Directory = "temp",
                        MaxWidthHeight = 1500,
                        CompressionQuality = 75,
                        RotateImage = Device.RuntimePlatform == Device.Android ? true : false,
                        Name = $"{DateTime.Now}.jpg"
                    });
                    if (img != null)
                    {
                        ProfileImage = img.Path;
                    }

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        #endregion

    }
}
