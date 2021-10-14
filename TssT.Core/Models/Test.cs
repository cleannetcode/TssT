using System;
using System.Collections.Generic;

namespace TssT.Core.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public IReadOnlyCollection<Topic> Topics { get; set; }
    }
}