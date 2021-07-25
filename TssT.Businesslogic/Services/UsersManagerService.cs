using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TssT.Core.Interfaces;
using TssT.DataAccess.Repositories;

namespace TssT.Businesslogic.Services
{
    public class UsersManagerService : IUsersManagerService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersManagerService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }
        public async Task<IdentityResult> Create(Core.Models.User newUser)
        {
            if (newUser.Email == null || newUser.Email != "")
            {
                throw new NullReferenceException("Emait mustn't be empty");
            } 
            if (newUser.PasswordHash == null || newUser.PasswordHash != "")
            {
                throw new NullReferenceException("Password mustn't be empty");
            } 
            if (newUser.UserName == null || newUser.UserName != "")
            {
                throw new NullReferenceException("UserName mustn't be empty");
            } 
            
            
            
            return await _usersRepository.Create(newUser);
        }

        public async Task<IActionResult> Update(Core.Models.User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IActionResult> Delete(Core.Models.User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<Core.Models.User> GetById(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}