using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TssT.API
{
    /// <summary>
    /// Класс для генерации токена.
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
        /// Время жизни токена в минутах
        /// </summary>
        public const int LIFETIME = 100;

        /// <summary>
        /// Ключ для шифрования.
        /// </summary>
        public static string Key { get; set; } 

        /// <summary>
        /// Returns a new instance of Microsoft.IdentityModel.Tokens.SymmetricSecurityKey
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
