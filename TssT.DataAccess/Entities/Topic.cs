﻿using System.Collections.Generic;

namespace TssT.DataAccess.Entities
{
    /// <summary>
    /// Топик / Предметная область
    /// </summary>
    public class Topic
    {
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Группы топиков
        /// </summary>
        public ICollection<TopicGroup> TopicGroup { get; set; }
    }
}