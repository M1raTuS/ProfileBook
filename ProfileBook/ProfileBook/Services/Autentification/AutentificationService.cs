using ProfileBook.Models;
using ProfileBook.Services.Autorization;
using ProfileBook.Services.Profile;
using ProfileBook.ViewModel;
using System.Collections.ObjectModel;

namespace ProfileBook.Services.Autentification
{
    public class AutentificationService : BaseViewModel, IAutentificationService
    {
        private readonly IProfileService _profile;
        private readonly IAutorizationService _autorization;

        public AutentificationService(IProfileService profile,
                                      IAutorizationService autorization)
        {
            _profile = profile;
            _autorization = autorization;

            Load();
        }

        public async void Load()
        {
            var _reg = await _profile.GetAllProfileListAsync();
            Regs = new ObservableCollection<RegistrateModel>(_reg);
        }

        public bool SignIn(string Login, string Password)
        {
            Load();

            foreach (var item in Regs)
            {
                if (item.Login == Login.ToString() && item.Password == Password.ToString())
                {
                    _autorization.GetCurrentId = item.Id;
                    _autorization.IsAutorized = true;
                    return true;
                }
            }
            return false;

        }

        public bool CheckLogin(string login)
        {
            Load();

            foreach (var item in Regs)
            {
                if (item.Login == login.ToString())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
