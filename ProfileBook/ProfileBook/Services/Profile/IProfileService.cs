﻿using ProfileBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Services.Profile
{
    public interface IProfileService
    {
        Task<List<UserModel>> GetProfileListByIdAsync();
        Task<List<RegistrateModel>> GetAllProfileListAsync();
        Task SaveProfileAsync(UserModel user);
        Task UpdateProfileAsync(UserModel user);
        Task DeleteProfileAsync(UserModel user);
    }
}
