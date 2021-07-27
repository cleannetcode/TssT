using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TssT.API
{
    
    /// <summary>
    /// Класс для генерации токена. Необходимо перенести в secrets секретный ключ.
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// Издатель токена.
        /// </summary>
        public const string ISSUER = "TssT.Api";

        /// <summary>
        /// Потребитель токена
        /// </summary>
        public const string AUDIENCE = "TssT.Frontend"; 

        /// <summary>
        /// Ключ для шифрования.
        /// </summary>
        const string KEY = "mysupersecret_secretkey!123";

        /// <summary>
        /// Время жизни токена в минутах
        /// </summary>
        public const int LIFETIME = 100;

        /// <summary>
        /// Returns a new instance of Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
