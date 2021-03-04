using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System;
using System.Collections.ObjectModel;

namespace ProfileBook.ViewModel
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware, IDisposable
    {
        public UserModel _user;
        public RegistrateModel _reg;

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

    }
}