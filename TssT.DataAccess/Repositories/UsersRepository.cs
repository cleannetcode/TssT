using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        public void Create(Core.Models.User newUser)
        {
            
            _userManager.CreateAsync(_mapper.Map<Core.Models.User, Entities.User>(newUser));
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
    }
}