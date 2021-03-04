using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.ViewModel;
using System.Collections.ObjectModel;

namespace ProfileBook.Services.Autorization
{
    public class AutorizationService : BaseViewModel, IAutorizationService
    {
        private readonly IRepository _repository;


        public AutorizationService(IRepository repository)
        {
            _repository = repository;

            Regs = new ObservableCollection<RegistrateModel>();
            CheckDb();
        }

        private readonly bool _isAutorized = false;

        public bool IsAutorized
        {
            get => _isAutorized;
        }

        private int _getCurrentId;
        public int GetCurrentId
        {
            get => _getCurrentId;
        }


        public void Unauthorize()
        {
            throw new System.NotImplementedException();
        }

        private async void CheckDb()
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
                    _getCurrentId = item.Id;
                    return true;
                }
            }
            return false;
        }

        public int GetCurrentUserId()
        {
            return GetCurrentId;
        }
    }
}
