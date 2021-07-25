using System;
using System.Collections.Generic;

namespace TssT.DataAccess.Entities
{
    public class TopicGroup
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Топики
        /// </summary>
        public List<Topic> Topics { get; set; }


        /// <summary>
        /// Дата и время удаления группы топиков
        /// </summary>
        public DateTime? DeletedAt { get; set; }
    }
}
