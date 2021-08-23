using System.ComponentModel.DataAnnotations;

namespace TssT.API.Contracts
{
    /// <summary>
    /// Контракт для нового пользователя.
    /// </summary>
    public class NewUser
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль для авторизации.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
