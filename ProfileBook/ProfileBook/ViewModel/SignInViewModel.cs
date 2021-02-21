using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace ProfileBook.ViewModel
{
    public class SignInViewModel : BindableBase
    {
        private DelegateCommand _navigateCommand;
        private DelegateCommand _createUserCommand;

        private readonly INavigationService _navigationService;
        public SignInViewModel(INavigationService navigationService)
        {
            Title = "Users SignIn";
            _navigationService = navigationService;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand CreateUserCommand =>
        _createUserCommand ?? (_createUserCommand = new DelegateCommand(ExecuteCreateUserCommand));

        private void ExecuteCreateUserCommand()
        {
            _navigationService.NavigateAsync("SignUpView");
        }

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand));

        private void ExecuteNavigateCommand()
        {
            _navigationService.NavigateAsync("MainListView");
        }


    }
}
