using ProfileBook.Services.Repository;
using ProfileBook.ViewModel;

namespace ProfileBook.Services.Autorization
{
    public class AutorizationService : BaseViewModel, IAutorizationService
    {
        private readonly IRepository _repository;
        public AutorizationService()
        {
        }
        public AutorizationService(IRepository repository)
        {
            _repository = repository;
            //CheckDb();
        }

        private bool _isAutorized;

        public bool IsAutorized
        {
            get => _isAutorized;
            set => SetProperty(ref _isAutorized, value);
        }

        private int _getCurrentId;
        public int GetCurrentId
        {
            get => _getCurrentId;
            set => SetProperty(ref _getCurrentId, value);
        }

        public void Unauthorize()
        {
            throw new System.NotImplementedException();
        }

        public int GetCurrentUserId()
        {
            return GetCurrentId;
        }

        public bool SignIn(string Login, string Password)
        {

            foreach (var item in Regs)
            {
                if (item.Login == Login.ToString() && item.Password == Password.ToString())
                {
                    _getCurrentId = item.Id;
                    _isAutorized = true;
                    return true;
                }
            }
            return false;

        }
    }
}
