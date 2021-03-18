using ProfileBook.Models;
using ProfileBook.Services.Autorization;
using ProfileBook.Services.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository _repository;
        private readonly IAutorizationService _autorizationService;

        public ProfileService(IRepository repository,
                              IAutorizationService autorizationService)
        {
            _repository = repository;
            _autorizationService = autorizationService;
        }
        public async Task DeleteProfileAsync(UserModel user)
        {
            await _repository.DeleteAsync(user);
        }

        public async Task<List<RegistrateModel>> GetAllProfileListAsync()
        {
            var users = new List<RegistrateModel>();
            var list = await _repository.GetAllAsync<RegistrateModel>();
            if (list.Count > 0)
            {
                users.AddRange(list);
            }
            return users;
        }

        public async Task<List<UserModel>> GetProfileListByIdAsync()
        {
            var users = new List<UserModel>();
            var Id = _autorizationService.GetCurrentUserId();
            var list = await _repository.FindAsync<UserModel>(c => c.RegId == Id);
            if (list.Count > 0)
            {
                users.AddRange(list);
            }
            return users;
        }


        public async Task SaveProfileAsync(UserModel user)
        {
            await _repository.InsertAsync(user);
        }

        public async Task UpdateProfileAsync(UserModel user)
        {
            await _repository.UpdateAsync(user);
        }
    }
}
