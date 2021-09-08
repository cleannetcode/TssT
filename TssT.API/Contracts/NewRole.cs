using System.ComponentModel.DataAnnotations;

namespace TssT.API.Contracts
{
    /// <summary>
    /// Новая роль.
    /// </summary>
    public class NewRole
    {
        /// <summary>
        /// Имя новой роли.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
