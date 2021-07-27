namespace TssT.API.Contracts
{
    public class LevelKnowledgeCreate
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