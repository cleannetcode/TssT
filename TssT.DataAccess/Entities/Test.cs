using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TssT.DataAccess.Entities
{
    public class Test: BaseEntity
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}