using System;
using System.Data;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TssT.Core.Exceptions;
using TssT.Core.Interfaces;
using TssT.Core.Models;
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
            if (newUser == null)
            {
                throw new NullReferenceException("newUser mustn't be null");
            }
            if (newUser.Email == null || newUser.Email == "")
            {
                throw new NullReferenceException("Emait mustn't be empty");
            } 
            if (newUser.PasswordHash == null || newUser.PasswordHash == "")
            {
                throw new NullReferenceException("Password mustn't be empty");
            } 
            if (newUser.UserName == null || newUser.UserName == "")
            {
                throw new NullReferenceException("UserName mustn't be empty");
            }

            var result = await _usersRepository.Create(newUser);
            
            if (result.Errors.Count() > 0)
            {
                string errors = null;
                foreach (var error in result.Errors)
                {
                    errors += error.Description + "\n";
                }
                
                throw new ArgumentException(errors);
            }
            
            return result;
        }

        public async Task<IdentityResult> Update(Core.Models.User user)
        {
            if (user == null)
            {
                throw new NullReferenceException("user mustn't be null");
            }

            throw new System.NotImplementedException();
        }

        public async Task<IdentityResult> DeleteById(string userId)
        {
            if (userId == null)
            {
                throw new NullReferenceException("userId mustn't be null");
            }

            var user = await GetById(userId);
            return await _usersRepository.Delete(user);
        }
        
        public async Task<IdentityResult> DeleteByUserName(string userName)
        {
            if (userName == null)
            {
                throw new NullReferenceException("userName mustn't be null");
            }

            var user = await GetByUserName(userName);
            return await _usersRepository.Delete(user);
        }

        public async Task<Core.Models.User> GetById(string userId)
        {
            if (userId == null)
            {
                throw new NullReferenceException("userId mustn't be null");
            }

            var user = await _usersRepository.GetById(userId);
            if (user is null || user.Id != userId)
            {
                throw new ObjectNotFoundException("User wasn't found");
            }
            return user;
        }

        public async Task<Core.Models.User> GetByUserName(string username)
        {
            if (username == null)
            {
                throw new NullReferenceException("userName mustn't be null");
            }

            var user = await _usersRepository.GetByUserName(username);
            if (user is null || user.UserName != username)
            {
                throw new ObjectNotFoundException("User wasn't found");
            }
            return user;
        }
    }
}