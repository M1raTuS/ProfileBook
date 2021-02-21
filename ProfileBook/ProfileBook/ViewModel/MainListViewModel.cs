using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace ProfileBook.ViewModel
{
    public class MainListViewModel : BindableBase
    {
        private DelegateCommand _navigateCommand;
        private readonly INavigationService _navigationService;

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigateCommand));
        public MainListViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        private void ExecuteNavigateCommand()
        {
            _navigationService.NavigateAsync("MainListView");
        }
    }
}
