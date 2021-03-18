using ProfileBook.ViewModel;

namespace ProfileBook.Services.Autorization
{
    public class AutorizationService : BaseViewModel, IAutorizationService
    {
        public AutorizationService()
        {
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
            IsAutorized = false;
        }

        public int GetCurrentUserId()
        {
            return GetCurrentId;
        }

    }
}
