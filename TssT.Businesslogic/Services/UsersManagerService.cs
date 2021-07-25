using System;
using AutoMapper;
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
        public void Create(Core.Models.User newUser)
        {
            if (newUser.Email == null)
            {
                throw new NullReferenceException("Emait mustn't be empty");
            } 
            if (newUser.PasswordHash == null)
            {
                throw new NullReferenceException("Password mustn't be empty");
            } 
            if (newUser.UserName == null)
            {
                throw new NullReferenceException("UserName mustn't be empty");
            } 
            
            
            
            _usersRepository.Create(newUser);
        }

        public void Update(Core.Models.User user)
        {
           
        }

        public void Delete(Core.Models.User user)
        {
            
        }

        public Core.Models.User GetById(int userId)
        {
            return null;
        }
    }
}