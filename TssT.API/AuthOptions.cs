using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TssT.API
{
    /// <summary>
    /// Класс для генерации токена.
    /// </summary>
    public class AuthOptions
    {
        private readonly IConfiguration _configuration;

        public AuthOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Издатель токена.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Потребитель токена.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Время жизни токена в минутах.
        /// </summary>
        public int LifeTime;

        /// <summary>
        /// Ключ для шифрования.
        /// </summary>
        public string Key { get; set; }

        public AuthOptions Configure()
        {
            Key = _configuration["EncryptionKey"];
            Issuer = _configuration["AuthOptions:Issuer"];
            Audience = _configuration["AuthOptions:Audience"];

            int lifeTime;
            var resultParse = int.TryParse(_configuration["AuthOptions:LifeTime"], out lifeTime);
            LifeTime = resultParse ? lifeTime: 100;

            return this;
        }

        /// <summary>
        /// Returns a new instance of Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
        /// </summary>
        /// <returns></returns>
        public SymmetricSecurityKey GetSymmetricSecurityKey() => 
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        
    }
}
