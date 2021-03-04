using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.ViewModel;
using System.Collections.ObjectModel;

namespace ProfileBook.Services.Autorization
{
    public class AutorizationService : BaseViewModel, IAutorizationService
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository _repository;

        private int ID;

        public AutorizationService(INavigationService navigationService,
                                   IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;


            Regs = new ObservableCollection<RegistrateModel>();
            CheckDb();
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

        public async void CheckDb()
        {
            var _reg = await _repository.GetAllAsync<RegistrateModel>();
            Regs = new ObservableCollection<RegistrateModel>(_reg);
        }

        public bool SignIn(string Login, string Password)
        {
            CheckDb();
            foreach (var item in Regs)
            {
                if (item.Login == Login.ToString() && item.Password == Password.ToString())
                {
                    ID = item.Id;
                    return true;
                }
            }
            return false;
        }
    }
}
