namespace TssT.API.Contracts
{
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public string RoleId { get; set; }
    }
}
