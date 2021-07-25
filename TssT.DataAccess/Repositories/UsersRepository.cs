using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TssT.Core.Exceptions;
using TssT.Core.Models;

namespace TssT.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Entities.User> _userManager;
        private readonly IMapper _mapper;
        private IUsersRepository _usersRepositoryImplementation;

        public UsersRepository(ApplicationDbContext applicationDbContext, UserManager<Entities.User> userManager, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IdentityResult> Create(Core.Models.User newUser)
        {
            return await _userManager.CreateAsync(_mapper.Map<Core.Models.User, Entities.User>(newUser));
        }

        public async Task<IdentityResult> Update(Core.Models.User user)
        {
            return await _userManager.UpdateAsync(_mapper.Map<Core.Models.User, Entities.User>(user));
        }

        public async Task<IdentityResult> Delete(User user)
        {
            return await _userManager.DeleteAsync(_mapper.Map<Core.Models.User, Entities.User>(user));
        }

        public async Task<Core.Models.User> GetById(string userId)
        {
            return _mapper.Map<Entities.User, Core.Models.User>(await _userManager.FindByIdAsync(userId));
        }

        public async Task<User> GetByUserName(string username)
        {
            return _mapper.Map<Entities.User, Core.Models.User>(await _userManager.FindByNameAsync(username));
        }
    }
}