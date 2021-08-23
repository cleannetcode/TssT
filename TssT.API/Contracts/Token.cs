namespace TssT.API.Contracts
{
    /// <summary>
    /// Класс обёртка для JWT токена.
    /// </summary>
    public class TokenContract
    {
        /// <summary>
        /// Сгенерированный JWT токен.
        /// </summary>
        public string Token { get; set; }
    }
}
