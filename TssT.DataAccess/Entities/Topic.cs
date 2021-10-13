using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TssT.DataAccess.Entities
{
    /// <summary>
    /// Топик / Предметная область
    /// </summary>
    public class Topic: BaseEntity
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }

        [NotMapped]
        public Test Test { get; set; }

        [Required]
        public int TestId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}