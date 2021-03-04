using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.ViewModel;

namespace ProfileBook.Services.Autorization
{
    public class AutorizationService : BaseViewModel, IAutorizationService
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;


        public AutorizationService(INavigationService navigationService,
                                   IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;

        }
        private bool _isAutorized;

        public bool IsAutorized
        {
            get => _isAutorized;
        }

        public int _getCurrentId;
        private int GetCurrentId 
        {
            get => _getCurrentId;
            set => SetProperty(ref _getCurrentId, value);
        }


        public int GetCurrentUserId()
        {
            _repository.FindAsync<RegistrateModel>(c => c.Id < _getCurrentId);
            Load();

            return 1;
        }

        public void Unauthorize()
        {
            throw new System.NotImplementedException();
        }

        private async void Load()
        {
            var _reg = await _repository.GetAllAsync<RegistrateModel>();
            foreach (var item in _reg)
            {

            }
        }
    }
}
