using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TssT.Core.Interfaces;
using TssT.Core.Models;

namespace TssT.DataAccess.Repositories
{
    /// <summary>
    /// Класс репозиторий для доступа к данным пользователя.
    /// </summary>
    public class UsersRepository : IUsersRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Entities.User> _userManager;
        private readonly SignInManager<Entities.User> _signInManager;

        public UsersRepository(IMapper mapper,
            ApplicationDbContext applicationDbContext,
            UserManager<Entities.User> userManager,
            SignInManager<Entities.User> signInManager
        )
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void Create(Core.Models.User newUser)
        {
            //_applicationDbContext.Users.Add();
        }

        public void Update(User user)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(User user)
        {
            throw new System.NotImplementedException();
        }

        public User GetById(int userId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получить пользователя по имени и паролю.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Возвращает пользователя.</returns>
        public async Task<User> GetByNameAndPassword(string userName, string userPassword)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException(nameof(userName));

            if (string.IsNullOrWhiteSpace(userPassword))
                throw new ArgumentException(nameof(userPassword));

            var user = await _applicationDbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == userName);

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userPassword, false);

            if (signInResult.Succeeded)
                return _mapper.Map<User>(user);
            else
                return null;
        }


    }
}