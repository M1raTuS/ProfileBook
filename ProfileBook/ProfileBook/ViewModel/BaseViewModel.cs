using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Helpers;
using ProfileBook.Models;
using ProfileBook.Resources.Strings;
using ProfileBook.Services.Settings;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProfileBook.ViewModel
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware, IDisposable
    {
        public UserModel _user;
        public RegistrateModel _reg;

        public ObservableCollection<RegistrateModel> Registrate;

        public BaseViewModel()
        {
            Resources = new LocalizedResources(typeof(Resource), App.CurrentLanguage);
        }
        #region -Public properties-


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

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public void Dispose()
        {

        }

        #endregion
        public LocalizedResources Resources
        {
            get;
            private set;
        }


        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}