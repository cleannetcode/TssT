using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TssT.Core.Models;

namespace TssT.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<Entities.User> _userManager;
        private readonly IMapper _mapper;

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

        public async Task<IActionResult> Update(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IActionResult> Delete(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetById(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}