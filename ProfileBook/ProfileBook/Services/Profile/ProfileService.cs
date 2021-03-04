using ProfileBook.Models;
using ProfileBook.Services.Autorization;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private IRepository _repository;
        private IAutorizationService _autorizationService;

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

        public async Task<List<UserModel>> GetProfileListAsync()
        {
            throw new NotImplementedException();
        }

        public async  Task SaveProfileAsync(UserModel user)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProfileAsync(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
