namespace TssT.Core.Models
{
    public class LevelKnowledge
    {
        public int Id { get; set; }

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