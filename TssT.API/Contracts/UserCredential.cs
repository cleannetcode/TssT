using System.ComponentModel.DataAnnotations;

namespace TssT.API.Contracts
{
    /// <summary>
    /// Учетные данные пользователя.
    /// </summary>
    public class UserCredential
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
