using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TssT.API.Contracts;
using User = TssT.DataAccess.Entities.User;

namespace TssT.API.Controllers
{
    /// <summary>
    /// Контроллер авторизации.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AuthOptions _authOptions;

        public AuthController(IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            AuthOptions authOptions)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authOptions = authOptions;
        }

        /// <summary>
        /// Получить токен для доступа к API.
        /// </summary>
        /// <param name="userCredential">Учетные данные пользователя.</param>
        /// <returns>Bearer токен.</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(TokenContract), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetToken(UserCredential userCredential)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await GetByNameAndPassword(userCredential.Name, userCredential.Password);

            if (user != null)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);

                var rolesString = String.Join(",", roles);

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userCredential.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, rolesString)
                };

                var symmetricSecurityKey = _authOptions.GetSymmetricSecurityKey();

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256),
                    Audience = _authOptions.Audience,
                    Issuer = _authOptions.Issuer
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new TokenContract{ Token = tokenString });
            }
            else
            {
                return Unauthorized("Authorize is failed.");
            }
        }

        /// <summary>
        /// Получить пользователя по имени и паролю.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="userPassword">Пароль пользователя.</param>
        /// <returns>Возвращает пользователя.</returns>
        private async Task<User> GetByNameAndPassword(string userName, string userPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return null;

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userPassword, false);

            if (signInResult.Succeeded)
                return user;
            else
                return null;
        }
    }
}
