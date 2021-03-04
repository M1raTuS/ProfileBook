using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ProfileBook.ViewModel
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware
    {
        public UserModel _user;
        public RegistrateModel _reg;

        #region -Public properties-

        public int RegId
        {
            get => _user.RegId;
            set
            {
                _user.RegId = value;
            }
        }
        public string Name
        {
            get => _user.Name;
            set
            {
                _user.Name = value;
            }
        }

        public string NickName
        {
            get => _user.NickName;
            set
            {
                _user.NickName = value;
            }
        }
        public string Description
        {
            get => _user.Description;
            set
            {
                _user.Description = value;
            }
        }

        public DateTime CreateDateTime
        {
            get => _user.DateCreate;
            set
            {
                _user.DateCreate = value;
            }
        }
        public string ProfileImage
        {
            get => _user.ProfileImage;
            set
            {
                _user.ProfileImage = value;
            }
        }
        public int Id
        {
            get => _reg.Id;
            set
            {
                _reg.Id = value;
            }
        }
        public string Login
        {
            get => _reg.Login;
            set
            {
                _reg.Login = value;
            }
        }

        public string Password
        {
            get => _reg.Password;
            set
            {
                _reg.Password = value;
            }
        }

        private ObservableCollection<UserModel> users;
        public ObservableCollection<UserModel> Users
        {
            get => users;
            set => SetProperty(ref users, value);
        }

        private ObservableCollection<RegistrateModel> regs;
        public ObservableCollection<RegistrateModel> Regs
        {
            get => regs;
            set => SetProperty(ref regs, value);
        }

        #endregion

        #region -Methods-

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Initialize(INavigationParameters parameters)
        {
            
        }

        #endregion

    }
}