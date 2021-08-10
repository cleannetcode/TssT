namespace TssT.API.Contracts
{
    public class NewLevelKnowledge
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Очки, для оценки общего результата
        /// </summary>
        public int Points { get; set; }
    }
}