using System.Net;
using System.Threading.Tasks;
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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(Contracts.NewUser newUser)
        {
            var user = _mapper.Map<Contracts.NewUser, Core.Models.User>(newUser);
            _usersManagerService.Create(user);
            
            return Ok(user.Id);
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