using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace ProfileBook.ViewModel
{
    public class SignUpViewModel : BindableBase
    {
        private DelegateCommand _goBack;
        private readonly INavigationService _navigationService;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public SignUpViewModel(INavigationService navigationService)
        {
            Title = "Users SignUp";
            _navigationService = navigationService;
        }

        public DelegateCommand GoBackCommand =>
            _goBack ?? (_goBack = new DelegateCommand(ExecuteGoBackCommand));

        async private void ExecuteGoBackCommand()
        {
            await _navigationService.GoBackAsync();
            //await _navigationService.NavigateAsync("SignInView");
        }
    }
}
