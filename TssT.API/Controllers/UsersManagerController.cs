using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TssT.Core.Interfaces;

namespace TssT.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersManagerController: ControllerBase
    {
        private readonly IUsersManagerService _usersManagerService;
        private readonly IMapper _mapper;

        public UsersManagerController(IUsersManagerService usersManagerService, IMapper mapper)
        {
            _usersManagerService = usersManagerService;
            _mapper = mapper;
        }
        
        [HttpPost]
        public void Create(Contracts.User newUser)
        {
            _usersManagerService.Create(_mapper.Map<Contracts.User, Core.Models.User>(newUser));
        }
        
        [HttpPut]
        public void Update(Contracts.User user)
        {
           
        }

        [HttpDelete]
        public void Delete(Contracts.User user)
        {
            
        }

        [HttpGet]
        public Contracts.User GetById(int userId)
        {
            return null;
        }
    }
}