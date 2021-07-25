using System.Collections.Generic;

namespace TssT.Core.Models
{
    /// <summary>
    /// Группа топиков
    /// </summary>
    public class TopicGroup
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Родительская группа топиков
        /// </summary>
        public TopicGroup Parent { get; set; }
    }
}
