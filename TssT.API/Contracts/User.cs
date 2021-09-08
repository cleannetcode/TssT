namespace TssT.API.Contracts
{
    /// <summary>
    /// Класс для отображения пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Телефонный номер.
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}